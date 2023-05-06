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
    public partial class Users : Form
    {
        //Creamos la conexion
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");
        public Users()
        {
            InitializeComponent();

            
            Consultar(); //LLenar la tabla al iniciar la ventana

            comboBox_Buscar.Text= "Nombre"; //Para que los comboBox tengan un valor predeterminado
            comboBox_Rol.Text = "Usuario";
        }


        
        //Funcion que actualiza (consulta) los datos de la tabla
        private void Consultar()
        {
            miConexion.Open();

            string queryConsulta = "select id_usuario as ID, nombre as Nombre, password as Contraseña, tipo_usuario as Rol from usuario order by id_usuario ASC";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryConsulta,miConexion);

            DataTable dataTable = new DataTable(); //Tabla virtual en memoria

            //Pasar consulta a la tabla memoria
            adapter.Fill(dataTable);

            //Mostrar en el dataGrid
            dataGridView1.DataSource = dataTable;

            miConexion.Close();
        }


        //Boton agregar nuevo usuario
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            string queryAddUser = "insert into usuario (nombre,password,tipo_usuario) values (@nombre,@password,@tipo_usuario)";

            NpgsqlCommand command = new NpgsqlCommand(queryAddUser, miConexion);

            //validaciones...
            if (txtContrasenia.Text != txtConfirmContrasenia.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden !");
            }

            if (comboBox_Rol.Text == "Usuario")
            {
                command.Parameters.AddWithValue("tipo_usuario", "usuario");
            }
            else //(comboBox_Rol.Text == "Administrador")
            {
                command.Parameters.AddWithValue("tipo_usuario", "admin");
            }

            command.Parameters.AddWithValue("nombre", txtNombre.Text);
            command.Parameters.AddWithValue("password", txtContrasenia.Text);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar(); //dejar campos vacios

            Consultar(); //actualizar tabla (vuelve a abrir la conexion)
        }

        //Boton para actualizar registro
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //validar por si quiere cambiar la password
            if (txtContrasenia.Text != txtConfirmContrasenia.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden !");
            }

            //Conseguir ID del usuario a actualizar
            int id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryUpdate = "update usuario set nombre = @nombre, password = @password, tipo_usuario = @tipo where id_usuario = " + id + "";

            NpgsqlCommand command = new NpgsqlCommand(queryUpdate, miConexion);

            command.Parameters.AddWithValue("nombre", txtNombre.Text);
            command.Parameters.AddWithValue("password", txtContrasenia.Text);

            //validar x si quiere cambiar el rol
            if (comboBox_Rol.Text == "Usuario")
            {
                command.Parameters.AddWithValue("tipo", "usuario");
            }
            else //(comboBox_Rol.Text == "Administrador")
            {
                command.Parameters.AddWithValue("tipo", "admin");
            }

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }

        //Boton para eliminar registro
        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            miConexion.Open();

            //Agarramos el ID desde la columna seleccionada que se muestra en el dataGrid

            int id = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());

            string queryDelete = "delete from usuario where id_usuario = " + id + "";

            NpgsqlCommand command = new NpgsqlCommand(queryDelete, miConexion);

            command.ExecuteNonQuery();

            miConexion.Close();

            limpiar();

            Consultar();
        }

        //Boton para buscar algun registro
        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            if (comboBox_Buscar.Text == "ID")
            {
                string queryID = "select id_usuario as ID, nombre as Nombre, password as Contraseña, tipo_usuario as Rol from usuario where id_usuario = " + Int32.Parse(textBox_Buscar.Text) + "";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryID, miConexion);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            else //Buscar por nombre
            {
                string queryName = "select id_usuario as ID, nombre as Nombre, password as Contraseña, tipo_usuario as Rol from usuario where nombre = '" + textBox_Buscar.Text + "' ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryName, miConexion);

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
            txtContrasenia.Text = dataGridView1.SelectedCells[2].Value.ToString();
            txtConfirmContrasenia.Text = dataGridView1.SelectedCells[2].Value.ToString();

            if (dataGridView1.SelectedCells[3].Value.ToString() == "usuario")
            {
                comboBox_Rol.Text = "Usuario";
            }
            else
            {
                comboBox_Rol.Text = "Administrador";
            }
        }

        //Boton para mostrar solo administradores
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            string queryAdmin = "select id_usuario as ID, nombre as Nombre, password as Contraseña, tipo_usuario as Rol from usuario where tipo_usuario = 'admin'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryAdmin, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        //Boton para mostrar solo usuarios
        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            string queryUser = "select id_usuario as ID, nombre as Nombre, password as Contraseña, tipo_usuario as Rol from usuario where tipo_usuario = 'usuario'";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryUser, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }


        //Funcion para dejar los textBlocks en blanco
        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            txtConfirmContrasenia.Text = string.Empty;
            comboBox_Rol.Text = string.Empty;
            comboBox_Buscar.Text = string.Empty;
            textBox_Buscar.Text = string.Empty;
        }

   
        //Refrescar
        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            Consultar();
            limpiar();
        }
    }
}
