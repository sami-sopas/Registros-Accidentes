using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Markup;

namespace ProyectoGriselda2._0
{
    public partial class Drivers : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");
        public Drivers()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Numero de Licencia"; //Para que los comboBox tengan un valor predeterminado
            comboBox_Estado.Text = "Sobrio";

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string queryConsulta = "select num_licencia as \"Numero de Licencia\", nombre as \"Nombre\", cinturon as \"Cinturon\", edad as \"Edad\",seguro as \"seguro\",estado as \"Estado\" from conductor;";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


        //Boton agregar nuevo conductor
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            bool cinturon = false;
            bool seguro = false;
            string estado = string.Empty;

            //Validar radio buttons y comboBox asignarle S,M, etc
            if (radioButtonCinturon_Si.Checked == true)
            {
                cinturon = true;
            }
            else if (radioButtonCinturon_No.Checked == true)
            {
                cinturon = false;
            }

            if (radioButtonSeguro_Si.Checked == true)
            {
                seguro = true;
            }
            else if (radioButtonSeguro_No.Checked == true)
            {
                seguro = false;
            }

            if (comboBox_Estado.Text == "Ebrio")
            {
                estado = "E";
            }
            else if (comboBox_Estado.Text == "Sobrio")
            {
                estado = "S";
            }
            else if (comboBox_Estado.Text == "Drogado")
            {
                estado = "D";
            }

            //string queryConductor = "insert into conductor values ("+int.Parse(textBox_nLicencia.Text)+",'"+textBox_cName.Text+"',;
            string queryConductor = "insert into conductor values (@numero,@nombre,@cinturon,@edad,@seguro,@estado)";

            NpgsqlCommand commandConductor = new NpgsqlCommand(queryConductor, miConexion);

            commandConductor.Parameters.AddWithValue("numero", int.Parse(txtLicencia.Text));
            commandConductor.Parameters.AddWithValue("nombre", txtNombre.Text);
            commandConductor.Parameters.AddWithValue("cinturon", cinturon);
            commandConductor.Parameters.AddWithValue("edad", int.Parse(txtEdad.Text));
            commandConductor.Parameters.AddWithValue("seguro", seguro);
            commandConductor.Parameters.AddWithValue("estado", estado);

            commandConductor.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar vehiculo
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Conseguir el numero de lincencia del conductor
            int licencia = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryUpdate = "update conductor set nombre = @nombre,cinturon = @cinturon,edad = @edad,seguro = @seguro,estado = @estado where num_licencia = " + licencia + " ";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);

            bool cinturon = false;
            bool seguro = false;
            string estado = string.Empty;

            //Validar radio buttons y comboBox asignarle S,M, etc
            if (radioButtonCinturon_Si.Checked == true)
            {
                cinturon = true;
            }
            else if (radioButtonCinturon_No.Checked == true)
            {
                cinturon = false;
            }

            if (radioButtonSeguro_Si.Checked == true)
            {
                seguro = true;
            }
            else if (radioButtonSeguro_No.Checked == true)
            {
                seguro = false;
            }

            if (comboBox_Estado.Text == "Ebrio")
            {
                estado = "E";
            }
            else if (comboBox_Estado.Text == "Sobrio")
            {
                estado = "S";
            }
            else if (comboBox_Estado.Text == "Drogado")
            {
                estado = "D";
            }

            command.Parameters.AddWithValue("nombre", txtNombre.Text);
            command.Parameters.AddWithValue("cinturon", cinturon);
            command.Parameters.AddWithValue("edad", int.Parse(txtEdad.Text));
            command.Parameters.AddWithValue("seguro", seguro);
            command.Parameters.AddWithValue("estado", estado);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }


        //Boton para buscar algun registro
        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Numero de Licencia")
            {
                string queryMatricula = "select num_licencia as \"Numero de Licencia\", nombre as \"Nombre\", cinturon as \"Cinturon\", edad as \"Edad\",seguro as \"seguro\",estado as \"Estado\" from conductor where num_licencia = "+ int.Parse(textBox_Buscar.Text)+"";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryMatricula, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable); 

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por nombre
            {
                string queryMatricula = "select num_licencia as \"Numero de Licencia\", nombre as \"Nombre\", cinturon as \"Cinturon\", edad as \"Edad\",seguro as \"seguro\",estado as \"Estado\" from conductor where nombre = '" + textBox_Buscar.Text + "'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryMatricula, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

            miConexion.Close();
        }

        //Evento para cuando demos click en una fila, se muestren los datos de ese registro a la izquierda
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Numero de licencia
            txtLicencia.Text = dataGridView1.SelectedCells[0].Value.ToString(); 

            //Nombre conductor
            txtNombre.Text = dataGridView1.SelectedCells[1].Value.ToString();

            //Cinturon
            if (dataGridView1.SelectedCells[2].Value.ToString() == "False")
            {
                radioButtonCinturon_No.Checked = true;
            }
            else
            {
                radioButtonCinturon_Si.Checked = true;
            }

            //Edad
            txtEdad.Text = dataGridView1.SelectedCells[3].Value.ToString();

            //Seguro
            if (dataGridView1.SelectedCells[4].Value.ToString() == "False")
            {
                radioButtonSeguro_No.Checked = true;
            }
            else
            {
                radioButtonSeguro_Si.Checked = true;
            }

            //Estado
            if (dataGridView1.SelectedCells[5].Value.ToString() == "S")
            {
                comboBox_Estado.Text = "Ebrio";
            }
            else if(dataGridView1.SelectedCells[5].Value.ToString() == "S")
            {
                comboBox_Estado.Text = "Sobrio";
            }
            else
            {
                comboBox_Estado.Text = "Drogado";
            }
        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            txtLicencia.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtEdad.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;

        }

        //Boton para refrescar a la tabla original
        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Consultar();
            limpiar();

        }
        
        //Mostrar sobrios
        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            string query = "select num_licencia as \"Numero de Licencia\", nombre as \"Nombre\", cinturon as \"Cinturon\", edad as \"Edad\",seguro as \"seguro\",estado as \"Estado\" from conductor where estado = 'S'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar drogados
        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            string query = "select num_licencia as \"Numero de Licencia\", nombre as \"Nombre\", cinturon as \"Cinturon\", edad as \"Edad\",seguro as \"seguro\",estado as \"Estado\" from conductor where estado = 'D'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar ebrios
        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            string query = "select num_licencia as \"Numero de Licencia\", nombre as \"Nombre\", cinturon as \"Cinturon\", edad as \"Edad\",seguro as \"seguro\",estado as \"Estado\" from conductor where estado = 'E'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            Consultar();
            limpiar();
        }
    }
}
