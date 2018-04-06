using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Bomberos.DAL.Informes
{
   public class ElementoIncendio
    {
        private String _fecha;

        public String fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private String _hora;

        public String hora
        {
            get { return _hora; }
            set { _hora = value; }
        }

        private String _cantidad;

        public String cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private String _lugar;

        public String lugar
        {
            get { return _lugar; }
            set { _lugar = value; }
        }

        private String _propietario;

        public String propietario
        {
            get { return _propietario; }
            set { _propietario = value; }
        }

        private String _causa;

        public String causa
        {
            get { return _causa; }
            set { _causa = value; }
        }
        private String _perdidas;

        public String perdidas
        {
            get { return _perdidas; }
            set { _perdidas = value; }
        }

        private String _aguaUtilizada;

        public String aguaUtilizada
        {
            get { return _aguaUtilizada; }
            set { _aguaUtilizada = value; }
        }

        public List<ElementoIncendio> obtenerElementos(DateTime fechaInicio, DateTime fechaFinal)
        {
            List<ElementoIncendio> elementosIncendio = new List<ElementoIncendio>();

            MySqlConnection con = ConexionABD.conectar();
            String consulta = "SELECT  DATE_FORMAT(fecha,'%d-%m-%Y'), horaSalida,  causa, perdidas, aguaUtilizada, propietario, direccion FROM lugar INNER JOIN " +
                                "(SELECT  fecha, horaSalida,  causa, perdidas, aguaUtilizada, propietario, Lugar_idLugar FROM viajes INNER JOIN "+
                                "(SELECT fecha, horaSalida, idReporte_incidente, causa, perdidas, aguaUtilizada, propietario FROM incendio INNER JOIN "+
                                "(SELECT fecha, horaSalida, idReporte_incidente, Incendio_idIncendio from reporte_incidente where tipoIncidente_idtipoIncidente = 18 and fecha between'" + fechaInicio.ToString("yyyy-MM-dd") + "' and  '" + fechaFinal.ToString("yyyy-MM-dd") + "') as informeIncidente " +
                                "where Incendio_idIncendio = idIncendio) as InformeIncendio "+
                               " WHERE  idReporte_incidente = Reporte_incidente_idReporte_incidente) as informeLugarIncendio "+
                               " WHERE Lugar_idLugar = idLugar; ";
            Console.Write(consulta);
            MySqlCommand comando = new MySqlCommand(string.Format(consulta), con);
            IDataReader data = comando.ExecuteReader();
            if (data != null)
            {
                while (data.Read())
                {
                    ElementoIncendio elemento = new ElementoIncendio();
                    elemento.fecha = data.GetString(0);
                    elemento.hora = data.GetString(1);
                    elemento.causa = data.GetString(2);
                    elemento.cantidad = "1";
                    elemento.perdidas = data.GetString(3);
                    elemento.aguaUtilizada = data.GetString(4);
                    elemento.propietario = data.GetString(5) ;
                    elemento.lugar= data.GetString(6);
                    elementosIncendio.Add(elemento);
                }
                ElementoIncendio elemento1 = new ElementoIncendio();
                elemento1.fecha = "";
                elemento1.hora = "Total";
                elemento1.cantidad = elementosIncendio.Count.ToString();
                elementosIncendio.Add(elemento1);
            }
            return elementosIncendio;
        }
    }
}
