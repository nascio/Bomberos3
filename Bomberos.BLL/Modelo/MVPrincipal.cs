using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bomberos.BLL.Modelo {

    public abstract class MVPrincipal : Notificador {
        private IErrorMsg errorMsg;

        public IErrorMsg ErrorMSG {
            protected get {
                return this.errorMsg;
            }
            set {
                this.errorMsg = value;
            }
        }

        private Comando quitar;

        public Comando QuitarCMD {
            get {
                return this.quitar ?? (this.quitar = new Comando (this.OnQuitar));
            }
        }

        private ErrorMsgArgs args;

        protected ErrorMsgArgs Args {
            get {
                return this.args ?? (this.args = new ErrorMsgArgs ( ));
            }
        }

        protected void OnErrorMsgSinConexion (Action metodo) {
            if (this.errorMsg == null) {
                return;
            }

            this.Args.Reintentar = true;

            this.errorMsg.ErrorEnActualizar (this.Args);

            if (this.Args.Reintentar) {
                var t = new Timer (x => metodo ( ));
                t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
            }
        }

        protected void OnErrorMsgEliminar (Action item) {
            if (this.errorMsg == null) {
                return;
            }

            this.Args.Reintentar = true;

            this.errorMsg.ErrorEnEliminar (this.Args);

            if (this.Args.Reintentar) {
                var t = new Timer (x => item ( ));
                t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
            }
        }

        protected void OnErrorMsgEliminarNoSePuede ( ) {
            if (this.errorMsg != null) {
                this.Args.Reintentar = true;

                this.errorMsg.ErrorEnEliminarNoSePuede (this.Args);
            }
        }

        protected void OnErrorPorMensaje (String mensaje) {
            if (this.errorMsg == null) {
                return;
            }

            this.errorMsg.ErrorPorMensaje (false, mensaje);
        }

        protected void OnMensajeBueno (String mensaje) {
            if (this.errorMsg == null) {
                return;
            }

            this.errorMsg.ErrorPorMensaje (true, mensaje);
        }

        protected void OnQuitar ( ) {
            if (this.errorMsg == null) {
                return;
            }

            this.errorMsg.Quitar ( );
        }

      
    }
}