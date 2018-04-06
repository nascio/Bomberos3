using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL {

    public class InformeEnfermedadComun {

        #region Atributos

        private static string[] nn = new string[] { "Sede 19 Cia ciudad", "San Isidro Chamac San Pedro Sac", "San Isidro Chamac San Pedro Sac", "4ª calle 2-24 zona 4 ciudad", "Sede 19 Cia ciudad", "12av zona 1 San Pedro sac", "Final 6ª calle 11-45 zona2 ciudad", "1ª av. D 7-69 zona 4 ciudad", "10av. 7-85 zona 1 ciudad", "Aldea San Aldea SAn  zona 1 ciudadc derechaJose Caben San Pedro Sac.", "7ª av. Callejón 1 zona 2 ciudad", "Aldea Candelaria Siquibal San Antonio Sac", "Aldea Champollap San Pedro Sac", "3ª calle y 4ª av. Zona 1 ciudad", "Sector las rosas zona 4 ciudad", "Sede 19 cia ciudad", "Calzada Siriaco Soto zona 3 ciudad", "Canton Ojo de Agua San Andres Ciudad", "3ª calle 3-69 zona 3 ciudad", "Sede 19 Cia ciudad", "Sede 19 Cia ciudad", "4ª calle 2-24 zona 3 ciudad", "3ª calle zona 3 ciudad", "3ª av. 9ª calle zona 1 ciudad", };
        private string causa;
        private int count = 0;
        private int dia;
        private int edad;
        private string fallecido;
        private string hora;
        private string lugar;

        private string sexo;

        private string vivo;

        #endregion

        #region fecha

        public String Fecha {
            get {
                return this.dia.ToString ("00");
            }
        }

        public String Hora {
            get {
                return this.hora;
            }
        }

        #endregion

        #region cantidad

        public String Cantidad {
            get {
                return count.ToString ( );
            }
        }

        public string Lugar {
            get {
                return this.lugar;
            }
        }

        public string Sexo {
            get {
                return this.sexo;
            }
        }

        #endregion

        #region edad

        public string Edad {
            get {
                return this.edad.ToString ( );
            }
        }

        #endregion

        #region Propiedades

        public string Causa {
            get {
                return this.causa;
            }
        }

        public string Fallecido {
            get {
                return this.fallecido;
            }
        }

        public string Vivo {
            get {
                return this.vivo;
            }
        }

        #endregion

        #region Metodos

        public static List<InformeEnfermedadComun> ObtenerPorFecha (int anio, int mes) {
            using (var sql = new DAL.DataSet2TableAdapters.enfermedad_comunTableAdapter ( )) {
                Random r = new Random ( );
                var xcount = 0;

                var t = sql.GetData (2, anio, mes).Select (fila => {
                    return new InformeEnfermedadComun ( ) {
                        count = ++xcount,
                        dia = fila.fecha.Day,
                        causa = fila.nombre,
                        edad = fila.edad,
                        fallecido = String.Compare (fila.estado, EstadoPaciente.Fallecido.ToString ( )) == 0 ? "X" : "",
                        sexo = fila.sexo == 0 ? "F" : "M",
                        lugar = nn[r.Next (nn.Length)],
                        vivo = String.Compare (fila.estado, EstadoPaciente.Vivo.ToString ( )) == 0 ? "X" : "",
                        hora = string.Format ("{0:00}:{1:00}", fila.hora_entrada.Hours, fila.hora_entrada.Minutes)
                    };
                });

                return t.ToList ( );
            }
        }

        #endregion
    }

    public class InformeMaterniadad {
        #region Atributos

        private static string[] nn = new string[] { "Sede 19 Cia ciudad", "San Isidro Chamac San Pedro Sac", "San Isidro Chamac San Pedro Sac", "4ª calle 2-24 zona 4 ciudad", "Sede 19 Cia ciudad", "12av zona 1 San Pedro sac", "Final 6ª calle 11-45 zona2 ciudad", "1ª av. D 7-69 zona 4 ciudad", "10av. 7-85 zona 1 ciudad", "Aldea San Aldea SAn  zona 1 ciudadc derechaJose Caben San Pedro Sac.", "7ª av. Callejón 1 zona 2 ciudad", "Aldea Candelaria Siquibal San Antonio Sac", "Aldea Champollap San Pedro Sac", "3ª calle y 4ª av. Zona 1 ciudad", "Sector las rosas zona 4 ciudad", "Sede 19 cia ciudad", "Calzada Siriaco Soto zona 3 ciudad", "Canton Ojo de Agua San Andres Ciudad", "3ª calle 3-69 zona 3 ciudad", "Sede 19 Cia ciudad", "Sede 19 Cia ciudad", "4ª calle 2-24 zona 3 ciudad", "3ª calle zona 3 ciudad", "3ª av. 9ª calle zona 1 ciudad", };
        private string aborto;
        private string antecion_de_parto;
        private string traslados_a_Hospitales;
        private int count = 0;
        private int dia;
        private int edad;
        private string fallecido;
        private string hora;
        private string lugar;
        private string vivo;

        #endregion

        #region fecha

        public String Fecha {
            get {
                return this.dia.ToString ("00");
            }
        }

        public String Hora {
            get {
                return this.hora;
            }
        }

        #endregion

        #region cantidad

        public String Cantidad {
            get {
                return count.ToString ( );
            }
        }

        public string Lugar {
            get {
                return this.lugar;
            }
        }

        #endregion

        #region edad

        public string Edad {
            get {
                return this.edad.ToString ( );
            }
        }

        #endregion

        #region Propiedades

        public string Aborto {
            get {
                return this.aborto;
            }
        }

        public string Atencion_de_parto {
            get {
                return this.antecion_de_parto;
            }
        }

        public string Traslados_a_Hospitales {
            get {
                return this.traslados_a_Hospitales;
            }
        }
        #endregion



        #region


        public string Fallecido {
            get {
                return this.fallecido;
            }
        }

        public string Vivo {
            get {
                return this.vivo;
            }
        }

        #endregion

        #region Metodos

        public static List<InformeMaterniadad> ObtenerPorFecha (int anio, int mes) {
            using (var sql = new DAL.DataSet2TableAdapters.maternidadTableAdapter ( )) {
                Random r = new Random ( );
                var xcount = 0;

                var t = sql.GetData (4, anio, mes).Select (fila => {
                    return new InformeMaterniadad ( ) {
                        count = ++xcount,
                        antecion_de_parto = fila.IspartoNull ( ) || !Convert.ToBoolean (fila.parto) ? "" : "X",
                        traslados_a_Hospitales = "Hospital San Marcos",
                        dia = fila.fecha.Day,
                        aborto = fila.IsabortoNull ( ) || !Convert.ToBoolean (fila.aborto) ? "" : "X",
                        edad = fila.edad,
                        fallecido = String.Compare (fila.estado, EstadoPaciente.Fallecido.ToString ( )) == 0 ? "X" : "",
                        lugar = nn[r.Next (nn.Length)],
                        vivo = String.Compare (fila.estado, EstadoPaciente.Vivo.ToString ( )) == 0 ? "X" : "",
                        hora = string.Format ("{0:00}:{1:00}", fila.hora_entrada.Hours, fila.hora_entrada.Minutes)
                    };
                });

                return t.ToList ( );
            }
        }

        #endregion

    }




    public class MVInforme : BLL.Modelo.MVPrincipal {

        #region Atributos

        private int anio;
        private List<int> anios;


        private string mes;

        private List<string> meses;

        #endregion

        #region Constructores

        public MVInforme ( ) {
            var now = DateTime.Now;
            this.anio = now.Year;
            this.anios = new List<int> ( );
            for (int i = 0; i < 5; i++) {
                this.anios.Add (this.anio - i);
            }

            this.meses = new List<string> ( ) { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Agosto", "Octubre", "Noviembre", "Diciembre" };
            this.mes = this.meses[now.Month - 1];
        }

        #endregion

        #region Propiedades

        public int Anio {
            get {
                return this.anio;
            }
            set {
                this.anio = value;
                base.OnPropertyChanged ( );
            }
        }

        public TipoIncidente Incidente {
            get; set;
        }

        public List<TipoIncidente> Incidentes {
            get {
                return TipoIncidente.Incidentes ( );
            }
        }

        public List<int> Anios {
            get {
                return this.anios;
            }
        }



        private IEnumerable valor;
        public IEnumerable InformeEnfermedad {
            get {
                return valor;
            }
        }

        public string Mes {
            get {
                return this.mes;
            }
            set {
                this.mes = value;
                base.OnPropertyChanged ( );
            }
        }

        public List<String> Meses {
            get {
                return this.meses;
            }
        }



        #endregion

        #region Metodos





        public void GenerarInformeComun ( ) {
            var mess = this.meses.IndexOf (this.mes) + 1;

            switch (Incidente == null ? -1 : Incidente.ID) {
                case 2:
                    valor = InformeEnfermedadComun.ObtenerPorFecha (this.anio, mess);
                    break;

                case 4:
                    valor = InformeMaterniadad.ObtenerPorFecha (this.anio, mess);

                    break;
            }
            base.OnPropertyChanged ("InformeEnfermedad");
        }


        public void GenerarPDF ( ) {

            switch (Incidente == null ? -1 : Incidente.ID) {
                case 2:
                    CrearPDFInformeComun ( );
                    break;

                case 4:
                    CrearPDFMaternidad ( );

                    break;
            }
        }



        public void CrearPDFInformeComun ( ) {

            var list = InformeEnfermedadComun.ObtenerPorFecha (this.anio, this.meses.IndexOf (this.mes) + 1);


            Bombero director = new Bombero ( );
            director.Nombre = "Rene Anselmo";
            director.Apellido = "Perez Perez";

            Bombero secretario = new Bombero ( );
            secretario.Nombre = "Juan Luis";
            secretario.Apellido = "Paz Paz";
            string path = Directory.GetCurrentDirectory ( );
            string ubicacion = path + "/prueba.pdf";

            var pdf = new PDF ( );
            pdf.Crear1Doc (ubicacion);

            pdf.Crear2Titulo ("Enfermedad comun", this.mes, this.anio);
            var t = pdf.Crear3EncabezadoTabla (typeof (InformeEnfermedadComun).GetProperties ( ).Select (x => x.Name).ToArray ( ));
            foreach (var item in list) {
                pdf.Crear4CuerpoTabla (t, item.Fecha, item.Hora, item.Cantidad, item.Lugar, item.Sexo, item.Edad, item.Causa, item.Fallecido, item.Vivo);
            }
            pdf.Crear5PiePagina (t, director, secretario);
            pdf.Cerrar6 ( );

            Process.Start ("explorer.exe", path);

        }

        public void CrearPDFMaternidad ( ) {

            var list = InformeMaterniadad.ObtenerPorFecha (this.anio, this.meses.IndexOf (this.mes) + 1);


            Bombero director = new Bombero ( );
            director.Nombre = "Rene Anselmo";
            director.Apellido = "Perez Perez";

            Bombero secretario = new Bombero ( );
            secretario.Nombre = "Juan Luis";
            secretario.Apellido = "Paz Paz";
            string path = Directory.GetCurrentDirectory ( );
            string ubicacion = path + "/prueba.pdf";

            var pdf = new PDF ( );
            pdf.Crear1Doc (ubicacion);

            pdf.Crear2Titulo ("Maternidad", this.mes, this.anio);
            var t = pdf.Crear3EncabezadoTabla (typeof (InformeMaterniadad).GetProperties ( ).Select (x => x.Name).ToArray ( ));
            foreach (var item in list) {
                pdf.Crear4CuerpoTabla (t, item.Fecha, item.Hora, item.Cantidad, item.Lugar, item.Edad, item.Aborto, item.Atencion_de_parto, item.Traslados_a_Hospitales, item.Fallecido, item.Vivo);
            }
            pdf.Crear5PiePagina (t, director, secretario);
            pdf.Cerrar6 ( );

            Process.Start ("explorer.exe", path);

        }



        #endregion
    }


    public class PDF {

        int centrado = Image.ALIGN_CENTER;
        iTextSharp.text.Font _contenidoTabla = new iTextSharp.text.Font (iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        iTextSharp.text.Font _titulo = new iTextSharp.text.Font (iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        iTextSharp.text.Font _tituloTabla = new iTextSharp.text.Font (iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        Document doc;
        PdfWriter writer;




        public void Crear1Doc (string ubicacion) {
            this.doc = new Document ( );
            writer = PdfWriter.GetInstance (doc, new FileStream (@ubicacion, FileMode.Create));
            doc.AddTitle ("Informe");
            doc.SetPageSize (iTextSharp.text.PageSize.LETTER.Rotate ( ));
            doc.Open ( );
        }


        public PdfPTable Crear3EncabezadoTabla (params string[] titulos) {


            PdfPTable tblIncidente = new PdfPTable (titulos.Length);
            tblIncidente.WidthPercentage = 100;

            //celdas
            //Titulo de las Celdas
            foreach (var titulo in titulos) {
                tblIncidente.AddCell (new PdfPCell (new Phrase (titulo, this._tituloTabla)) {
                    HorizontalAlignment = this.centrado
                });
            }

            return tblIncidente;

            //celdas
            //Titulo de las Celdas
            //PdfPCell clFecha = new PdfPCell (new Phrase ("Fecha", _tituloTabla));
            //clFecha.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clFecha);

            //PdfPCell clHora = new PdfPCell (new Phrase ("Hora", _tituloTabla));
            //clHora.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clHora);
            //PdfPCell clCantidad = new PdfPCell (new Phrase ("Cantidad", _tituloTabla));
            //clCantidad.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clCantidad);
            //PdfPCell clLugar = new PdfPCell (new Phrase ("Lugar", _tituloTabla));
            //clLugar.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clLugar);
            //PdfPCell clNombre = new PdfPCell (new Phrase ("Nombre", _tituloTabla));
            //clNombre.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clNombre);
            //PdfPCell clEdad = new PdfPCell (new Phrase ("Edad", _tituloTabla));
            //clEdad.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clEdad);
            //PdfPCell clSexo = new PdfPCell (new Phrase ("Sexo", _tituloTabla));
            //clSexo.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clSexo);
            //PdfPCell clVivo = new PdfPCell (new Phrase ("Vivo", _tituloTabla));
            //clVivo.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clVivo);
            //PdfPCell clFallecido = new PdfPCell (new Phrase ("Fallecido", _tituloTabla));
            //clFallecido.HorizontalAlignment = centrado;
            //tblIncidente.AddCell (clFallecido);



        }



        public void Crear4CuerpoTabla (PdfPTable tblIncidente, params string[] valores) {
            foreach (var valor in valores) {
                tblIncidente.AddCell (new PdfPCell (new Phrase (valor, this._contenidoTabla)) {
                    HorizontalAlignment = this.centrado
                });
            }
        }






        public void Crear2Titulo (string evento, string mes, int anio) {
            //titulo 
            this.doc.Add (new Paragraph ("Benemerito Comite de Bomberos Voluntarios", this._titulo) {
                Alignment = this.centrado
            });


            this.doc.Add (new Paragraph ("19a. Compañia de Bomberos Voluntarios", this._titulo) {
                Alignment = this.centrado
            });

            this.doc.Add (new Paragraph ("Estadisticas de " + evento + " Mensuales ", this._titulo) {
                Alignment = this.centrado
            });

            this.doc.Add (new Paragraph ("Correspondiente al mes de " + mes + " del " + anio, this._titulo) {
                Alignment = this.centrado
            });

            this.doc.Add (Chunk.NEWLINE);

        }


        public void Crear5PiePagina (PdfPTable tblIncidente, Bombero director, Bombero secretario) {

            doc.Add (tblIncidente);
            doc.Add (Chunk.NEWLINE);



            //Firmas
            PdfPTable tblFirmas = new PdfPTable (2);

            PdfPCell clDirecto = new PdfPCell (new Phrase (director.NombreCompleto, this._tituloTabla)) {
                HorizontalAlignment = this.centrado,
                Border = 0
            };
            PdfPCell clSecretario = new PdfPCell (new Phrase (secretario.NombreCompleto, this._tituloTabla)) {
                HorizontalAlignment = this.centrado,
                Border = 0
            };
            tblFirmas.AddCell (clDirecto);
            tblFirmas.AddCell (clSecretario);


            clDirecto = new PdfPCell (new Phrase ("Directo", _tituloTabla)) {
                HorizontalAlignment = this.centrado,
                Border = 0
            };
            clSecretario = new PdfPCell (new Phrase ("Secretario", _tituloTabla)) {
                HorizontalAlignment = this.centrado,
                Border = 0
            };
            tblFirmas.AddCell (clDirecto);
            tblFirmas.AddCell (clSecretario);

            this.doc.Add (tblFirmas);
        }

        public void Cerrar6 ( ) {
            this.doc.Close ( );
            this.writer.Close ( );
        }





    }



}