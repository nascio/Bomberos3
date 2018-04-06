using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bomberos.BLL {

    public class MVBombero : Modelo.MVPrincipal {

        public MVBombero ( ) {
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
                var incidentes = Bombero.ObtenerBomberos ( );
                if (incidentes == null) {
                    throw new Excepciones.SinConexionException ( );
                }

                this.coleccion = new ObservableCollection<Bombero> (incidentes);
                base.OnPropertyChanged ("Coleccion");
            }
            catch (Excepciones.SinConexionException) {
                base.OnErrorMsgSinConexion (this.ActualizarLista);
            }
        }

        #endregion

        #region Lista

        private ObservableCollection<Bombero> coleccion;

        public ObservableCollection<Bombero> Coleccion {
            get {
                return this.coleccion;
            }
        }

        public Bombero Seleccion {
            get;
            set;
        }

        #endregion

        #region Nuevo/Editar

        private Bombero actual;
        private bool? editarNuevoEstado;
        private Comando editarNuevo, nuevoUsuario;
        private String actualPassword;
        private String actualNickName;

        public Bombero Actual {
            get {
                return this.actual;
            }
            set {
                this.actual = value;
                base.OnPropertyChanged ( );
            }
        }

        public String ActualNickName {
            get {
                return this.actualNickName;
            }
            set {
                this.actualNickName = value;
                base.OnPropertyChanged ( );
            }
        }
        public String ActualPassword {
            get {
                return this.actualPassword;
            }
            set {
                this.actualPassword = value;
                base.OnPropertyChanged ( );
            }
        }
        public Boolean ActualConUsuario {
            get; set;
        }

        private Acceso acceso;
        public Acceso Acceso {
            get {
                return this.acceso;
            }
            set {
                this.acceso = value;
                base.OnPropertyChanged ( );
            }
        }

        private List<Acceso> accesos;
        public List<Acceso> Accesos {
            get {
                return this.accesos;
            }
        }

        public void Inicializar ( ) {
            var b = Acceso.Incidentes ( );

            this.accesos = new List<Acceso> (b);

            base.OnPropertyChanged ("Accesos");
        }



        public void Nuevo ( ) {
            this.editarNuevoEstado = true;
            this.Acceso = this.accesos.FirstOrDefault ( );
            this.ActualPassword = String.Empty;
            this.ActualNickName = string.Empty;
            this.ActualConUsuario = false;
            this.Actual = new Bombero ( );
        }

        public void Editar ( ) {
            this.editarNuevoEstado = false;
            this.Acceso = this.accesos.FirstOrDefault ( );
            this.ActualPassword = String.Empty;
            this.ActualNickName = string.Empty;
            this.ActualConUsuario = false;
            this.Actual = this.Seleccion;
        }

        public void Aceptar ( ) {
            if (!this.editarNuevoEstado.HasValue) {
                base.OnErrorPorMensaje ("No se seleccion la opcion de Nuevo o Editar.");
            }

            var errores = new List<string> ( );

            if (this.editarNuevoEstado.Value) {
                if (StringIsNullOrEmpty (this.actual.NombreCompleto)) {
                    errores.Add ("Se necesita el nombre del bombero.");
                }

                if (StringIsNullOrEmpty (this.actual.DPI)) {
                    errores.Add ("Se necesita el DPI del bombero.");
                }

                if (this.ActualConUsuario) {
                    if (StringIsNullOrEmpty (this.ActualNickName)) {
                        errores.Add ("Se necesita el nombre de usuario del bombero.");
                    }
                    if (StringIsNullOrEmpty (this.ActualPassword) || this.ActualPassword.Length < 6) {
                        errores.Add ("Se necesita una contraseña con no menos de 6 caracteres.");
                    }
                    else if (!Regex.IsMatch (this.ActualPassword, @"\d") || !Regex.IsMatch (this.ActualPassword, @"[a-z]", RegexOptions.IgnoreCase)) {
                        errores.Add ("Se necesita una contraseña con un numero y una letra como minimo.");
                    }
                }
            }
            else/* if (!this.actual.Estado)*/ {
                if (StringIsNullOrEmpty (this.actual.NombreCompleto)) {
                    errores.Add ("Se necesita el nombre del bombero.");
                }
                if (StringIsNullOrEmpty (this.actual.DPI)) {
                    errores.Add ("Se necesita el DPI del bombero.");
                }

                if (this.ActualConUsuario) {
                    if (StringIsNullOrEmpty (this.ActualNickName)) {
                        errores.Add ("Se necesita el nombre de usuario del bombero.");
                    }
                    if (StringIsNullOrEmpty (this.ActualPassword) || this.ActualPassword.Length < 6) {
                        errores.Add ("Se necesita una contraseña con no menos de 6 caracteres.");
                    }
                    else if (!Regex.IsMatch (this.ActualPassword, @"\d") || !Regex.IsMatch (this.ActualPassword, @"[a-z]", RegexOptions.IgnoreCase)) {
                        errores.Add ("Se necesita una contraseña con un numero y una letra como minimo.");
                    }
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
                    this.actual.Estado = EstadoBombero.Activo;


                    if (ActualConUsuario) {
                        this.actual.Insertar (0, this.ActualNickName, this.actualPassword, this.Acceso.ID);
                    }
                    else {
                        this.actual.Insertar (0);
                    }

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

                    if (this.ActualConUsuario) {
                        try {
                            this.actual.Editar (0, this.ActualNickName, this.actualPassword, this.Acceso.ID);
                        }
                        catch (Excepciones.AlgoMasException) {
                            this.actual.Editar (0);

                        }
                        catch {
                            throw;
                        }
                    }
                    else {
                        this.actual.Editar (0);
                    }

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
            catch (Exception ex) {
                Console.WriteLine (ex.ToString ( ));
            }
        }

        public ICommand EditarNuevoCMD {
            get {
                return this.editarNuevo ?? (this.editarNuevo = new Comando (this.editarNuevoCMD));
            }
        }

        public ICommand NuevoUsuarioCMD {
            get {
                return this.nuevoUsuario ?? (this.nuevoUsuario = new Comando (this.nuevoUsuarioCMD));
            }
        }

        private void nuevoUsuarioCMD ( ) {
            this.ActualConUsuario = !this.ActualConUsuario;
            base.OnPropertyChanged ("ActualConUsuario");
        }

        private void editarNuevoCMD ( ) {
            var t = new Timer (x => this.Aceptar ( ));
            t.Change (TimeSpan.FromSeconds (1), Timeout.InfiniteTimeSpan);
        }

        #endregion
    }
}