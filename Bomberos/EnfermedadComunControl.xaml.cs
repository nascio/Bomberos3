using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace Bomberos {

    /// <summary>
    ///     Lógica de interacción para EnfermedadComunControl.xaml 
    /// </summary>
    public partial class EnfermedadComunControl : UserControl {

        BLL.Modelo1 modelo;

        public EnfermedadComunControl ( ) {
            InitializeComponent ( );
            // this.DataContext = new Modelo1 ( );

            this.modelo = this.DataContext as BLL.Modelo1;

            DispatcherTimer d = new DispatcherTimer (DispatcherPriority.Normal, this.Dispatcher);
            d.Tick += this.D_Tick;
            d.Interval = TimeSpan.FromSeconds (1);
            d.Start ( );

        }

        private void D_Tick (Object sender, EventArgs e) {
            var s = sender as DispatcherTimer;
            s.Stop ( );

            this.modelo.Inicializar ( );
        }

        private void Image_MouseEnter (Object sender, MouseEventArgs e) {
            var s = this.DataContext as BLL.Modelo1;
            var n = s;
        }

        private void Button_Click (Object sender, RoutedEventArgs e) {
            if (!this.modelo.Aceptar ( )) {
                MessageBox.Show ("Hay errores");
                return;
            }


            var parent = this.Parent as Grid;
            parent.Children.Clear ( );
            parent.Children.Add (new EnfermedadComunControl ( ) {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            });

        }
    }
}