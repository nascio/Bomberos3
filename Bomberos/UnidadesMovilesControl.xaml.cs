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
using Bomberos.BLL;

namespace Bomberos {
    /// <summary>
    /// Lógica de interacción para UnidadesMovilesControl.xaml
    /// </summary>
    public partial class UnidadesMovilesControl : UserControl, IErrorMsg {

        BLL.MVUnidadMovil modelo;

        public UnidadesMovilesControl ( ) {
            InitializeComponent ( );
            this.modelo = this.DataContext as BLL.MVUnidadMovil;
            this.modelo.ErrorMSG = this;
        }

        private void btnNuevo_Click (Object sender, RoutedEventArgs e) {
            this.modelo.Nuevo ( );
            this.grdNuevoContenido.Children.Add (this.FindResource ("nuevoKey") as UIElement);
            this.grdNuevoContenido.Visibility = Visibility.Visible;

        }

        private void btnEditar_Click (Object sender, RoutedEventArgs e) {
            this.modelo.Editar ( );
            this.grdNuevoContenido.Children.Add (this.FindResource ("editarKey") as UIElement);
            this.grdNuevoContenido.Visibility = Visibility.Visible;
        }

        private void cerrar_Click (Object sender, MouseButtonEventArgs e) {

        }

        public void ErrorEnActualizar (ErrorMsgArgs e) => throw new NotImplementedException ( );
        public void ErrorEnEliminar (ErrorMsgArgs e) => throw new NotImplementedException ( );
        public void ErrorEnEliminarNoSePuede (ErrorMsgArgs e) => throw new NotImplementedException ( );
        public void ErrorPorMensaje (Boolean bueno, String mensaje) {
            Console.WriteLine (mensaje);
        }

        public void Quitar ( ) {
            this.grdNuevoContenido.Children.Clear ( );
            this.grdNuevoContenido.Visibility = Visibility.Collapsed;
        }

        public void AccionHiloPrincipal (Action accion) {
            var naccion = (Action) (( ) => {
                if (accion != null) {
                    accion ( );

                }
                this.Quitar ( );
            });
            App.Current.Dispatcher.BeginInvoke (System.Windows.Threading.DispatcherPriority.Normal, naccion);
        }
    }
}
