using Bomberos.DAL.Informes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Bomberos
{
    /// <summary>
    /// Lógica de interacción para Informes.xaml
    /// </summary>
    public partial class Informes : UserControl
    {
        private List<Incidente> _incidentes;

        private List<Incidente> incidentes
        {
            get { return _incidentes; }
            set { _incidentes = value; }
        }

        public Informes()
        {
            InitializeComponent();
        }

        private void btGenerarInfome_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaInicio = Convert.ToDateTime(this.calInicio.SelectedDate);
            DateTime fechaFinal = Convert.ToDateTime(this.calFinal.SelectedDate);
            var informe = new BLL.Informes.Informe();
            if (fechaInicio <= fechaFinal)
            {
                try
                {
                    int itemSeleccionado = cbIncidentes.SelectedIndex;
                    int id = incidentes[itemSeleccionado].idincidente;

                    informe.crearInforme(id, fechaInicio, fechaFinal);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("No seleccionado incidente", "Alert");
                }



            }
            else
            {
                MessageBox.Show("Fechas Incorrectas", "Alert");
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Incidente incidente = new Incidente();
            this.incidentes = incidente.obtenerIncidentes();
            foreach (Incidente elemento in incidentes)
            {
                this.cbIncidentes.Items.Add(elemento.nombre);

            }

        }
    }
}
