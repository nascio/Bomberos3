using Bomberos.BLL.Excepciones;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bomberos.BLL {
    public class Modelo1 : BLL.Notificador {



        private DateTime fecha;
        private TimeSpan horaEntrada;
        private TimeSpan horaSalida;
        private String lugarEntrada;
        private String lugarSalida;
        private String direccion;

        public string Direccion {
            get {
                return this.direccion;
            }
            set {
                this.direccion = value;
                base.OnPropertyChanged ( );
            }
        }


        private string solicitantePrincipal;

        public String SolicitantePrincipal {
            get {
                return this.solicitantePrincipal;
            }
            set {
                this.solicitantePrincipal = value;
                base.OnPropertyChanged ( );
            }
        }

        private ICommand agregarSolicitante;
        public ICommand CmdAgregarSolicitante {
            get {
                return agregarSolicitante ?? (agregarSolicitante = new BLL.Comando (( ) => {
                    //for (int x = this.solicitantes.Count - 1; x >= 0; x--) {
                    //    if (condicionSolicitante (this.solicitantes[x].Nombre)) {
                    //        return;
                    //    }
                    //}
                    //if (!condicionSolicitante (this.solicitantePrincipal)) {
                    this.solicitantes.Add (new Solicitante (this));
                    //}
                }
                ));
            }
        }

        private static bool condicionSolicitante (String nombre) {
            return string.IsNullOrEmpty (nombre) || nombre.Trim ( ).Length == 0;
        }



        private ObservableCollection<Solicitante> solicitantes;
        public ObservableCollection<Solicitante> Solicitantes {
            get {
                return this.solicitantes;
            }
        }



        private Paciente pacientePrincipal;
        public Paciente PacientePrincipal {
            get {
                return this.pacientePrincipal;
            }
        }
        private ObservableCollection<Paciente> pacientes;
        public ObservableCollection<Paciente> Pacientes {
            get {
                return this.pacientes;
            }
        }

        private ICommand agregarPaciente;
        public ICommand CmdAgregarPaciente {
            get {
                return agregarPaciente ?? (agregarPaciente = new BLL.Comando (( ) => {
                    //for (int x = this.pacientes.Count - 1; x >= 0; x--) {
                    //    if (this.pacientes[x].ComprobarPacienteNuevo ( )) {
                    //        return;
                    //    }
                    //}
                    //if (this.pacientePrincipal.ComprobarPacienteNuevo ( )) {
                    this.pacientes.Add (new Paciente (this));
                    //}
                }
                ));
            }
        }


        private AsignacionUnidadMovil asignacionUnidadPrincipal;
        private ObservableCollection<AsignacionUnidadMovil> asignaciones;

        public AsignacionUnidadMovil AsignacionUnidadPrincipal {
            get {
                return this.asignacionUnidadPrincipal;
            }
        }

        public ObservableCollection<AsignacionUnidadMovil> AsignacionesUnidades {
            get {
                return this.asignaciones;
            }
        }
        private ICommand agregarAsignacionUnidad;
        public ICommand CmdAgregaAsignacionUnidad {
            get {
                return agregarAsignacionUnidad ?? (agregarAsignacionUnidad = new BLL.Comando (( ) => {
                    //for (int x = this.asignaciones.Count - 1; x >= 0; x--) {
                    //    if (this.asignaciones[x].ComprobarPacienteNuevo ( )) {
                    //        return;
                    //    }
                    //}
                    //if (this.asignacionUnidadPrincipal.ComprobarPacienteNuevo ( )) {
                    this.asignaciones.Add (new AsignacionUnidadMovil (this, this.bomberoVacio, this.unidadMovilVacio));
                    //}
                }
                ));
            }
        }


        PersonalDestacado prinPer;
        public PersonalDestacado PrimerPersonalDestacado {
            get {
                return this.prinPer;
            }
        }
        private ObservableCollection<PersonalDestacado> personalDestacado;
        public ObservableCollection<PersonalDestacado> PersonalDestacado {
            get {
                return this.personalDestacado;
            }
        }
        private ICommand agregarPersonalDestacado;
        public ICommand CmdPersonalDestacado {
            get {
                return agregarPersonalDestacado ?? (agregarPersonalDestacado = new BLL.Comando (( ) => {
                    //for (int x = this.asignaciones.Count - 1; x >= 0; x--) {
                    //    if (this.asignaciones[x].ComprobarPacienteNuevo ( )) {
                    //        return;
                    //    }
                    //}
                    //if (this.asignacionUnidadPrincipal.ComprobarPacienteNuevo ( )) {
                    this.personalDestacado.Add (new PersonalDestacado (this, this.bomberoVacio));
                    //}
                }
                ));
            }
        }




        private List<Bombero> bomberos;
        private List<UnidadMovil> unidadesMoviles;

        public List<UnidadMovil> UnidadesMoviles {
            get {
                return this.unidadesMoviles;
            }
        }

        public List<Bombero> Bomberos {
            get {
                return this.bomberos;
            }
        }




        private string observaciones;





        public Modelo1 ( ) {
            this.fecha = DateTime.Now;
            this.solicitantes = new ObservableCollection<Solicitante> ( );
            this.pacientes = new ObservableCollection<Paciente> ( );

            this.asignaciones = new ObservableCollection<AsignacionUnidadMovil> ( );
            this.personalDestacado = new ObservableCollection<PersonalDestacado> ( );

            this.bomberoVacio = new Bombero ( ) { Nombre = "Sin seleccionar" };
            this.unidadMovilVacio = new UnidadMovil ( ) { Placa = "Vacio", Numero = 0 };

            this.solicitantePrincipal = String.Empty;
            this.pacientePrincipal = new Paciente (this);
            this.asignacionUnidadPrincipal = new AsignacionUnidadMovil (this, bomberoVacio, unidadMovilVacio);
            this.prinPer = new PersonalDestacado (this, this.bomberoVacio);

            this.bomberosUsados[0] = this.bomberoVacio;
            this.bomberosUsados[1] = this.bomberoVacio;
            this.bomberosUsados[2] = this.bomberoVacio;
            this.bomberosUsados[3] = this.bomberoVacio;
        }



        public TimeSpan HoraEntrada {
            get {
                return this.horaEntrada;
            }
            set {
                this.horaEntrada = value;
                base.OnPropertyChanged ( );
            }
        }

        public TimeSpan HoraSalida {
            get {
                return this.horaSalida;
            }
            set {
                this.horaSalida = value;
                base.OnPropertyChanged ( );
            }
        }


        public DateTime Fecha {
            get {
                return this.fecha;
            }
            set {
                this.fecha = value;
                base.OnPropertyChanged ( );
            }
        }

        public string Observaciones {
            get {
                return this.observaciones;
            }
            set {
                this.observaciones = value;
                base.OnPropertyChanged ( );
            }
        }




        public string LugarEntrada {
            get {
                return this.lugarEntrada;
            }
            set {

                this.lugarEntrada = value;
                base.OnPropertyChanged ( );
            }
        }

        public string LugarSalida {
            get {
                return this.lugarSalida;
            }
            set {
                this.lugarSalida = value;
                base.OnPropertyChanged ( );
            }
        }





        public void Inicializar ( ) {
            this.initBomberos ( );
            this.initUnidadesMoviles ( );
        }


        private Bombero bomberoVacio;
        private UnidadMovil unidadMovilVacio;

        public bool ComprobarAsignacionConVacio (Bombero b, UnidadMovil u) {
            return b == bomberoVacio || u == unidadMovilVacio;
        }



        private void initBomberos ( ) {
            var b = Bombero.ObtenerBomberos ( );

            this.bomberos = new List<Bombero> ( );
            this.bomberos.Add (this.bomberoVacio);
            this.bomberos.AddRange (b);

            base.OnPropertyChanged ("Bomberos");
        }

        private Comando agregar;
        public ICommand AgregarCMD {
            get {
                return this.agregar ?? (this.agregar = new Comando (( ) => this.Aceptar ( )));
            }
        }

        public bool Aceptar ( ) {
            var sql = null as DAL.DataSet1TableAdapters.insertarTableAdapter;

            try {
                sql = new DAL.DataSet1TableAdapters.insertarTableAdapter ( );
                var id = -1;

                var t = sql.sp_reporte_incidente (1, null, this.Fecha, false, false, this.Observaciones, this.HoraEntrada, this.HoraSalida, 2, 0);
                var r = t.First ( );
                if (r.out_status != 1) {
                    throw new NoSePudoIngresarException ("");
                }

                id = r.out_id;

                #region solicitantes
                (new Solicitante (this) { Nombre = this.SolicitantePrincipal }).Ingresar (0, sql, id);
                foreach (var item in solicitantes) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                #region paciente
                this.pacientePrincipal.Ingresar (0, sql, id);
                foreach (var item in pacientes) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                #region asinaciones
                this.asignacionUnidadPrincipal.Ingresar (0, sql, id);
                foreach (var item in asignaciones) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                #region asinaciones
                this.prinPer.Ingresar (0, sql, id);
                foreach (var item in personalDestacado) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                AgregarIntegrantes (0, sql, id, FormuladoPor, 10);
                AgregarIntegrantes (0, sql, id, Piloto, 11);
                AgregarIntegrantes (0, sql, id, JefeServicios, 12);
                AgregarIntegrantes (0, sql, id, Secretario, 13);

                return true;

            }
            catch (Exception ex) {
                Console.WriteLine (ex);
                return false;
            }
            finally {
                if (sql != null) {
                    sql.Dispose ( );
                }
            }








        }


        private void AgregarIntegrantes (int id_redactor, DAL.DataSet1TableAdapters.insertarTableAdapter sql, int reporte_id, Bombero b, byte labor) {
            var t = sql.sp_integrantes (1, null, reporte_id, b.ID, labor, 0);
        }



        public Bombero FormuladoPor {
            get {
                return this.bomberosUsados[0];
            }
            set {
                this.bomberosUsados[0] = value;
                base.OnPropertyChanged ( );
            }
        }
        public Bombero Piloto {
            get {
                return this.bomberosUsados[1];
            }
            set {
                this.bomberosUsados[1] = value;
                base.OnPropertyChanged ( );
            }
        }
        public Bombero JefeServicios {
            get {
                return this.bomberosUsados[2];
            }
            set {
                this.bomberosUsados[2] = value;
                base.OnPropertyChanged ( );
            }
        }
        public Bombero Secretario {
            get {
                return this.bomberosUsados[3];
            }
            set {
                this.bomberosUsados[3] = value;
                base.OnPropertyChanged ( );
            }
        }




        private Bombero[] bomberosUsados = new Bombero[4];

        private void initOtrosBomberos ( ) {
            var b = this.bomberos;

            foreach (var item in b) {
                //if(item.ac)
            }


        }



        private void initUnidadesMoviles ( ) {
            var b = UnidadMovil.ObtenerUnidadesMoviles ( );

            this.unidadesMoviles = new List<UnidadMovil> ( );
            this.unidadesMoviles.Add (this.unidadMovilVacio);
            this.unidadesMoviles.AddRange (b);

            base.OnPropertyChanged ("UnidadesMoviles");
        }
    }




    public class Modelo2 : Modelo1 {



        private DateTime fecha;
        private TimeSpan horaEntrada;
        private TimeSpan horaSalida;
        private String lugarEntrada;
        private String lugarSalida;
        private String direccion;

        public new string Direccion {
            get {
                return this.direccion;
            }
            set {
                this.direccion = value;
                base.OnPropertyChanged ( );
            }
        }


        private string solicitantePrincipal;

        public new String SolicitantePrincipal {
            get {
                return this.solicitantePrincipal;
            }
            set {
                this.solicitantePrincipal = value;
                base.OnPropertyChanged ( );
            }
        }

        private ICommand agregarSolicitante;
        public new ICommand CmdAgregarSolicitante {
            get {
                return agregarSolicitante ?? (agregarSolicitante = new BLL.Comando (( ) => {
                    //for (int x = this.solicitantes.Count - 1; x >= 0; x--) {
                    //    if (condicionSolicitante (this.solicitantes[x].Nombre)) {
                    //        return;
                    //    }
                    //}
                    //if (!condicionSolicitante (this.solicitantePrincipal)) {
                    this.solicitantes.Add (new Solicitante (this));
                    //}
                }
                ));
            }
        }

        private static bool condicionSolicitante (String nombre) {
            return string.IsNullOrEmpty (nombre) || nombre.Trim ( ).Length == 0;
        }



        private ObservableCollection<Solicitante> solicitantes;
        public new ObservableCollection<Solicitante> Solicitantes {
            get {
                return this.solicitantes;
            }
        }



        private Paciente pacientePrincipal;
        public new Paciente PacientePrincipal {
            get {
                return this.pacientePrincipal;
            }
        }
        private ObservableCollection<Paciente> pacientes;
        public new ObservableCollection<Paciente> Pacientes {
            get {
                return this.pacientes;
            }
        }

        private ICommand agregarPaciente;
        public new ICommand CmdAgregarPaciente {
            get {
                return agregarPaciente ?? (agregarPaciente = new BLL.Comando (( ) => {
                    //for (int x = this.pacientes.Count - 1; x >= 0; x--) {
                    //    if (this.pacientes[x].ComprobarPacienteNuevo ( )) {
                    //        return;
                    //    }
                    //}
                    //if (this.pacientePrincipal.ComprobarPacienteNuevo ( )) {
                    this.pacientes.Add (new Paciente (this) { Parto = true, Sexo = false });
                    //}
                }
                ));
            }
        }


        private AsignacionUnidadMovil asignacionUnidadPrincipal;
        private ObservableCollection<AsignacionUnidadMovil> asignaciones;

        public new AsignacionUnidadMovil AsignacionUnidadPrincipal {
            get {
                return this.asignacionUnidadPrincipal;
            }
        }

        public new ObservableCollection<AsignacionUnidadMovil> AsignacionesUnidades {
            get {
                return this.asignaciones;
            }
        }
        private ICommand agregarAsignacionUnidad;
        public new ICommand CmdAgregaAsignacionUnidad {
            get {
                return agregarAsignacionUnidad ?? (agregarAsignacionUnidad = new BLL.Comando (( ) => {
                    //for (int x = this.asignaciones.Count - 1; x >= 0; x--) {
                    //    if (this.asignaciones[x].ComprobarPacienteNuevo ( )) {
                    //        return;
                    //    }
                    //}
                    //if (this.asignacionUnidadPrincipal.ComprobarPacienteNuevo ( )) {
                    this.asignaciones.Add (new AsignacionUnidadMovil (this, this.bomberoVacio, this.unidadMovilVacio));
                    //}
                }
                ));
            }
        }


        PersonalDestacado prinPer;
        public new PersonalDestacado PrimerPersonalDestacado {
            get {
                return this.prinPer;
            }
        }
        private ObservableCollection<PersonalDestacado> personalDestacado;
        public new ObservableCollection<PersonalDestacado> PersonalDestacado {
            get {
                return this.personalDestacado;
            }
        }
        private ICommand agregarPersonalDestacado;
        public new ICommand CmdPersonalDestacado {
            get {
                return agregarPersonalDestacado ?? (agregarPersonalDestacado = new BLL.Comando (( ) => {
                    //for (int x = this.asignaciones.Count - 1; x >= 0; x--) {
                    //    if (this.asignaciones[x].ComprobarPacienteNuevo ( )) {
                    //        return;
                    //    }
                    //}
                    //if (this.asignacionUnidadPrincipal.ComprobarPacienteNuevo ( )) {
                    this.personalDestacado.Add (new PersonalDestacado (this, this.bomberoVacio));
                    //}
                }
                ));
            }
        }




        private List<Bombero> bomberos;
        private List<UnidadMovil> unidadesMoviles;

        public new List<UnidadMovil> UnidadesMoviles {
            get {
                return this.unidadesMoviles;
            }
        }

        public new List<Bombero> Bomberos {
            get {
                return this.bomberos;
            }
        }




        private string observaciones;





        public Modelo2 ( ) {
            this.fecha = DateTime.Now;
            this.solicitantes = new ObservableCollection<Solicitante> ( );
            this.pacientes = new ObservableCollection<Paciente> ( );

            this.asignaciones = new ObservableCollection<AsignacionUnidadMovil> ( );
            this.personalDestacado = new ObservableCollection<PersonalDestacado> ( );

            this.bomberoVacio = new Bombero ( ) { Nombre = "Sin seleccionar" };
            this.unidadMovilVacio = new UnidadMovil ( ) { Placa = "Vacio", Numero = 0 };

            this.solicitantePrincipal = String.Empty;
            this.pacientePrincipal = new Paciente (this) { Parto = true, Sexo = false };
            this.asignacionUnidadPrincipal = new AsignacionUnidadMovil (this, bomberoVacio, unidadMovilVacio);
            this.prinPer = new PersonalDestacado (this, this.bomberoVacio);

            this.bomberosUsados[0] = this.bomberoVacio;
            this.bomberosUsados[1] = this.bomberoVacio;
            this.bomberosUsados[2] = this.bomberoVacio;
            this.bomberosUsados[3] = this.bomberoVacio;
        }



        public new TimeSpan HoraEntrada {
            get {
                return this.horaEntrada;
            }
            set {
                this.horaEntrada = value;
                base.OnPropertyChanged ( );
            }
        }

        public new TimeSpan HoraSalida {
            get {
                return this.horaSalida;
            }
            set {
                this.horaSalida = value;
                base.OnPropertyChanged ( );
            }
        }


        public new DateTime Fecha {
            get {
                return this.fecha;
            }
            set {
                this.fecha = value;
                base.OnPropertyChanged ( );
            }
        }

        public new string Observaciones {
            get {
                return this.observaciones;
            }
            set {
                this.observaciones = value;
                base.OnPropertyChanged ( );
            }
        }




        public new string LugarEntrada {
            get {
                return this.lugarEntrada;
            }
            set {

                this.lugarEntrada = value;
                base.OnPropertyChanged ( );
            }
        }

        public new string LugarSalida {
            get {
                return this.lugarSalida;
            }
            set {
                this.lugarSalida = value;
                base.OnPropertyChanged ( );
            }
        }





        public new void Inicializar ( ) {
            this.initBomberos ( );
            this.initUnidadesMoviles ( );
        }


        private Bombero bomberoVacio;
        private UnidadMovil unidadMovilVacio;

        public new bool ComprobarAsignacionConVacio (Bombero b, UnidadMovil u) {
            return b == bomberoVacio || u == unidadMovilVacio;
        }



        private void initBomberos ( ) {
            var b = Bombero.ObtenerBomberos ( );

            this.bomberos = new List<Bombero> ( );
            this.bomberos.Add (this.bomberoVacio);
            this.bomberos.AddRange (b);

            base.OnPropertyChanged ("Bomberos");
        }

        private Comando agregar;
        public new ICommand AgregarCMD {
            get {
                return this.agregar ?? (this.agregar = new Comando (( ) => this.Aceptar ( )));
            }
        }

        public new bool Aceptar ( ) {
            var sql = null as DAL.DataSet1TableAdapters.insertarTableAdapter;

            try {
                sql = new DAL.DataSet1TableAdapters.insertarTableAdapter ( );
                var id = -1;

                var t = sql.sp_reporte_incidente (1, null, this.Fecha, false, false, this.Observaciones, this.HoraEntrada, this.HoraSalida, 4, 0);
                var r = t.First ( );
                if (r.out_status != 1) {
                    throw new NoSePudoIngresarException ("");
                }

                id = r.out_id;

                #region solicitantes
                (new Solicitante (this) { Nombre = this.SolicitantePrincipal }).Ingresar (0, sql, id);
                foreach (var item in solicitantes) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                #region paciente
                this.pacientePrincipal.Ingresar (0, sql, id);
                foreach (var item in pacientes) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                #region asinaciones
                this.asignacionUnidadPrincipal.Ingresar (0, sql, id);
                foreach (var item in asignaciones) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                #region asinaciones
                this.prinPer.Ingresar (0, sql, id);
                foreach (var item in personalDestacado) {
                    item.Ingresar (0, sql, id);
                }
                #endregion


                AgregarIntegrantes (0, sql, id, FormuladoPor, 10);
                AgregarIntegrantes (0, sql, id, Piloto, 11);
                AgregarIntegrantes (0, sql, id, JefeServicios, 12);
                AgregarIntegrantes (0, sql, id, Secretario, 13);

                return true;

            }
            catch (Exception ex) {
                Console.WriteLine (ex);
                return false;
            }
            finally {
                if (sql != null) {
                    sql.Dispose ( );
                }
            }








        }


        private void AgregarIntegrantes (int id_redactor, DAL.DataSet1TableAdapters.insertarTableAdapter sql, int reporte_id, Bombero b, byte labor) {
            var t = sql.sp_integrantes (1, null, reporte_id, b.ID, labor, 0);
        }



        public new Bombero FormuladoPor {
            get {
                return this.bomberosUsados[0];
            }
            set {
                this.bomberosUsados[0] = value;
                base.OnPropertyChanged ( );
            }
        }
        public new Bombero Piloto {
            get {
                return this.bomberosUsados[1];
            }
            set {
                this.bomberosUsados[1] = value;
                base.OnPropertyChanged ( );
            }
        }
        public new Bombero JefeServicios {
            get {
                return this.bomberosUsados[2];
            }
            set {
                this.bomberosUsados[2] = value;
                base.OnPropertyChanged ( );
            }
        }
        public new Bombero Secretario {
            get {
                return this.bomberosUsados[3];
            }
            set {
                this.bomberosUsados[3] = value;
                base.OnPropertyChanged ( );
            }
        }




        private Bombero[] bomberosUsados = new Bombero[4];

        private void initOtrosBomberos ( ) {
            var b = this.bomberos;

            foreach (var item in b) {
                //if(item.ac)
            }


        }



        private void initUnidadesMoviles ( ) {
            var b = UnidadMovil.ObtenerUnidadesMoviles ( );

            this.unidadesMoviles = new List<UnidadMovil> ( );
            this.unidadesMoviles.Add (this.unidadMovilVacio);
            this.unidadesMoviles.AddRange (b);

            base.OnPropertyChanged ("UnidadesMoviles");
        }
    }

    public class PersonalDestacado : BLL.Notificador {
        enum banderas {
            cero, una, dos, tres, cuatro, cinco
        }
        private BLL.Bombero bombero;
        private banderas valoracion;
        private Modelo1 modelo;
        private string group;

        public PersonalDestacado (Modelo1 mo, Bombero b) {
            this.modelo = mo;
            this.bombero = b;
            this.group = Guid.NewGuid ( ).ToString ( ).Replace ("-", "");
        }

        public string Grupo {
            get {
                return this.group;
            }
        }


        public Modelo1 Reporte {
            get {
                return this.modelo;
            }
        }

        public Bombero Bombero {
            get {
                return this.bombero;
            }
            set {
                this.bombero = value;
                base.OnPropertyChanged ( );
            }
        }


        public bool UnaEstrellas {
            get {
                return this.valoracion == banderas.una;
            }
            set {
                this.valoracion = value ? banderas.una : UnaEstrellas ? banderas.cero : this.valoracion;
                base.OnPropertyChanged ( );
            }
        }

        public bool DosEstrellas {
            get {
                return this.valoracion == banderas.dos;
            }
            set {
                this.valoracion = value ? banderas.dos : DosEstrellas ? banderas.cero : this.valoracion;
                base.OnPropertyChanged ( );
            }
        }
        public bool TresEstrellas {
            get {
                return this.valoracion == banderas.tres;
            }
            set {
                this.valoracion = value ? banderas.tres : TresEstrellas ? banderas.cero : this.valoracion;
                base.OnPropertyChanged ( );
            }
        }
        public bool CuatroEstrellas {
            get {
                return this.valoracion == banderas.cuatro;
            }
            set {
                this.valoracion = value ? banderas.cuatro : CuatroEstrellas ? banderas.cero : this.valoracion;
                base.OnPropertyChanged ( );
            }
        }

        public bool CincoEstrellas {
            get {
                return this.valoracion == banderas.cinco;
            }
            set {
                this.valoracion = value ? banderas.cinco : CincoEstrellas ? banderas.cero : this.valoracion;
                base.OnPropertyChanged ( );
            }
        }

        public void QuitarEstrellas ( ) {
            this.valoracion = banderas.cero;
        }




        public bool ComprobarPersonalDestacado ( ) {
            return true;

        }


        internal void Ingresar (int id_redactor, DAL.DataSet1TableAdapters.insertarTableAdapter sql, int reporte_id) {
            if (modelo.ComprobarAsignacionConVacio (bombero, null) || valoracion == banderas.cero) {
                return;
            }
            var t = sql.sp_personal_destacado (1, null, reporte_id, bombero.ID, (int) valoracion, 0);
        }




        private ICommand quitar;

        public ICommand QuitarPersonalDestacado {
            get {
                return this.quitar ?? (quitar = new BLL.Comando (( ) => modelo.PersonalDestacado.Remove (this)));
            }
        }


    }


    public class Solicitante : BLL.Notificador {

        private String nombre;
        private Modelo1 modelo;
        public Solicitante (Modelo1 m) {
            this.modelo = m;
        }

        public String Nombre {
            get {
                return this.nombre;
            }
            set {
                this.nombre = value;
                base.OnPropertyChanged ( );
            }
        }

        private ICommand quitar;

        public void Nada ( ) {
        }
        public int omega;

        public ICommand QuitarSolicitante {
            get {
                return this.quitar ?? (quitar = new BLL.Comando (( ) => modelo.Solicitantes.Remove (this)));
            }
        }


        internal void Ingresar (int id_redactor, DAL.DataSet1TableAdapters.insertarTableAdapter sql, int reporte_id) {
            if (StringIsNullOrEmpty (this.Nombre)) {
                return;
            }

            var t = sql.sp_solicitante (1, null, reporte_id, modelo.Fecha, this.nombre, "", 0);
        }

    }


    public enum EstadoPaciente {
        Vacio,
        Herido,
        Fallecido,
        Vivo
    }

    public class Paciente : BLL.Notificador {

        private String nombre;
        private EstadoPaciente estado = EstadoPaciente.Vivo;
        private int edad;

        private string causa;
        private string domicilio;
        private string nombreAcompaniante;
        private Modelo1 modelo;
        private bool sexo;

        public Paciente (Modelo1 m) {
            this.modelo = m;
        }



        public string Domicilio {
            get {
                return this.domicilio;
            }
            set {
                this.domicilio = value;
                base.OnPropertyChanged ( );
            }
        }

        public string Acompaniante {
            get {
                return this.nombreAcompaniante;
            }
            set {
                this.nombreAcompaniante = value;
                base.OnPropertyChanged ( );
            }
        }

        public string Nombre {
            get {
                return this.nombre;
            }
            set {
                this.nombre = value;
                base.OnPropertyChanged ( );
            }
        }

        public bool Estado {
            get {
                return this.estado == EstadoPaciente.Fallecido;
            }
            set {
                this.EstadoEnum = value ? EstadoPaciente.Fallecido : EstadoPaciente.Vivo;
            }
        }

        public EstadoPaciente EstadoEnum {
            get {
                return this.estado;
            }
            set {
                this.estado = value;
                base.OnPropertyChanged ( );
                base.OnPropertyChanged ("Estado");
            }
        }

        public int Edad {
            get {
                return this.edad;
            }
            set {
                this.edad = value;
                base.OnPropertyChanged ( );
            }
        }

        public string Causa {
            get {
                return this.causa;
            }
            set {
                this.causa = value;
                base.OnPropertyChanged ( );
            }
        }

        public bool Sexo {
            get {
                return this.sexo;
            }
            set {
                this.sexo = value;
                base.OnPropertyChanged ( );
            }
        }


        public bool ComprobarPacienteNuevo ( ) {
            return true;

        }



        private ICommand quitar;

        public ICommand QuitarPaciente {
            get {
                return this.quitar ?? (quitar = new BLL.Comando (( ) => modelo.Pacientes.Remove (this)));
            }
        }


        public bool? Parto {
            get; set;
        }
        public bool? Aborto {
            get; set;
        }






        internal void Ingresar (int id_redactor, DAL.DataSet1TableAdapters.insertarTableAdapter sql, int reporte_id) {
            if (StringIsNullOrEmpty (this.Nombre)) {
                return;
            }

            var t = sql.sp_paciente (1, null, reporte_id, this.Nombre, "", this.EstadoEnum.ToString ( ),
                this.Edad, this.Sexo, this.Causa, this.Domicilio, StringIsNullOrEmpty (this.Acompaniante) ? null : this.Acompaniante,
                this.Parto, this.Aborto, 0);
        }



    }


    public class AsignacionUnidadMovil : BLL.Notificador {

        private UnidadMovil unidad;
        private BLL.Bombero bombero;

        private Modelo1 reporte;


        public UnidadMovil UnidadMovil {
            get {
                return this.unidad;
            }
            set {
                this.unidad = value;
                base.OnPropertyChanged ( );
            }
        }

        public Bombero Bombero {
            get {
                return this.bombero;
            }
            set {
                this.bombero = value;
                base.OnPropertyChanged ( );
            }
        }


        public AsignacionUnidadMovil (Modelo1 reporte, Bombero b, UnidadMovil um) {
            this.reporte = reporte;
            this.bombero = b;
            this.unidad = um;
        }


        public Modelo1 Reporte {
            get {
                return this.reporte;
            }
        }


        public bool ComprobarAsignacionUnidad ( ) {
            return true;

        }

        internal void Ingresar (int id_redactor, DAL.DataSet1TableAdapters.insertarTableAdapter sql, int reporte_id) {
            if (reporte.ComprobarAsignacionConVacio (bombero, unidad)) {
                return;
            }
            var t = sql.sp_asignaciones_unidades (1, null, reporte_id, bombero.ID, unidad.ID, 0);
        }

        private ICommand quitar;

        public ICommand QuitarAsignacionUnidad {
            get {
                return this.quitar ?? (quitar = new BLL.Comando (( ) => reporte.AsignacionesUnidades.Remove (this)));
            }
        }

    }


}
