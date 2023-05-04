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
    public partial class Passengers : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                         "User Id = postgres;" +
                                         "Password = admin;" +
                                         "Database = accidentes");

        public Passengers()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Nombre"; //Para que los comboBox tengan un valor predeterminado

            button2.Checked = true; 

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();
            //select nombre as "Nombre",cinturon as "Cinturon",cantidad as "Cantidad de Pasajeros",asiento as "Numero de Asiento",curp as "CURP" from pasajero
            string queryConsulta = "select nombre as \"Nombre\",cinturon as \"Cinturon\",cantidad as \"Cantidad de Pasajeros\",asiento as \"Numero de Asiento\",curp as \"CURP\",matricula_carro as \"Matricula\" from pasajero";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


        //Boton agregar nuevo pasajero
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            bool cinturon = false;

            //Validar radio buttons
            if (radioButtonCinturon_Si.Checked == true)
            {
                cinturon = true;
            }
            else if (radioButtonCinturon_No.Checked == true)
            {
                cinturon = false;
            }

            //Boton seleccionado por el usuario para indicar el asiento
            int asiento = 0;

            if(button2.Checked == true)
            {
                asiento = 2;
            }
            else if(button3.Checked == true)
            {
                asiento = 3;
            }
            else if(button4.Checked == true)
            { 
                asiento = 4;
            }
            else if(button5.Checked == true)
            {
                asiento = 5;
            }

            string queryPasajero = "insert into pasajero values (@nombre,@cinturon,@cantidad,@asiento,@curp,@matricula_carro)";

            NpgsqlCommand commandPasajero = new NpgsqlCommand(queryPasajero, miConexion);

            commandPasajero.Parameters.AddWithValue("nombre",txtNombre.Text);
            commandPasajero.Parameters.AddWithValue("cinturon", cinturon);
            commandPasajero.Parameters.AddWithValue("cantidad", int.Parse(txtCantidad.Text));
            commandPasajero.Parameters.AddWithValue("asiento", asiento);
            commandPasajero.Parameters.AddWithValue("curp", txtCurp.Text);
            commandPasajero.Parameters.AddWithValue("matricula_carro",txtMatricula.Text);
           
            commandPasajero.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar vehiculo
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Conseguir la curp (PK) de el registro a editar
            string curp = dataGridView1.SelectedCells[4].Value.ToString();

            string queryUpdate = "update pasajero set nombre = @nombre,cinturon = @cinturon,cantidad = @cantidad,asiento = @asiento where curp = '" + curp + "' ";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);

            bool cinturon = false;

            //Validar radio buttons
            if (radioButtonCinturon_Si.Checked == true)
            {
                cinturon = true;
            }
            else if (radioButtonCinturon_No.Checked == true)
            {
                cinturon = false;
            }

            //Boton seleccionado por el usuario para indicar el asiento
            int asiento = 0;

            if (button2.Checked == true)
            {
                asiento = 2;
            }
            else if (button3.Checked == true)
            {
                asiento = 3;
            }
            else if (button4.Checked == true)
            {
                asiento = 4;
            }
            else if (button5.Checked == true)
            {
                asiento = 5;
            }

            command.Parameters.AddWithValue("nombre", txtNombre.Text);
            command.Parameters.AddWithValue("cinturon", cinturon);
            command.Parameters.AddWithValue("cantidad", int.Parse(txtCantidad.Text));
            command.Parameters.AddWithValue("asiento", asiento);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }


        //Boton para buscar algun registro
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //select nombre as "Nombre",cinturon as "Cinturon",cantidad as "Cantidad de Pasajeros",asiento as "Numero de Asiento",curp as "CURP" from pasajero

            if (comboBox_Buscar.Text == "Nombre")
            {
                string queryNombre = "select nombre as \"Nombre\",cinturon as \"Cinturon\",cantidad as \"Cantidad de Pasajeros\",asiento as \"Numero de Asiento\",curp as \"CURP\",matricula_carro as \"Matricula\" from pasajero where nombre = '" + textBox_Buscar.Text+"' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryNombre, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable); 

                dataGridView1.DataSource = dataTable;
            }
            else if(comboBox_Buscar.Text == "CURP")
            {
                string queryCurp = "select nombre as \"Nombre\",cinturon as \"Cinturon\",cantidad as \"Cantidad de Pasajeros\",asiento as \"Numero de Asiento\",curp as \"CURP\",matricula_carro as \"Matricula\" from pasajero where curp = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryCurp, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por matricula 
            {
                string queryCurp = "select nombre as \"Nombre\",cinturon as \"Cinturon\",cantidad as \"Cantidad de Pasajeros\",asiento as \"Numero de Asiento\",curp as \"CURP\",matricula_carro as \"Matricula\" from pasajero where matricula_carro = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryCurp, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }

            miConexion.Close();
        }

        //Evento para cuando demos click en una fila, se muestren los datos de ese registro a la izquierda
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Nombre pasajero
            txtNombre.Text = dataGridView1.SelectedCells[0].Value.ToString(); 

            //Cinturon
            if (dataGridView1.SelectedCells[1].Value.ToString() == "False")
            {
                radioButtonCinturon_No.Checked = true;
            }
            else
            {
                radioButtonCinturon_Si.Checked = true;
            }

            //Cantidad
            txtCantidad.Text = dataGridView1.SelectedCells[2].Value.ToString();

            //Asiento

            if (dataGridView1.SelectedCells[3].Value.ToString() == "2")
            {
                button2.Checked = true;
                button3.Checked = false;
                button4.Checked = false;
                button5.Checked = false;
            }
            else if (dataGridView1.SelectedCells[3].Value.ToString() == "3")
            {
                button3.Checked = true;
                button2.Checked = false;
                button4.Checked = false;
                button5.Checked = false;
            }
            else if (dataGridView1.SelectedCells[3].Value.ToString() == "4")
            {
                button4.Checked = true;
                button3.Checked = false;
                button2.Checked = false;
                button5.Checked = false;
            }
            else if (dataGridView1.SelectedCells[3].Value.ToString() == "5")
            {
                button5.Checked = true;
                button3.Checked = false;
                button4.Checked = false;
                button2.Checked = false;
            }

            //Curp
            txtCurp.Text = dataGridView1.SelectedCells[4].Value.ToString();

            //Mostrar matricula
            txtMatricula.Text = dataGridView1.SelectedCells[5].Value.ToString();

        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {

            txtNombre.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtCurp.Text = string.Empty;
            txtMatricula.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;

        }

        //Boton para refrescar a la tabla original
        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Consultar();
            limpiar();
        }
        

        //Eventos para que solo se pueda activar un boton
        private void button2_CheckedChanged(object sender, EventArgs e)
        {
            if(button2.Checked)
            {
                button3.Checked = false;
                button4.Checked = false;
                button5.Checked = false;
            }
        }

        private void button3_CheckedChanged(object sender, EventArgs e)
        {
            if (button3.Checked)
            {
                button2.Checked = false;
                button4.Checked = false;
                button5.Checked = false;
            }
        }

        private void button4_CheckedChanged(object sender, EventArgs e)
        {
            if (button4.Checked)
            {
                button2.Checked = false;
                button3.Checked = false;
                button5.Checked = false;
            }

        }

        private void button5_CheckedChanged(object sender, EventArgs e)
        {
            if (button5.Checked)
            {
                button2.Checked = false;
                button3.Checked = false;
                button4.Checked = false;
            }
        }
    }
}
