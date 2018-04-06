using Bomberos.BLL.Excepciones;
using Bomberos.DAL.DataSet1TableAdapters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL {

    public enum EstadoBombero : sbyte {
        Vacio = 0,
        Inactivo = 1,
        Activo = 2
    }

    public class Bombero : Notificador {

        #region Atributos

        private string apellido;
        private Bombero copia;

        private string dpi;
        private EstadoBombero estado;
        private int id;
        private string nombre;
        private Usuario user;
        private int? id_usuario;

        #endregion

        #region Propiedades

        public Usuario User {
            get {
                return this.user;
            }
        }

        public string Apellido {
            get {
                return this.apellido;
            }
            set {
                this.apellido = value;
                base.OnPropertyChanged ( );
                base.OnPropertyChanged ("NombreCompleto");
            }
        }

        public string DPI {
            get {
                return this.dpi;
            }
            set {
                this.dpi = value;
                base.OnPropertyChanged ( );
            }
        }

        public EstadoBombero Estado {
            get {
                return this.estado;
            }
            set {
                this.estado = value;
                base.OnPropertyChanged ( );
            }
        }

        public Int32 ID {
            get {
                return this.id;
            }
            set {
                this.id = value;
            }
        }

        public String Nombre {
            get {
                return this.nombre;
            }
            set {
                this.nombre = value;
                base.OnPropertyChanged ( );
                base.OnPropertyChanged ("NombreCompleto");
            }
        }

        public String NombreCompleto {
            get {
                return String.Concat (this.nombre, " ", this.apellido);
            }
        }

        public String Tiempo {
            get {
                var d = DateTime.Now;

                if (d.Hour > 17 && d.Minute > 30) {
                    return "a buena noche.";
                }

                if (d.Hour > 12) {
                    return "a buena tarde.";
                }

                return " buen dia.";
            }
        }

        public bool HayUsuario {
            get {
                return this.id_usuario.HasValue;
            }
        }

        #endregion

        #region Metodos

        public void Editar (int id_redactor) {
            try {
                using (var r = new insertarTableAdapter ( )) {
                    using (var result = r.sp_bombero (2, this.id, IFC (this.nombre, copia.nombre), IFC (this.apellido, copia.apellido), IFC (this.dpi, copia.dpi), null, IF (this.estado, copia.estado), null, IFE (this.user == null ? null : (int?) this.user.ID, copia.user == null ? (int?) null : copia.user.ID), id_redactor)) {
                        var row = result.First ( );

                        if (row.out_status != 1200) {
                            throw new NoSePuedeEliminarException ("el tipo de incidente.");
                        }

                        //this.id = -1;
                        this.copia = base.MemberwiseClone ( ) as Bombero;
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

        public void Editar (int id_redactor, string username, string contra, int id_acceso) {
            try {
                if (HayUsuario) {
                    throw new AlgoMasException ( );
                }

                using (var r = new insertarTableAdapter ( )) {

                    var id_user = -1;

                    using (var s = r.sp_user (1, username, Usuario.CifrarValor (contra), id_acceso)) {
                        var ss = s.First ( );

                        if (ss.out_status != 1) {
                            throw new NoSePudoIngresarException ("");
                        }

                        id_user = ss.out_id;
                    }

                    using (var result = r.sp_bombero (2, this.id, IFC (this.nombre, copia.nombre), IFC (this.apellido, copia.apellido), IFC (this.dpi, copia.dpi), null, IF (this.estado, copia.estado), null, id_user, id_redactor)) {
                        var row = result.First ( );

                        if (row.out_status != 1200) {
                            throw new NoSePuedeEliminarException ("el tipo de incidente.");
                        }

                        //this.id = -1;
                        this.copia = base.MemberwiseClone ( ) as Bombero;
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




                using (var r = new insertarTableAdapter ( )) {



                    using (var s = r.sp_bombero (1, null, this.nombre, this.apellido, this.dpi, null, (byte) this.estado, null, user == null ? null : (int?) user.ID, id_redactor)) {
                        var ss = s.First ( );

                        if (ss.out_status != 1) {
                            throw new NoSePudoIngresarException ("");
                        }

                        this.copia = base.MemberwiseClone ( ) as Bombero;
                        this.id = ss.out_id;
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

        public void Insertar (int id_redactor, string username, string contra, int id_acceso) {
            try {
                if (this.copia != null) {
                    return;
                }

                using (var r = new insertarTableAdapter ( )) {

                    var id_user = -1;

                    using (var s = r.sp_user (1, username, Usuario.CifrarValor (contra), id_acceso)) {
                        var ss = s.First ( );

                        if (ss.out_status != 1) {
                            throw new NoSePudoIngresarException ("");
                        }

                        id_user = ss.out_id;
                    }

                    using (var s = r.sp_bombero (1, null, this.nombre, this.apellido, this.dpi, null, (byte) this.estado, null, id_user, id_redactor)) {
                        var ss = s.First ( );

                        if (ss.out_status != 1) {
                            throw new NoSePudoIngresarException ("");
                        }

                        this.copia = base.MemberwiseClone ( ) as Bombero;
                        this.id = ss.out_id;
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


        public static Bombero BuscarPorUsuario (String nickname) {
            try {
                using (var consulta = new DAL.DataSet1TableAdapters.bomberoTableAdapter ( )) {
                    var tabla = consulta.GetDataBy (nickname);

                    using (var sk = tabla.GetEnumerator ( )) {
                        if (!sk.MoveNext ( )) {
                            throw new InvalidOperationException ("Sin usuarios en la consulta.");
                        }

                        var fila = sk.Current;

                        if (sk.MoveNext ( )) {
                            throw new InvalidOperationException ("Multiples usuarios en la consulta.");
                        }

                        var r = new Bombero ( ) {
                            nombre = fila.nombre,
                            apellido = fila.apellido,
                            dpi = fila.dpi,
                            id = fila.id_bombero,
                            estado = fila.IsestadoNull ( ) ? EstadoBombero.Vacio : (EstadoBombero) fila.estado,
                            user = Usuario.BuscarPorUsuario (fila.id_usuario)
                        };
                        r.copia = r.MemberwiseClone ( ) as Bombero;
                        return r;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine (ex.ToString ( ));

                return null;
            }
        }

        public static List<Bombero> ObtenerBomberos ( ) {
            try {
                using (var consulta = new DAL.DataSet1TableAdapters.bomberoTableAdapter ( )) {
                    var tabla = consulta.GetData ( );

                    return tabla.Select (fila => {
                        var r = new Bombero ( ) {
                            nombre = fila.nombre,
                            apellido = fila.apellido,
                            dpi = fila.dpi,
                            id = fila.id_bombero,
                            estado = fila.IsestadoNull ( ) ? EstadoBombero.Vacio : (EstadoBombero) fila.estado,
                            id_usuario = fila.Isid_usuarioNull ( ) ? null : (int?) fila.id_usuario
                        };
                        r.copia = r.MemberwiseClone ( ) as Bombero;
                        return r;
                    }).ToList ( );
                }
            }
            catch (Exception ex) {
                Console.WriteLine (ex.ToString ( ));
                return null;
                //throw;
            }
        }

        public bool AccesoValido (String nombre_control) {
            try {
                if (estado == EstadoBombero.Inactivo) {
                    return false;
                }

                return user.AccesoValido (nombre_control);
            }
            catch {
                return false;
            }
        }

        #endregion
    }
}