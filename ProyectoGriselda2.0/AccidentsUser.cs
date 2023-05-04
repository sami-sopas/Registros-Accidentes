using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGriselda2._0
{
    public partial class AccidentsUser : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                         "User Id = postgres;" +
                                         "Password = admin;" +
                                         "Database = accidentes");
        public AccidentsUser()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Nombre"; //Para que los comboBox tengan un valor predeterminado

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string query = "select " +
                           "ubicacion.estado as \"Sucedio en\"," +
                           "fecha as \"Fecha\"," +
                           "to_char(hora,'HH24:MI') as \"Hora\"," +
                           "oficial.nombre as \"Atendido por\"," +
                           "oficial.num_placa as \"Con numero de placa\"," +
                           "conductor.nombre as \"Ocasionado por\"," +
                           "vehiculo.matricula as \"Con matricula\"," +
                           "pasajero.nombre as \"Acompañado de\"," +
                           "consecuencia.vialidad as \"Afectando la vialidad\"" +
                           " from accidente " +
                           " join usuario on accidente.id_usuario = usuario.id_usuario" +
                           " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                           " join oficial on accidente.id_oficial = oficial.num_placa" +
                           " join conductor on accidente.id_conductor = conductor.num_licencia" +
                           " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                           " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                           " join consecuencia on accidente.id_consecuencia = consecuencia.id";



            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


        //Boton agregar nuevo accidente
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            //Primero hay que validar si el usuario esta registrado
            if (Program.logueado == false)
            {
                MessageBox.Show("Necesitas iniciar sesion para hacer esta accion !","Advertencia",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            }
            else
            {
                Form addAccidentWindow = new AddAccident1();
          
                addAccidentWindow.Show();
            }

            Consultar(); //Para actualizar el registro que se acaba de hacer
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Estado")
            {
                string query = "select" +
                               "ubicacion.estado as \"Sucedio en\"," +
                               "fecha as \"Fecha\"," +
                               "to_char(hora,'HH24:MI') as \"Hora\"," +
                               "oficial.nombre as \"Atendido por\"," +
                               "oficial.num_placa as \"Con numero de placa\"," +
                               "conductor.nombre as \"Ocasionado por\"," +
                               "vehiculo.matricula as \"Con matricula\"," +
                               "pasajero.nombre as \"Acompañado de\"," +
                               "consecuencia.vialidad as \"Afectando la vialidad\"" +
                               " from accidente " +
                               " join usuario on accidente.id_usuario = usuario.id_usuario" +
                               " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                               " join oficial on accidente.id_oficial = oficial.num_placa" +
                               " join conductor on accidente.id_conductor = conductor.num_licencia" +
                               " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                               " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                               " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                               " where ubicacion.estado = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Nombre de oficial")
            {
                string query = "select" +
                               "ubicacion.estado as \"Sucedio en\"," +
                               "fecha as \"Fecha\"," +
                               "to_char(hora,'HH24:MI') as \"Hora\"," +
                               "oficial.nombre as \"Atendido por\"," +
                               "oficial.num_placa as \"Con numero de placa\"," +
                               "conductor.nombre as \"Ocasionado por\"," +
                               "vehiculo.matricula as \"Con matricula\"," +
                               "pasajero.nombre as \"Acompañado de\"," +
                               "consecuencia.vialidad as \"Afectando la vialidad\"" +
                               " from accidente " +
                               " join usuario on accidente.id_usuario = usuario.id_usuario" +
                               " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                               " join oficial on accidente.id_oficial = oficial.num_placa" +
                               " join conductor on accidente.id_conductor = conductor.num_licencia" +
                               " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                               " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                               " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                               " where oficial.nombre = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Num. Placa de Oficial")
            {
                string query = "select" +
                               "ubicacion.estado as \"Sucedio en\"," +
                               "fecha as \"Fecha\"," +
                               "to_char(hora,'HH24:MI') as \"Hora\"," +
                               "oficial.nombre as \"Atendido por\"," +
                               "oficial.num_placa as \"Con numero de placa\"," +
                               "conductor.nombre as \"Ocasionado por\"," +
                               "vehiculo.matricula as \"Con matricula\"," +
                               "pasajero.nombre as \"Acompañado de\"," +
                               "consecuencia.vialidad as \"Afectando la vialidad\"" +
                               " from accidente " +
                               " join usuario on accidente.id_usuario = usuario.id_usuario" +
                               " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                               " join oficial on accidente.id_oficial = oficial.num_placa" +
                               " join conductor on accidente.id_conductor = conductor.num_licencia" +
                               " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                               " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                               " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                               " where oficial.num_placa = " + int.Parse(textBox_Buscar.Text) + " ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Nombre de Conductor")
            {
                string query = "select" +
                               "ubicacion.estado as \"Sucedio en\"," +
                               "fecha as \"Fecha\"," +
                               "to_char(hora,'HH24:MI') as \"Hora\"," +
                               "oficial.nombre as \"Atendido por\"," +
                               "oficial.num_placa as \"Con numero de placa\"," +
                               "conductor.nombre as \"Ocasionado por\"," +
                               "vehiculo.matricula as \"Con matricula\"," +
                               "pasajero.nombre as \"Acompañado de\"," +
                               "consecuencia.vialidad as \"Afectando la vialidad\"" +
                               " from accidente " +
                               " join usuario on accidente.id_usuario = usuario.id_usuario" +
                               " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                               " join oficial on accidente.id_oficial = oficial.num_placa" +
                               " join conductor on accidente.id_conductor = conductor.num_licencia" +
                               " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                               " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                               " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                               " where pasajero.nombre = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Matricula de Vehiculo")
            {
                string query = "select" +
               "ubicacion.estado as \"Sucedio en\"," +
               "fecha as \"Fecha\"," +
               "to_char(hora,'HH24:MI') as \"Hora\"," +
               "oficial.nombre as \"Atendido por\"," +
               "oficial.num_placa as \"Con numero de placa\"," +
               "conductor.nombre as \"Ocasionado por\"," +
               "vehiculo.matricula as \"Con matricula\"," +
               "pasajero.nombre as \"Acompañado de\"," +
               "consecuencia.vialidad as \"Afectando la vialidad\"" +
               " from accidente " +
               " join usuario on accidente.id_usuario = usuario.id_usuario" +
               " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
               " join oficial on accidente.id_oficial = oficial.num_placa" +
               " join conductor on accidente.id_conductor = conductor.num_licencia" +
               " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
               " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
               " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
               " where vehiculo.matricula = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

            miConexion.Close();
        }

        //Boton para refrescar a la tabla original
        public void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Consultar();

            textBox_Buscar.Text = string.Empty;
        }


    }
}
