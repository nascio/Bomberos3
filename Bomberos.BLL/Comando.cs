using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bomberos.BLL {

    public class Comando : ICommand {
        private Action metodo;

        public Comando (Action metodo) {
            this.metodo = metodo;
        }

        public Boolean CanExecute (Object parameter) {
            return true;
        }

        public void Execute (Object parameter) {
            this.metodo ( );
        }

        public event EventHandler CanExecuteChanged;

        internal void OnCanExecuteChanged ( ) {
            CanExecuteChanged?.Invoke (this, EventArgs.Empty);
        }
    }
}