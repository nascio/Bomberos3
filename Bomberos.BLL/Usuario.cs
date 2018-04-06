using Bomberos.DAL.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL {


    public enum EstadosUsuario : byte {
        Vacio = 0,
        Eliminado = 1,
        Valido = 2,
    }




    public class Usuario {

        public static bool BloquearCiertoTiempo = true;

        public int ID;
        int id_acceso;
        string nombre_acceso;


        public int IDAcceso {
            get {
                return this.id_acceso;
            }
        }

        EstadosUsuario estado;
        List<Tuple<int, string, bool>> accesos;

        public Usuario ( ) : this (true) { }

        public Usuario (bool iniciar) {
            if (!iniciar) {
                return;
            }

            this.id_acceso = -1;
            this.nombre_acceso = String.Empty;
            this.estado = EstadosUsuario.Vacio;
            this.accesos = new List<Tuple<int, string, bool>> ( );
        }





        public bool AccesoValido (String control_nombre) {
            if (estado == EstadosUsuario.Eliminado) {
                return false;
            }

            try {

                var s = accesos.First (x => String.CompareOrdinal (x.Item2, control_nombre) == 0);
                return s.Item3;
            }
            catch { return false; }
        }







        static public bool ComprobarInicioSesion (String usuario) {
            try {
                using (var consulta = new QueriesTableAdapter ( )) {
                    var sal = consulta.comprobarUsuario (usuario);

                    Console.WriteLine ("ComprobarInicioSesion");
                    Console.WriteLine (sal.GetType ( ));
                    Console.WriteLine ( );

                    return Convert.ToBoolean (sal);
                }
            }
            catch (Exception) {

            }
            return false;
        }

        static public bool ComprobarInicioSesion (String usuario, String contra) {

            try {
                var data = obtenerEncriptacion (contra);


                using (var consulta = new QueriesTableAdapter ( )) {

                    var sal = consulta.sp_comprobar_tipo1 (usuario, data);


                    Console.WriteLine ("ComprobarInicioSesion");
                    Console.WriteLine (sal.GetType ( ));
                    Console.WriteLine ( );

                    return Convert.ToBoolean (sal);
                }
            }
            catch (Exception) {

            }
            return false;
        }


        public static Byte[] CifrarValor (String valor) {
            return obtenerEncriptacion (valor);
        }


        private static Byte[] obtenerEncriptacion (String value) {

            // ToDo: Mejorar esta seccion para una mejor encriptacion.

            using (var sha = new System.Security.Cryptography.SHA256Managed ( )) {
                return sha.ComputeHash (Encoding.UTF8.GetBytes (value));
            }
        }







        public static Usuario BuscarPorUsuario (int id_usuario) {
            try {
                var id_acceso = 0;
                var nombre_acceso = String.Empty;
                var estado = EstadosUsuario.Vacio;
                var lista = new List<Tuple<int, string, bool>> ( );

                using (var consulta = new DAL.DataSet1TableAdapters.usuarioTableAdapter ( )) {
                    var tabla = consulta.GetData (id_usuario);
                    using (var sk = tabla.GetEnumerator ( )) {
                        if (!sk.MoveNext ( )) {
                            throw new InvalidOperationException ( );
                        }

                        var fila = sk.Current;

                        id_acceso = fila.id_acceso;
                        estado = fila.Isestado_usuarioNull ( ) ? EstadosUsuario.Vacio : (EstadosUsuario) fila.estado_usuario;
                        nombre_acceso = fila.nombre_acceso;
                    }
                }


                using (var consulta = new DAL.DataSet1TableAdapters.control_accesoTableAdapter ( )) {
                    var tabla = consulta.GetData (id_acceso);
                    lista.AddRange (tabla.Select (x => Tuple.Create (x.id_control, x.nombre, Convert.ToBoolean (x.estado))));
                }

                return new Usuario (false) {
                    id_acceso = id_acceso,
                    nombre_acceso = nombre_acceso,
                    estado = estado,
                    accesos = lista
                };

            }
            catch {
                return null;
            }
        }



    }
}
