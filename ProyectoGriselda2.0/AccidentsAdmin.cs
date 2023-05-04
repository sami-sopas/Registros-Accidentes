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
    public partial class AccidentsAdmin : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                         "User Id = postgres;" +
                                         "Password = admin;" +
                                         "Database = accidentes");
        public AccidentsAdmin()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Nombre"; //Para que los comboBox tengan un valor predeterminado

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string query = "select accidente.id as \"ID\"," +
                           "fecha as \"Fecha\"," +
                           "to_char(hora,'HH24:MI') as \"Hora\"," +
                           "usuario.nombre as \"Reportado por\"," +
                           "ubicacion.estado as \"Sucedio en\"," +
                           "oficial.nombre as \"Atendido por\"," +
                           "conductor.nombre as \"Ocasionado por\"," +
                           "vehiculo.matricula as \"Con matricula\"," +
                           "pasajero.nombre as \"Acompañado de\"," +
                           "consecuencia.vialidad as \"Afectando la vialidad\"" +
                           "from accidente" +
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
            //ToDo
        }

        //Boton para eliminar registro
        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            //Agarramos el numero de placa desde la columna seleccionada que se muestra en el dataGrid

            int id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryDelete = "delete from accidente where id = " + id + "";

            NpgsqlCommand command = new NpgsqlCommand(queryDelete, miConexion);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }

        //Boton para buscar algun registro

             /*
             ID
            Estado
            Matricula
            Nombre de Usuario
            Nombre de Pasajero
            Nombre de Oficial
            Nombre de Conductor 
             */

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "ID")
            {
                string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where accidente.id = " + int.Parse(textBox_Buscar.Text) + "";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Estado")
            {
                string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where ubicacion.estado = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Matricula")
            {
                string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where vehiculo.matricula = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Nombre de Usuario")
            {
                string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where usuario.nombre = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Nombre de Pasajero")
            {
                 string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where pasajero.nombre = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Nombre de Oficial")
            {
                string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where pasajero.nombre = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Nombre de Conductor")
            {
                string query = "select accidente.id as \"ID\"," +
                "fecha as \"Fecha\"," +
                "to_char(hora,'HH24:MI') as \"Hora\"," +
                "usuario.nombre as \"Reportado por\"," +
                "ubicacion.estado as \"Sucedio en\"," +
                "oficial.nombre as \"Atendido por\"," +
                "conductor.nombre as \"Ocasionado por\"," +
                "vehiculo.matricula as \"Con matricula\"," +
                "pasajero.nombre as \"Acompañado de\"," +
                "consecuencia.vialidad as \"Afectando la vialidad\"" +
                "from accidente" +
                " join usuario on accidente.id_usuario = usuario.id_usuario" +
                " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                " join oficial on accidente.id_oficial = oficial.num_placa" +
                " join conductor on accidente.id_conductor = conductor.num_licencia" +
                " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor" +
                " join pasajero on vehiculo.matricula = pasajero.matricula_carro" +
                " join consecuencia on accidente.id_consecuencia = consecuencia.id" +
                " where conductor.nombre = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

            miConexion.Close();
        }

        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            textBox_Buscar.Text = string.Empty;
        }

        //Boton para refrescar a la tabla original
        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Consultar();

            textBox_Buscar.Text = string.Empty;
        }


    }
}
