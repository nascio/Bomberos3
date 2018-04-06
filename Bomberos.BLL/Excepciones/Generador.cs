using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL.Excepciones {

    internal static class Generador {

        public static Exception GenerarDesdeMySqlException (MySqlException ex) {
            switch (ex.Number) {
                case 1042:
                    return new SinConexionException (ex);

                case 1062:
                    return new ElementoDuplicadoException (ex);

                case 1451:
                    return new NoSePuedeEliminarException ("porque existe una referencia.", ex);

                default:
                    Console.WriteLine (ex.Number);
                    Console.WriteLine (ex.ToString ( ));
                    return null;
            }
        }
    }
}