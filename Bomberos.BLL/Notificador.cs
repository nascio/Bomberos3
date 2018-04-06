using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL {

    public class Notificador : INotifyPropertyChanged {
        private readonly object @lock = new object ( );

        private PropertyChangedEventHandler propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged {
            add {
                lock (@lock) {
                    this.propertyChanged += value;
                }
            }
            remove {
                lock (@lock) {
                    this.propertyChanged -= value;
                }
            }
        }

        protected void OnPropertyChanged ([CallerMemberName] String propertyName = "") {
            var handler = null as PropertyChangedEventHandler;

            lock (@lock) {
                handler = this.propertyChanged;
            }

            handler?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        public void Liberar ( ) {
            lock (@lock) {
                this.propertyChanged = null;
            }
        }


        internal static T? IF<T> (T val1, T val2) where T : struct {
            return val1.Equals (val2) ? (T?) null : val1;
        }
        internal static T? IFE<T> (T? val1, T? val2) where T : struct {
            if (val1 == null || val2 == null) {
                return val1.HasValue == val2.HasValue ? (T?) null : val1;
            }
            return val1.Equals (val2) ? (T?) null : val1;
        }
        internal static T IFC<T> (T val1, T val2) where T : class {
            return val1.Equals (val2) ? null : val1;
        }

        internal static Boolean StringIsNullOrEmpty (String valor) {
            return valor == null || valor.Length == 0 || valor.Trim ( ).Length == 0;
        }

    }
}