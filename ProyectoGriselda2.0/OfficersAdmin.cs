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
    public partial class OfficersAdmin : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                         "User Id = postgres;" +
                                         "Password = admin;" +
                                         "Database = accidentes");
        public OfficersAdmin()
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
                queryConsulta = "select num_placa as Placa, cargo as Cargo, nombre as Nombre from oficial ORDER BY num_placa ASC";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Orden.Text == "Nombre")
            {
                queryConsulta = "select num_placa as Placa, cargo as Cargo, nombre as Nombre from oficial ORDER BY nombre ASC";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por el cargo
            {
                queryConsulta = "select num_placa as Placa, cargo as Cargo, nombre as Nombre from oficial ORDER BY cargo ASC";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }


            miConexion.Close();
        }


        //Boton agregar nuevo usuario
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            string queryAddUser = "insert into oficial (num_placa,cargo,nombre) values (@placa,@cargo,@nombre)";

            NpgsqlCommand command = new NpgsqlCommand(queryAddUser, miConexion);

            command.Parameters.AddWithValue("placa", Int32.Parse(txtPlaca.Text));
            command.Parameters.AddWithValue("cargo", txtCargo.Text);
            command.Parameters.AddWithValue("nombre", txtNombre.Text);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar registro
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Conseguir el Numpero de placa del usuario a actualizar
            int num_placa = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryUpdate = "update oficial set cargo = @cargo, nombre = @nombre where num_placa = " + num_placa + "";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);

            command.Parameters.AddWithValue("cargo", txtCargo.Text);
            command.Parameters.AddWithValue("nombre", txtNombre.Text);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }

        //Boton para eliminar registro
        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            //Agarramos el numero de placa desde la columna seleccionada que se muestra en el dataGrid

            int num_placa = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryDelete = "delete from oficial where num_placa = " + num_placa + "";

            NpgsqlCommand command = new NpgsqlCommand(queryDelete, miConexion);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }

        //Boton para buscar algun registro

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Placa")
            {
                string queryPlaca = "select num_placa as Placa, cargo as Cargo, nombre as Nombre from oficial where num_placa = " + Int32.Parse(textBox_Buscar.Text) + "";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryPlaca, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Nombre")
            {
                string queryNombre = "select num_placa as Placa, cargo as Cargo, nombre as Nombre from oficial where nombre = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryNombre, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por el cargo
            {
                string queryCargo = "select num_placa as Placa, cargo as Cargo, nombre as Nombre from oficial where cargo = '" + textBox_Buscar.Text + "' ";

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
        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Consultar();
            txtNombre.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtCargo.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;
        }

        private void comboBox_Orden_SelectedValueChanged(object sender, EventArgs e)
        {
            Consultar();
        }


    }
}
