using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Bomberos.BLL;
using System.Diagnostics;

namespace Bomberos {

    /// <summary>
    ///     Lógica de interacción para TipoIncidentesControl.xaml 
    /// </summary>
    public partial class TipoIncidentesControl : UserControl, IErrorMsg {

        public TipoIncidentesControl ( ) {
            InitializeComponent ( );

            (this.DataContext as BLL.MVTipoIncidentes).ErrorMSG = this;
        }

        private class Context : BLL.Notificador {
            public int intento = 0;
            public bool dejarAbierto = true;
            private string s;
            private string s2;
            private ICommand cc;

            public ICommand cm {
                get {
                    return cc ?? (cc = new BLL.Comando (( ) => {
                        dejarAbierto = false;
                    }));
                }
            }

            public string msg1 {
                get {
                    return s;
                }
                set {
                    s = value;
                    base.OnPropertyChanged ( );
                }
            }

            public string segundo {
                get {
                    return s2;
                }
                set {
                    s2 = value;
                    base.OnPropertyChanged ( );
                }
            }
        }

        private Context intentoActualizarSinConexion = new Context ( );
        private Context intentoEliminarSinConexion = new Context ( );
        private Context intentoEliminaNoSePuede = new Context ( );

        private Boolean Errores (Context intento, String llaveRecurso, int intentoMaximo = 3) {
            var intentar = !(++intento.intento >= intentoMaximo);

            Action accion = ( ) => {
                var resBorder = this.TryFindResource (llaveRecurso) as Border;
                resBorder.DataContext = intento;
                var count = 5;

                switch (intento.intento) {
                    case 1:
                        intento.msg1 = "Primer intento";
                        break;

                    case 2:
                        intento.msg1 = "Segundo intento";
                        break;

                    default:
                        intento.msg1 = "Ultimo intento";
                        break;
                }

                intento.segundo = "5";

                this.f.Children.Add (resBorder);

                var tmrCuentaRegresiva = new DispatcherTimer (DispatcherPriority.Render, Application.Current.Dispatcher) {
                    Interval = new TimeSpan (0, 0, 1)
                };
                tmrCuentaRegresiva.Tick += (x, y) => {
                    if (!intento.dejarAbierto) {
                        tmrCuentaRegresiva.Stop ( );
                        this.f.Children.Remove (resBorder);
                        return;
                    }

                    if (count == 0) {
                        tmrCuentaRegresiva.Stop ( );
                        return;
                    }

                    count--;

                    intento.segundo = count + "";
                    if (count == 0) {
                        this.f.Children.Remove (resBorder);
                    }
                };
                tmrCuentaRegresiva.Start ( );
            };

            App.Current?.Dispatcher?.BeginInvoke (accion, DispatcherPriority.Render);
            Thread.Sleep (5100);

            return intentar && intento.dejarAbierto;
        }

        //public void ErrorHappen (BLL.ErrorMsgArgs e) {
        //    //if () {
        //    e.Reintentar = !(++iii.intento >= 3);
        //    //}
        //    var r = iii.intento;

        // Action n = ( ) => { var resBorder = this.TryFindResource ("tipoIncidenteMsgError") as
        // Border; resBorder.DataContext = iii; var content = new ContentControl ( ) { Content =
        // resBorder, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment =
        // VerticalAlignment.Bottom };

        // //var border = new Border ( ) { // HorizontalAlignment = HorizontalAlignment.Right, //
        // VerticalAlignment = VerticalAlignment.Bottom, // Background = Brushes.LightCoral, // Child
        // = new TextBlock ( ) { // Text = "No hay conexion a la red. \nVolviendo a intentar de
        // nuevo. (5 segs)" // }, // CornerRadius = new CornerRadius (5), // Padding = new Thickness
        // (5), //}; Grid.SetColumnSpan (content, 3);

        // switch (iii.intento) { case 1: iii.msg1 = "Primer intento"; break; case 2: iii.msg1 =
        // "Segundo intento"; break; default: iii.msg1 = "Ultimo intento"; break; }

        // iii.segundo = 5;

        // this.f.Children.Add (content);

        // var nnn = new DispatcherTimer (DispatcherPriority.Render, Application.Current.Dispatcher);
        // var count = 5; nnn.Interval = new TimeSpan (0, 0, 1); nnn.Tick += (x, y) => { if
        // (!iii.intentar) { nnn.Stop ( ); this.f.Children.Remove (content); return; }

        // if (count == 0) { nnn.Stop ( ); return; // count = 1; }

        // count--;

        // iii.segundo = count; if (count == 0) { this.f.Children.Remove (content); }

        // }; nnn.Start ( );

        // }; App.Current?.Dispatcher?.BeginInvoke (n, DispatcherPriority.Render);

        // Thread.Sleep (5100);

        // e.Reintentar = iii.intentar; Console.WriteLine ("Error conexion");

        //}

        public void ErrorEnActualizar (ErrorMsgArgs e) {
            Console.WriteLine ("Error al actualizar, sin conexion");
            e.Reintentar = Errores (this.intentoActualizarSinConexion, "tipoIncidenteMsgError");
        }

        public void ErrorEnEliminar (ErrorMsgArgs e) {
            Console.WriteLine ("Error en eliminar, sin conexion");
            e.Reintentar = Errores (this.intentoEliminarSinConexion, "tipoIncidenteEliminarMsgError");
        }

        public void ErrorEnEliminarNoSePuede (ErrorMsgArgs e) {
            Console.WriteLine ("Error en eliminar, no es posible");

            var intento = this.intentoEliminaNoSePuede;
            Action accion = ( ) => {
                var resBorder = this.TryFindResource ("tipoIncidenteEliminarMsgErrorNoSePuede") as Border;
                resBorder.DataContext = this.intentoEliminaNoSePuede;
                var count = 5;

                intento.segundo = e.Motivo + "\n(" + count + " segs).";

                this.f.Children.Add (resBorder);

                var tmrCuentaRegresiva = new DispatcherTimer (DispatcherPriority.Render, Application.Current.Dispatcher) {
                    Interval = new TimeSpan (0, 0, 1)
                };
                tmrCuentaRegresiva.Tick += (x, y) => {
                    if (!intento.dejarAbierto) {
                        tmrCuentaRegresiva.Stop ( );
                        this.f.Children.Remove (resBorder);
                        return;
                    }

                    if (count == 0) {
                        tmrCuentaRegresiva.Stop ( );
                        return;
                    }

                    count--;

                    intento.segundo = e.Motivo + "\n(" + count + " segs).";

                    if (count == 0) {
                        this.f.Children.Remove (resBorder);
                    }
                };
                tmrCuentaRegresiva.Start ( );
            };

            App.Current?.Dispatcher?.BeginInvoke (accion, DispatcherPriority.Render);
            Thread.Sleep (5100);
        }

        public void ErrorPorMensaje (Boolean bueno, String mensaje) => throw new NotImplementedException ( );
        public void Quitar ( ) => throw new NotImplementedException ( );
        public void AccionHiloPrincipal (Action accion) => throw new NotImplementedException ( );
    }
}