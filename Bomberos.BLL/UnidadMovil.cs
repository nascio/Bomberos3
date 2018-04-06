using Bomberos.BLL.Excepciones;
using Bomberos.DAL.DataSet1TableAdapters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL {

    public class UnidadMovil : Notificador {

        #region Atributos

        private UnidadMovil copia;
        private bool estado;
        private int id;
        private int numero;
        private string placa;

        #endregion

        #region Propiedades

        public bool Estado {
            get {
                return this.estado;
            }
            set {
                this.estado = value;
                base.OnPropertyChanged ( );
            }
        }

        public int ID {
            get {
                return this.id;
            }
            set {
                this.id = value;
                base.OnPropertyChanged ( );
            }
        }

        public int Numero {
            get {
                return this.numero;
            }
            set {
                this.numero = value;
                base.OnPropertyChanged ( );
            }
        }

        public string Placa {
            get {
                return this.placa;
            }
            set {
                this.placa = value;
                base.OnPropertyChanged ( );
            }
        }

        #endregion

        #region Metodos

        public static List<UnidadMovil> ObtenerUnidadesMoviles ( ) {
            try {
                using (var consulta = new DAL.DataSet1TableAdapters.unidad_movilTableAdapter ( )) {
                    var tabla = consulta.GetData ( );

                    return tabla.Select (fila => {
                        var r = new UnidadMovil ( ) {
                            id = fila.id_unidad,
                            numero = fila.Isnumero_unidadNull ( ) ? 0 : fila.numero_unidad,
                            placa = fila.IsplacaNull ( ) ? String.Empty : fila.placa,
                            estado = true
                        };
                        r.copia = r.MemberwiseClone ( ) as UnidadMovil;
                        return r;
                    }).ToList ( );
                }
            }
            catch (Exception ex) {
                Console.WriteLine (ex.ToString ( ));
                return null;
            }
        }

        public void Editar (int id_redactor) {
            try {
                using (var r = new insertarTableAdapter ( )) {
                    using (var result = r.sp_unidad_movil (2, this.id, IF (this.numero, copia.numero), IFC (this.placa, copia.placa), IF (this.estado, copia.estado), id_redactor)) {
                        var row = result.First ( );

                        if (row.out_status != 1200) {
                            throw new NoSePuedeEliminarException ("el tipo de incidente.");
                        }

                        //this.id = -1;
                        this.copia = base.MemberwiseClone ( ) as UnidadMovil;
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
                using (var s = r.sp_unidad_movil (1, null, this.numero, this.placa, this.estado, id_redactor)) {
                    var ss = s.First ( );

                    if (ss.out_status != 1) {
                        throw new NoSePudoIngresarException ("");
                    }

                    this.copia = base.MemberwiseClone ( ) as UnidadMovil;
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