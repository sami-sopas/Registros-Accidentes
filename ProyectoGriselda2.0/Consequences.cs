using Bunifu.UI.WinForms.BunifuButton;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Markup;

namespace ProyectoGriselda2._0
{
    public partial class Consequences : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                         "User Id = postgres;" +
                                         "Password = admin;" +
                                         "Database = accidentes");

        public Consequences()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Vialidad"; //Para que los comboBox tengan un valor predeterminado

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string queryConsulta = "select id as \"ID\",vialidad as \"Vialidad\",vehiculo as \"Vehiculo\",conductor as \"Conductor\",pasajero as \"Pasajero\" from consecuencia;";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


        //Boton agregar nueva afetacion
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            string queryAgregar = "insert into consecuencia (vialidad,vehiculo,conductor,pasajero) values (@vialidad,@vehiculo,@conductor,@pasajero)";

            NpgsqlCommand command = new NpgsqlCommand(queryAgregar, miConexion);

            command.Parameters.AddWithValue("vialidad", txtVialidad.Text);
            command.Parameters.AddWithValue("vehiculo", txtVehiculo.Text);
            command.Parameters.AddWithValue("conductor", txtConductor.Text);
            command.Parameters.AddWithValue("pasajero", txtPasajero.Text);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar la afectacion
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Conseguir el ID del registro a modificar
            int id = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryUpdate = "update consecuencia set vialidad = @vialidad,vehiculo = @vehiculo,conductor = @conductor,pasajero = @pasajero where id = " + id + " ";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);

            command.Parameters.AddWithValue("vialidad", txtVialidad.Text);
            command.Parameters.AddWithValue("vehiculo", txtVehiculo.Text);
            command.Parameters.AddWithValue("conductor", txtConductor.Text);
            command.Parameters.AddWithValue("pasajero", txtPasajero.Text);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }


        //Boton para buscar algun registro
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Vialidad")
            {
                string query = "select id as \"ID\",vialidad as \"Vialidad\",vehiculo as \"Vehiculo\",conductor as \"Conductor\",pasajero as \"Pasajero\" from consecuencia where vialidad = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Vehiculo")
            {
                string query = "select id as \"ID\",vialidad as \"Vialidad\",vehiculo as \"Vehiculo\",conductor as \"Conductor\",pasajero as \"Pasajero\" from consecuencia where vehiculo = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Conductor")
            {
                string query = "select id as \"ID\",vialidad as \"Vialidad\",vehiculo as \"Vehiculo\",conductor as \"Conductor\",pasajero as \"Pasajero\" from consecuencia where conductor = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Pasajero")
            {
                string query = "select id as \"ID\",vialidad as \"Vialidad\",vehiculo as \"Vehiculo\",conductor as \"Conductor\",pasajero as \"Pasajero\" from consecuencia where pasajero = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "ID")
            {
                string query = "select id as \"ID\",vialidad as \"Vialidad\",vehiculo as \"Vehiculo\",conductor as \"Conductor\",pasajero as \"Pasajero\" from consecuencia where id = " + int.Parse(textBox_Buscar.Text) + "";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

            miConexion.Close();
        }

        //Evento para cuando demos click en una fila, se muestren los datos de ese registro a la izquierda
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Vialidad
            txtVialidad.Text = dataGridView1.SelectedCells[1].Value.ToString();

            //Vehiculo
            txtVehiculo.Text = dataGridView1.SelectedCells[2].Value.ToString();

            //Conductor
            txtConductor.Text = dataGridView1.SelectedCells[3].Value.ToString();

            //Pasajero
            txtPasajero.Text = dataGridView1.SelectedCells[4].Value.ToString();

        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            txtVialidad.Text = string.Empty;
            txtPasajero.Text = string.Empty;
            txtVehiculo.Text = string.Empty;
            txtConductor.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;

        }

        //Boton para refrescar a la tabla original
        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Consultar();
            limpiar();
        }

    }
    
}
