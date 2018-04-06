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

namespace Bomberos {
    /// <summary>
    /// Lógica de interacción para ReporteControl.xaml 
    /// </summary>
    public partial class ReporteControl : UserControl {
        public ReporteControl ( ) {
            InitializeComponent ( );
        }

        private void Button_Click (Object sender, RoutedEventArgs e) {
            (this.DataContext as BLL.MVInforme).GenerarInformeComun ( );
        }

        private void generar_Click (Object sender, RoutedEventArgs e) {
            (this.DataContext as BLL.MVInforme).GenerarPDF ( );

        }
    }




}
