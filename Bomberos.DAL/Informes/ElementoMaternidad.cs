using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.DAL.Informes
{
   public class ElementoMaternidad
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

        private String _hospitalDeTraslado;

        public String hospitalDeTraslado
        {
            get { return _hospitalDeTraslado; }
            set { _hospitalDeTraslado = value; }
        }

        private String _edad;

        public String edad
        {
            get { return _edad; }
            set { _edad = value; }
        }

        private String _atencionDeParto;

        public String atencionDeParto
        {
            get { return _atencionDeParto; }
            set { _atencionDeParto = value; }
        }

        private String _vivo;

        public String vivo
        {
            get { return _vivo; }
            set { _vivo = value; }
        }

        private String _fallecido;

        public String fallecido
        {
            get { return _fallecido; }
            set { _fallecido = value; }
        }
        private String _aborto;

        public String aborto
        {
            get { return _aborto; }
            set { _aborto = value; }
        }

        public List<ElementoMaternidad> seleccionarElementosMaternidad(DateTime fechaInicio, DateTime fechaFinal)
        {
            List<ElementoMaternidad> elementosMaternidad = new List<ElementoMaternidad>();
            MySqlConnection con = ConexionABD.conectar();
            String consulta = "SELECT DATE_FORMAT(fecha,'%d-%m-%Y'), horaSalida,  estado, edad, direccionPaciente, atencionAlParto,Aborto,  nombreLugar, nombre FROM tipolugar INNER JOIN " +
                            "(SELECT  fecha, horaSalida, estado, edad,  IncidenteIdLugar.direccion AS direccionPaciente, atencionAlParto, Aborto, Lugar.direccion as nombreLugar, TipoLugar_id FROM Lugar INNER JOIN "+
                            "(SELECT fecha, horaSalida, estado, edad,  direccion, atencionAlParto, Aborto, Lugar_idLugar FROM viajes INNER JOIN "+
                            "(SELECT fecha, horaSalida, idReporte_incidente, estado, edad,  direccion, atencionAlParto, Aborto from maternidad INNER JOIN "+
                            "(SELECT  fecha, horaSalida, idReporte_incidente, estado, edad,  Maternidad_id, direccion FROM domicilio INNER JOIN "+
                            "(SELECT  fecha, horaSalida, Paciente_idPaciente as idPaciente, idReporte_incidente, estado, edad,  Maternidad_id FROM paciente INNER JOIN "+
                            "(SELECT fecha, horaSalida, Paciente_idPaciente, idReporte_incidente from reporte_incidente where tipoIncidente_idtipoIncidente = 4 and fecha between '"+ fechaInicio.ToString("yyyy-MM-dd") + "' and  '"+ fechaFinal.ToString("yyyy-MM-dd") +"') as incidenteInforme " +
                            " WHERE  Paciente_idPaciente = idPaciente) as paciente"+
                            " WHERE paciente.idPaciente = domicilio.Paciente_idPaciente) as direcionPaciente"+
                            " WHERE Maternidad_id = id) as pacienteMaternidad"+
                            " WHERE idReporte_incidente = Reporte_incidente_idReporte_incidente) as IncidenteIdLugar"+
                            " WHERE Lugar_idLugar = idLugar) as LugarIncidente"+
                            " WHERE TipoLugar_id = id; ";
            Console.Write(consulta);
            MySqlCommand comando = new MySqlCommand(string.Format(consulta), con);
            IDataReader data = comando.ExecuteReader();
            if (data != null)
            {
                while (data.Read())
                {
                    ElementoMaternidad elemento = new ElementoMaternidad();
                    elemento.fecha = data.GetString(0);
                    elemento.hora = data.GetString(1);
                    
                    elemento.cantidad = "1";
                    elemento.edad = data.GetString(3);
                    elemento.lugar = data.GetString(4);
                    if (data.GetString(5) == "True")
                    {
                        elemento.atencionDeParto = "X";
                    }
                    if (data.GetString(6) == "True")
                    {
                        elemento.aborto = "X";
                        elemento.fallecido = "X";
                    }
                    else
                    {
                        if (data.GetString(2) == "viva")
                        {
                            elemento.vivo = "X";
                        }
                        else
                        {
                            elemento.fallecido = "X";
                        }
                    }
                    elemento.hospitalDeTraslado = data.GetString(7)+" " + data.GetString(8);
                    elementosMaternidad.Add(elemento);
                }
                ElementoMaternidad elemento1 = new ElementoMaternidad();
                elemento1.fecha = "";
                elemento1.hora = "Total";
                elemento1.cantidad = elementosMaternidad.Count.ToString();
                elementosMaternidad.Add(elemento1);
            }
            
          

            return elementosMaternidad;

           
        }


    }
}
