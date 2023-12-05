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
    public partial class OfficersUser : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = usuario;" +
                                                 "Database = accidentes");
        public OfficersUser()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Nombre"; //Para que los comboBox tengan un valor predeterminado
            comboBox_Orden.Text = "Nombre";

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string queryConsulta = string.Empty;

            if (comboBox_Orden.Text == "Placa")
            {
                queryConsulta = "select num_placa as \"Numero de Placa\",nombre as \"Nombre\",cargo as \"Cargo\" from oficial ORDER BY num_placa ASC";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Orden.Text == "Nombre")
            {
                queryConsulta = "select num_placa as \"Numero de Placa\",nombre as \"Nombre\",cargo as \"Cargo\" from oficial ORDER BY nombre ASC";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por el cargo
            {
                queryConsulta = "select num_placa as \"Numero de Placa\",nombre as \"Nombre\",cargo as \"Cargo\" from oficial ORDER BY cargo ASC";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }


            miConexion.Close();
        }


        //Boton para buscar algun registro

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Placa")
            {
                string queryPlaca = "select num_placa as \"Numero de Placa\",nombre as \"Nombre\",cargo as \"Cargo\" from oficial where num_placa = " + Int32.Parse(textBox_Buscar.Text) + "";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryPlaca, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Nombre")
            {
                string queryNombre = "select num_placa as \"Numero de Placa\",nombre as \"Nombre\",cargo as \"Cargo\" from oficial where nombre = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryNombre, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por el cargo
            {
                string queryCargo = "select num_placa as \"Numero de Placa\",nombre as \"Nombre\",cargo as \"Cargo\" from oficial where cargo = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryCargo, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

            miConexion.Close();
        }

        //Evento para cuando demos click en una fila, se muestren los datos de ese registro a la izquierda
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = dataGridView1.SelectedCells[1].Value.ToString();
            txtPlaca.Text = dataGridView1.SelectedCells[0].Value.ToString();
            txtCargo.Text = dataGridView1.SelectedCells[2].Value.ToString();
        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtCargo.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;
        }

        //Boton para refrescar a la tabla original
        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            Consultar();
            limpiar();
        }

        private void comboBox_Orden_SelectedValueChanged(object sender, EventArgs e)
        {
            Consultar();
        }



    }
}
