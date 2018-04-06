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

namespace Bomberos {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {


        class menuContext {

            public String Llave {
                get; set;
            }

            public bool Quitar = true;

            public String Nombre {
                get; set;
            }

            public List<menuContext> SubMenu {
                get; set;
            }

            public ICommand Click {
                get; set;
            }

            public String GestosDeIntrada {
                get; set;
            }
        }

        class N {
            public N ( ) {
                this.Menu = new List<menuContext> ( );
            }

            public List<menuContext> Menu {
                get; set;
            }
        }

        menuContext inicioSesion;
        List<menuContext> restos;
        ObservableCollection<menuContext> menugenial;

        BLL.Bombero bombero;



        public MainWindow ( ) {
            this.inicioSesion = new menuContext ( ) {
                Nombre = "Iniciar sesión",
                Click = new BLL.Comando (( ) => mnuitIniciarSesion_Click (null, null))
            };
            this.restos = new List<menuContext> ( );
            this.menugenial = new ObservableCollection<menuContext> ( );


            this.restos.Add (new menuContext ( ) {
                Nombre = "Servicio enfermedad comun",
                Llave = "reporte",
                Click = new BLL.Comando (( ) => servicioEnfermedadComun_Click ( ))
            });
            this.restos.Add (new menuContext ( ) {
                Nombre = "Maternidad",
                Llave = "reporte",
                Click = new BLL.Comando (( ) => materniadad_Click ( ))
            });
            this.restos.Add (new menuContext ( ) {
                Nombre = "Registro de unidades moviles",
                Llave = "registros",
                Click = new BLL.Comando (( ) => registroUnidadesMoviles_Click ( ))
            });
            this.restos.Add (new menuContext ( ) {
                Nombre = "Registro de bomberos",
                Llave = "registros",
                Click = new BLL.Comando (( ) => registroBomberos_Click ( ))
            });

            this.restos.Add (new menuContext ( ) {
                Nombre = "Generacion de informes",
                Llave = "informes",
                Click = new BLL.Comando (( ) => generacionDeInformes_Click ( ))
            });


            this.restos.Add (new menuContext ( ) {
                Quitar = false,
                Nombre = "Cerrar sesión",
                Click = new BLL.Comando (( ) => cerrarSesion_Click ( ))
            });

            InitializeComponent ( );


            this.menugenial.Add (this.inicioSesion);
            this.menu.DataContext = this.menugenial;
        }

        private void materniadad_Click ( ) {
            this.contenido.Children.Clear ( );

            var n = new MaternidadControl ( ) {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            this.contenido.Children.Add (n);
        }

        private void generacionDeInformes_Click ( ) {
            this.contenido.Children.Clear ( );

            var n = new ReporteControl ( ) {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            this.contenido.Children.Add (n);
        }

        private void registroBomberos_Click ( ) {
            this.contenido.Children.Clear ( );

            var n = new BomberoControl ( ) {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            this.contenido.Children.Add (n);
        }

        private void registroUnidadesMoviles_Click ( ) {
            this.contenido.Children.Clear ( );

            var n = new UnidadesMovilesControl ( ) {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            this.contenido.Children.Add (n);

        }

        private void servicioEnfermedadComun_Click ( ) {
            this.contenido.Children.Clear ( );

            var n = new EnfermedadComunControl ( ) {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            this.contenido.Children.Add (n);
        }

        private void mnuitIniciarSesion_Click (Object sender, RoutedEventArgs e) {
            var n = new IniciarSesionWindow ( );
            var r = n.ShowDialog ( );
            //(bool?) true;



            if (!(r.HasValue && r.Value)) {
                return;
            }

            var activo = n.BomberoActivo;
            // TODO: comentariar luego.
            //activo = new BLL.Bombero ( ) { Nombre = "Saul Adolofo", Apellido = "Sac Herrera" };

            this.brdBienvenida.Tag = "Hola";
            this.lblBienvenida.DataContext = activo;
            this.brdBienvenida.Tag = null;

            this.bombero = activo;

            this.menugenial.Clear ( );

            foreach (var item in this.restos) {
                //TODO: quitar comentarios
                if (this.bombero.AccesoValido (item.Llave) || !item.Quitar) {
                    this.menugenial.Add (item);
                }
            }

        }

        private void cerrarSesion_Click ( ) {

            this.bombero = null;
            this.lblBienvenida.DataContext = null;

            this.contenido.Children.Clear ( );
            this.menugenial.Clear ( );
            this.menugenial.Add (this.inicioSesion);
        }


    }
}
