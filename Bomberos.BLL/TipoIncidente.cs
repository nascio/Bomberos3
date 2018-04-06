using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bomberos.DAL;
using Bomberos.DAL.DataSet1TableAdapters;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;
using MySql.Data.MySqlClient;
using Bomberos.BLL.Excepciones;

namespace Bomberos.BLL {

    public interface IErrorMsg {

        #region Metodos

        void ErrorEnActualizar (ErrorMsgArgs e);

        void ErrorEnEliminar (ErrorMsgArgs e);

        void ErrorEnEliminarNoSePuede (ErrorMsgArgs e);

        void ErrorPorMensaje (Boolean bueno, String mensaje);

        #endregion

        void Quitar ( );

        void AccionHiloPrincipal (Action accion);
    }

    public class ErrorMsgArgs {

        #region Atributos

        public String Motivo = "";
        public Boolean Reintentar = true;

        #endregion

        #region Constructores

        public ErrorMsgArgs ( ) {
        }

        #endregion
    }

    public class MVTipoIncidentes : Notificador {

        #region Atributos

        private Comando actualizarLista, eliminar;
        private ErrorMsgArgs arg1;
        private IErrorMsg errorMsg;
        private ObservableCollection<TipoIncidente> incidentes;

        #endregion

        #region Constructores

        public MVTipoIncidentes ( ) {
            var t = new Timer (x => ActualizarLista ( ));
            t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
        }

        #endregion

        #region Propiedades

        public ICommand ActualizarListaCMD {
            get {
                return this.actualizarLista ?? (this.actualizarLista = new Comando (this.actualizarS));
            }
        }

        public ICommand EliminarCMD {
            get {
                return this.eliminar ?? (this.eliminar = new Comando (this.eliminarS));
            }
        }

        public IErrorMsg ErrorMSG {
            set {
                this.errorMsg = value;
            }
        }

        //public ICommand AgregarCMD {
        //    get {
        //        return this.agregar ?? (this.agregar = new Comando (this.agregarS));
        //    }
        //}

        public ObservableCollection<TipoIncidente> Incidentes {
            get {
                return this.incidentes;
            }
        }

        public TipoIncidente Seleccion {
            get;
            set;
        }

        #endregion

        #region Metodos

        private void actualizarS ( ) {
            var t = new Timer (x => this.ActualizarLista ( ));
            t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
        }

        private void agregarS ( ) {
            this.Agregar (this.Seleccion);
        }

        private void eliminarS ( ) {
            var t = new Timer (x => this.Eliminar (this.Seleccion));
            t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
        }

        private void OnErrorMsg (Action m) {
            if (this.errorMsg != null) {
                this.arg1 = this.arg1 ?? new ErrorMsgArgs ( );
                this.arg1.Reintentar = true;

                this.errorMsg.ErrorEnEliminarNoSePuede (this.arg1);
                // this.errorMsg.ErrorHappen (this.arg1);

                //if (!arg1.Reintentar) {
                //    return;
                //}

                //var t = new Timer (x => m ( ));
                //t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
            }
        }

        private void OnErrorMsgEliminar (TipoIncidente item) {
            if (this.errorMsg == null) {
                return;
            }

            this.arg1 = this.arg1 ?? new ErrorMsgArgs ( );
            this.arg1.Reintentar = true;

            this.errorMsg.ErrorEnEliminar (this.arg1);
            if (this.arg1.Reintentar) {
                var t = new Timer (x => this.Eliminar (item));
                t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
            }
        }

        private void OnErrorMsgSinConexion ( ) {
            if (this.errorMsg == null) {
                return;
            }

            this.arg1 = this.arg1 ?? new ErrorMsgArgs ( );
            this.arg1.Reintentar = true;

            this.errorMsg.ErrorEnActualizar (this.arg1);

            if (this.arg1.Reintentar) {
                var t = new Timer (x => this.ActualizarLista ( ));
                t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
            }
        }

        public void ActualizarLista ( ) {
            try {
                var incidentes = TipoIncidente.Incidentes ( );
                if (incidentes == null) {
                    throw new Excepciones.SinConexionException ( );
                }
                this.incidentes = new ObservableCollection<TipoIncidente> (incidentes);
                base.OnPropertyChanged ("Incidentes");
            }
            catch (Excepciones.SinConexionException) {
                OnErrorMsgSinConexion ( );
            }
        }

        public void Agregar (TipoIncidente nuevo) {
            if (nuevo == null) {
                return;
            }

            nuevo.Insertar (0);
            if (this.incidentes != null) {
                this.incidentes.Add (nuevo);
            }
        }

        public void Eliminar (TipoIncidente eliminar) {
            if (eliminar == null) {
                return;
            }

            try {
                eliminar.Eliminar (0);
                if (this.incidentes != null) {
                    this.incidentes.Remove (eliminar);
                }
            }
            catch (SinConexionException) {
                OnErrorMsgEliminar (eliminar);
            }
            catch (NoSePuedeEliminarException) {
                this.arg1 = this.arg1 ?? new ErrorMsgArgs ( );
                this.arg1.Motivo = "Hay una referencia al incidente.";
                OnErrorMsg (null);
            }
            catch (BloqueoException) {
                this.arg1 = this.arg1 ?? new ErrorMsgArgs ( );
                this.arg1.Motivo = "Esta prestablecido.";
                OnErrorMsg (null);
            }
            catch (Exception) {
                throw;
            }
        }

        #endregion
    }

    public class TipoIncidente : Notificador {

        #region Atributos

        private bool bloqueo;
        private TipoIncidente copia;
        private int id;
        private string nombre;

        #endregion

        #region Constructores

        public TipoIncidente ( ) {
            this.id = -1;
            this.nombre = string.Empty;
            this.copia = null;
        }

        #endregion

        #region Propiedades

        public bool Bloqueado {
            get {
                return this.bloqueo;
            }
        }

        public int ID {
            get {
                return this.id;
            }
            set {
                if (value == this.id) {
                    return;
                }

                this.id = value;
                this.OnPropertyChanged ( );
            }
        }

        public string Nombre {
            get {
                return this.nombre;
            }
            set {
                if (value == null || string.Compare (this.nombre, value, StringComparison.CurrentCultureIgnoreCase) == 0) {
                    return;
                }

                this.nombre = value;
                this.OnPropertyChanged ( );
            }
        }

        #endregion

        #region Metodos

        public static List<TipoIncidente> Incidentes ( ) {
            var s = null as List<TipoIncidente>;

            try {
                using (var r = new tipo_incidenteTableAdapter ( )) {
                    s = r.GetData ( ).Select (x => {
                        var n = new TipoIncidente ( ) {
                            nombre = x.nombre,
                            id = x.id_tipo_incidente,
                            bloqueo = x.id_tipo_incidente > 0 && x.id_tipo_incidente < 40
                        };
                        n.copia = n.MemberwiseClone ( ) as TipoIncidente;

                        return n;
                    }).ToList ( );
                }
            }
            catch (MySqlException ex) {
                var nex = Generador.GenerarDesdeMySqlException (ex);
                if (nex != null) {
                    throw nex;
                }
            }

            return s;
        }

        public void Eliminar (int id_redactor) {
            try {
                if (this.bloqueo) {
                    throw new BloqueoException ( );
                }

                using (var r = new insertarTableAdapter ( )) {
                    using (var result = r.sp_tipo_incidente (3, this.id, this.nombre, id_redactor)) {
                        var row = result.First ( );

                        if (row.out_status != 200) {
                            throw new NoSePuedeEliminarException ("el tipo de incidente.");
                        }

                        this.id = -1;
                        this.copia = null;
                    }
                }
            }
            catch (MySqlException ex) {
                var nex = Generador.GenerarDesdeMySqlException (ex);

                if (nex != null) {
                    throw nex;
                }
            }
        }

        public void Insertar (int id_redactor) {
            try {
                if (this.copia != null) {
                    return;
                }

                using (var r = new insertarTableAdapter ( ))
                using (var s = r.sp_tipo_incidente (1, null, this.nombre, id_redactor)) {
                    var ss = s.First ( );

                    if (ss.out_status != 1) {
                        throw new NoSePudoIngresarException ("");
                    }

                    this.copia = base.MemberwiseClone ( ) as TipoIncidente;
                    this.id = ss.out_id;
                }
            }
            catch (MySqlException ex) {
                var nex = Generador.GenerarDesdeMySqlException (ex);

                if (nex != null) {
                    throw nex;
                }
            }
        }

        #endregion
    }
}