using Bomberos.BLL.Excepciones;
using Bomberos.DAL.DataSet1TableAdapters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL {

    public class Acceso {

        #region Atributos

        private int id;

        private string nombre;

        #endregion

        #region Propiedades

        public int ID {
            get {
                return this.id;
            }
        }

        public string Nombre {
            get {
                return this.nombre;
            }
        }

        #endregion

        #region Metodos

        public static List<Acceso> Incidentes ( ) {
            var s = null as List<Acceso>;

            try {
                using (var r = new accesoTableAdapter ( )) {
                    s = r.GetData ( ).Select (x => {
                        var n = new Acceso ( ) {
                            nombre = x.nombre,
                            id = x.id_acceso
                        };
                        //n.copia = n.MemberwiseClone ( ) as TipoIncidente;

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

        #endregion
    }
}