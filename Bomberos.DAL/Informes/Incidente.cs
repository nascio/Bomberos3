using Bomberos.DAL.Informes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bomberos.DAL.Informes
{
    public class Incidente
    {
        private int _incidente;

        public int idincidente
        {
            get { return _incidente; }
            set { _incidente = value; }
        }
        private String _nombre;

        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public List<Incidente> obtenerIncidentes()
        {
            List<Incidente> incidentes = new List<Incidente>();
           MySqlConnection con = ConexionABD.conectar();
            String consulta =" SELECT * FROM bitacorabomberos.tipoincidente; ";
            Console.WriteLine(consulta);
            MySqlCommand comando = new MySqlCommand(string.Format(consulta), con);
            IDataReader data = comando.ExecuteReader();
            if (data != null)
            {
                while (data.Read())
                {
                    Incidente elemento = new Incidente();
                    elemento.idincidente = data.GetInt32(0);
                    elemento.nombre = data.GetString(1);
                    incidentes.Add(elemento);
                }
            }
            return incidentes;
        }

        public Incidente obtenerIncidente(int id)
        {
            Incidente incidente = new Incidente();
            MySqlConnection con = ConexionABD.conectar();
            String consulta = " SELECT * FROM bitacorabomberos.tipoincidente where idtipoIncidente ="+id.ToString()+"; ";
            MySqlCommand comando = new MySqlCommand(string.Format(consulta), con);
            IDataReader data = comando.ExecuteReader();
            if (data != null)
            {
                while (data.Read())
                {
                    incidente.idincidente = data.GetInt32(0);
                    incidente.nombre = data.GetString(1);
                }
            }

            return incidente;
        }

    }
}
