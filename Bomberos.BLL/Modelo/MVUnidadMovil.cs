using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bomberos.BLL {

    public class MVUnidadMovil : Modelo.MVPrincipal {

        public MVUnidadMovil ( ) {
            this.actualizarListaCMD ( );
        }

        #region Actualizar lista

        private Comando actualizarLista;

        public ICommand ActualizarListaCMD {
            get {
                return this.actualizarLista ?? (this.actualizarLista = new Comando (this.actualizarListaCMD));
            }
        }

        private void actualizarListaCMD ( ) {
            var t = new Timer (x => this.ActualizarLista ( ));
            t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
        }

        public void ActualizarLista ( ) {
            try {
                var incidentes = UnidadMovil.ObtenerUnidadesMoviles ( );
                if (incidentes == null) {
                    throw new Excepciones.SinConexionException ( );
                }

                this.coleccion = new ObservableCollection<UnidadMovil> (incidentes);
                base.OnPropertyChanged ("Coleccion");
            }
            catch (Excepciones.SinConexionException) {
                base.OnErrorMsgSinConexion (this.ActualizarLista);
            }
        }

        #endregion

        #region Lista

        private ObservableCollection<UnidadMovil> coleccion;

        public ObservableCollection<UnidadMovil> Coleccion {
            get {
                return this.coleccion;
            }
        }

        public UnidadMovil Seleccion {
            get;
            set;
        }

        #endregion

        #region Nuevo/Editar

        private UnidadMovil actual;
        private bool? editarNuevoEstado;
        private Comando editarNuevo;

        public UnidadMovil Actual {
            get {
                return this.actual;
            }
            set {
                this.actual = value;
                base.OnPropertyChanged ( );
            }
        }

        public void Nuevo ( ) {
            this.editarNuevoEstado = true;
            this.Actual = new UnidadMovil ( );
        }

        public void Editar ( ) {
            this.editarNuevoEstado = false;
            this.Actual = this.Seleccion;
        }

        public void Aceptar ( ) {
            if (!this.editarNuevoEstado.HasValue) {
                base.OnErrorPorMensaje ("No se seleccion la opcion de Nuevo o Editar.");
            }

            var errores = new List<string> ( );

            if (this.editarNuevoEstado.Value) {
                if (StringIsNullOrEmpty (this.actual.Placa)) {
                    errores.Add ("Se necesita la placa del vehiculo.");
                }

                if (this.actual.Numero == 0) {
                    errores.Add ("Se necesita un numero de la unidad.");
                }
            }
            else if (!this.actual.Estado) {
                if (StringIsNullOrEmpty (this.actual.Placa)) {
                    errores.Add ("Se necesita la placa del vehiculo.");
                }

                if (this.actual.Numero == 0) {
                    errores.Add ("Se necesita un numero de la unidad.");
                }
            }

            if (errores.Count > 0) {
                foreach (var item in errores) {
                    base.OnErrorPorMensaje (item);
                }
                return;
            }

            try {

                Action ingresar = null;

                if (this.editarNuevoEstado.Value) {
                    this.actual.Insertar (0);

                    var n = this.actual;
                    var s = this.coleccion;

                    ingresar = ( ) => {
                        try {
                            if (s != null) {
                                s.Add (n);
                            }
                        }
                        catch { }
                    };
                }
                else {
                    this.actual.Editar (0);
                }

                this.editarNuevoEstado = null;
                this.Actual = null;

                if (base.ErrorMSG != null) {
                    base.ErrorMSG.AccionHiloPrincipal (ingresar);
                }

                //base.OnMensajeBueno ("Cambios realizados.");
            }
            catch (MySqlException ex) {
                Console.WriteLine (ex.ToString ( ));
                base.OnErrorPorMensaje ("Errores en el servidor.");
            }
        }

        public ICommand EditarNuevoCMD {
            get {
                return this.editarNuevo ?? (this.editarNuevo = new Comando (this.editarNuevoCMD));
            }
        }

        private void editarNuevoCMD ( ) {
            var t = new Timer (x => this.Aceptar ( ));
            t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
        }

        #endregion
    }
}