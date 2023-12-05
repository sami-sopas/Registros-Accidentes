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
using System.Windows.Markup;

namespace ProyectoGriselda2._0
{
    public partial class Vehicles : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = usuario;" +
                                                 "Database = accidentes");
        public Vehicles()
        {
            InitializeComponent();

            comboBox_Buscar.Text = "Matricula"; //Para que los comboBox tengan un valor predeterminado
            comboBox_Tipo.Text = "Automovil";

            Consultar(); //LLenar la tabla al iniciar la ventana
        }

        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string queryConsulta = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            /*
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
            */

            miConexion.Close();
        }


        //Boton agregar nuevo vehiculo
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            string queryVehicle = "insert into vehiculo values (@matricula,@modelo,@tipo,@asegurado,@numeroLicenciaConductor)";

            //Validar Radio Buttons de Seguro
            bool estaAsegurado = false;


            if (radioButton_Si.Checked == true)
            {
                estaAsegurado = true;
            }
            else if (radioButton_No.Checked == true)
            {
                estaAsegurado = false;
            }

            NpgsqlCommand command = new NpgsqlCommand(queryVehicle, miConexion);

            command.Parameters.AddWithValue("matricula", txtMatricula.Text);
            command.Parameters.AddWithValue("modelo",txtModelo.Text);
            command.Parameters.AddWithValue("tipo", comboBox_Tipo.Text);
            command.Parameters.AddWithValue("asegurado", estaAsegurado);
            command.Parameters.AddWithValue("numeroLicenciaConductor", int.Parse(txtNumero.Text));

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar vehiculo
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Conseguir la matricula (PK) de el vehiculo
            string matricula = dataGridView1.SelectedCells[0].Value.ToString();


            string queryUpdate = "update vehiculo set modelo = @modelo, tipo = @tipo, asegurado = @asegurado where matricula = '" + matricula + "' ";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);


            command.Parameters.AddWithValue("modelo", txtModelo.Text);
            command.Parameters.AddWithValue("tipo", comboBox_Tipo.Text);

            bool asegurado = false;

            if (radioButton_Si.Checked == true)
            {
                asegurado = true;
            }
            else if (radioButton_No.Checked == true)
            {
                asegurado = false;
            }

            command.Parameters.AddWithValue("asegurado", asegurado);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }


        //Boton para buscar algun registro
        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "Matricula")
            {
                string queryMatricula = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia,nombre from vehiculo,conductor where vehiculo.num_licencia_conductor = conductor.num_licencia and matricula = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryMatricula, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable); 

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Modelo")
            {
                string queryModelo = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia,nombre from vehiculo,conductor where vehiculo.num_licencia_conductor = conductor.num_licencia and modelo = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryModelo, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else if (comboBox_Buscar.Text == "Numero de Licencia")//Buscar por el numero de licencia
            {
                string queryLicencia = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia,nombre from vehiculo,conductor where vehiculo.num_licencia_conductor = conductor.num_licencia and num_licencia_conductor = " + Int32.Parse(textBox_Buscar.Text) + " ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryLicencia, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por el nombre del conducor
            {
                string queryNombreConductor = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia,nombre from vehiculo,conductor where vehiculo.num_licencia_conductor = conductor.num_licencia and nombre = '" + textBox_Buscar.Text + "' ";

            }

            miConexion.Close();
        }

        //Evento para cuando demos click en una fila, se muestren los datos de ese registro a la izquierda
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //En este caso no usaremos los datos que tenemos en el datagrid...

            //txtMatricula.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //txtModelo.Text = dataGridView1.SelectedCells[0].Value.ToString();

            // Crearemos otra consulta sql por detras usando innerJoin para
            // que en base a la llave foranea se muestre el nombre del conductor

            string query = "select matricula,modelo,tipo,asegurado,num_licencia,nombre from vehiculo,conductor where vehiculo.num_licencia_conductor = conductor.num_licencia";

            //Creamos un comando sql para mandarle la consulta
            NpgsqlCommand command = new NpgsqlCommand(query, miConexion);

            //Creamos el dataAdapter y le mandamos el comando
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);

            //LLenamos una dataTable con los datos que nos trajimos de la consulta
            DataTable table = new DataTable();
            dataAdapter.Fill(table);

            //Ya tenemos nuestra tabla virtual pero estra trae todas las columnas

            //Para asegurarnos de que la seleccionada coincida con la que queremos mostrar, hacemos una validacion
            //Veamos que coincidan las llaves primarias de esa tabla

            for(int i = 0; i < table.Rows.Count; i++)
            {
                if(dataGridView1.SelectedCells[0].Value.ToString() == table.Rows[i][0].ToString())
                {
                    txtMatricula.Text = table.Rows[i][0].ToString();
                    txtModelo.Text = table.Rows[i][1].ToString();
                    comboBox_Tipo.Text = table.Rows[i][2].ToString();
                    //radioButton_Si.Text = table.Rows[i][3].ToString(); // asegurado?
                   
                    if (table.Rows[i][3].ToString()  == "False")
                    {
                        radioButton_No.Checked = true; 
                    }
                    else
                    {
                        radioButton_Si.Checked = true; 
                    }

                    txtNumero.Text = table.Rows[i][4].ToString();
                    txtNombre.Text = table.Rows[i][5].ToString();

                }
            }
        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            txtMatricula.Text = string.Empty;
            txtModelo.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtNombre.Text= string.Empty;
        }

        //Mostrar motocicletas
        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            string query = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo where tipo = 'Motocicleta'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar automoviles
        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            string query = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo where tipo = 'Automovil'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar camionetas
        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            string query = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo where tipo = 'Camioneta'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar camiones
        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            string query = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo where tipo = 'Camion'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar asegurados
        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            string query = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo where asegurado = 'True'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Mostrar no asegurados
        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            string query = "select matricula as Matricula,modelo as Modelo,tipo as Tipo,asegurado as Seguro,num_licencia_conductor as Licencia from vehiculo where asegurado = 'False'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }



        private void textBox_Buscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_Buscar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

 

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            Consultar();
            limpiar();
        }




        /*
        private void comboBox_Orden_SelectedValueChanged(object sender, EventArgs e)
        {
            Consultar();
        }
        */

    }
}
