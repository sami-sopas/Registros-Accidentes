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
    public partial class Locations : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");

        public Locations()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Estado"; //Para que los comboBox tengan un valor predeterminado

            comboBox_Estado.Text = "Jalisco";

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();
            //select nombre as "Nombre",cinturon as "Cinturon",cantidad as "Cantidad de Pasajeros",asiento as "Numero de Asiento",curp as "CURP" from pasajero
            string queryConsulta = "select id as \"ID\", estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


        //Boton agregar nueva ubicacion
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            string queryAgregar = "insert into ubicacion (estado,municipio,colonia,calle) values (@estado,@municipio,@colonia,@calle)";

            NpgsqlCommand command = new NpgsqlCommand(queryAgregar, miConexion);

            command.Parameters.AddWithValue("estado", comboBox_Estado.Text);
            command.Parameters.AddWithValue("municipio", txtMunicipio.Text);
            command.Parameters.AddWithValue("colonia", txtColonia.Text);
            command.Parameters.AddWithValue("calle", txtCalle.Text);
           
            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar ubicacion
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Conseguir el ID del registro a modificar
            int id = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryUpdate = "update ubicacion set estado = @estado,municipio = @municipio,colonia = @colonia,calle = @calle where id = " + id + " ";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);

            command.Parameters.AddWithValue("estado", comboBox_Estado.Text);
            command.Parameters.AddWithValue("municipio", txtMunicipio.Text);
            command.Parameters.AddWithValue("colonia", txtColonia.Text);
            command.Parameters.AddWithValue("calle", txtCalle.Text);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }


        //Boton para buscar algun registro
        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Estado")
            {
                string query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = '" + textBox_Buscar.Text+"' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable); 

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Municipio")
            {
                string query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where municipio = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Colonia")
            {
                string query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where colonia = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "Calle")
            {
                string query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where calle = '" + textBox_Buscar.Text + "' ";

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
            //Estado
            comboBox_Estado.Text = dataGridView1.SelectedCells[1].Value.ToString(); 

            //Municipio
            txtMunicipio.Text = dataGridView1.SelectedCells[2].Value.ToString();

            //Colonia
            txtColonia.Text = dataGridView1.SelectedCells[3].Value.ToString();

            //Calle
            txtCalle.Text = dataGridView1.SelectedCells[4].Value.ToString();

        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            txtCalle.Text = string.Empty;
            txtMunicipio.Text = string.Empty;
            txtColonia.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;

        }

        //Boton para refrescar a la tabla original
        private void bunifuButton8_Click(object sender, EventArgs e)
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

        //Se consulte dependiendo del estado que selecciono
        private void comboBox_Orden_SelectedValueChanged(object sender, EventArgs e)
        {
            miConexion.Open();
            
            string queryConsulta = "select id as \"ID\", estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = '"+comboBox_Filtro.Text+"'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


    }
}
