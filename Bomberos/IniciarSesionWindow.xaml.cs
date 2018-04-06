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
using System.Windows.Shapes;

namespace Bomberos {

    /// <summary>
    ///     Lógica de interacción para IniciarSesionWindow.xaml 
    /// </summary>
    public partial class IniciarSesionWindow : Window {
        private BLL.Bombero bombero;
        private int conteo = 0;

        public IniciarSesionWindow ( ) {
            InitializeComponent ( );
        }

        public BLL.Bombero BomberoActivo {
            get {
                return this.bombero;
            }
        }

        private void btnAtras_Click (Object sender, RoutedEventArgs e) {
            this.grdContra.Visibility = Visibility.Collapsed;

            this.txtContra.Password = String.Empty;
            this.lblTextoErrorContra.Visibility = Visibility.Hidden;

            this.grdUsuario.Visibility = Visibility.Visible;
        }

        private void btnIniciarSesion_Click (Object sender, RoutedEventArgs e) {
            var username = this.lblUsuario.Text;
            var contra = this.txtContra.Password;

            var c = BLL.Usuario.ComprobarInicioSesion (username, contra);

            if (c) {
                this.lblTextoErrorContra.Visibility = Visibility.Hidden;

                try {
                    this.bombero = BLL.Bombero.BuscarPorUsuario (username);

                    this.DialogResult = true;
                }
                catch {
                    mostrarError ( );
                }
            }
            else {
                mostrarError ( );
            }
        }

        private void btnSiguiente_Click (Object sender, RoutedEventArgs e) {
            var username = this.txtUsuario.Text;

            var c = BLL.Usuario.ComprobarInicioSesion (username);

            if (c) {
                this.grdUsuario.Visibility = Visibility.Collapsed;
                this.lblTextoErrorUsuario.Visibility = Visibility.Hidden;

                this.lblUsuario.Text = username;
                FocusManager.SetFocusedElement (this, this.txtContra);

                this.grdContra.Visibility = Visibility.Visible;
            }
            else {
                this.lblTextoErrorUsuario.Visibility = Visibility.Visible;
            }
        }

        private void mostrarError ( ) {
            this.lblTextoErrorContra.Visibility = Visibility.Visible;

            this.conteo++;
            if (this.conteo >= 3) {
                MessageBox.Show (this, "Demasiados intentos.\nCerrando la aplicacion.", "Bomberos - App", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown (-1);
            }
        }
    }
}