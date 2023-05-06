using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoGriselda2._0
{
    class PostgresConnection
    {


        //Conexion anterior
        //NpgsqlConnection conexion = new NpgsqlConnection("Server = localhost;" +
        //                                         "User Id = postgres;" +
        //                                         "Password = admin;" +
        //                                         "Database = accidentes");

        NpgsqlConnection conexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");

        //Nombre del servidor: proyectogriselda
        //Nombre de usuario admin: postgres
        //password: Admin1234

        public void Open()
        {
            conexion.Open();

            //MessageBox.Show("Conectado a Postgres :)");
        }

        public void Close()
        {
            conexion.Close();

            //MessageBox.Show("Desconectado de Postgres");
        }

        //Metodo para loggearse
        public void logear(string user, string password)
        {
            try
            {
                //Abrimos la conexion a postgres
                Open();

                //Creamos el query que le haremos a la base de datos
                string query = "select nombre,tipo_usuario from usuario where nombre = @usu and password = @pas";

                //Creamos un comando sql para mandarle la consulta
                NpgsqlCommand command = new NpgsqlCommand(query, conexion);

                //Le pasamos los parametros de la variable que recibe este metodo
                command.Parameters.AddWithValue("usu", user);
                command.Parameters.AddWithValue("pas", password);

                //Creamos el dataAdaptes y le mandamos el comando ya con los parametros
                NpgsqlDataAdapter data = new NpgsqlDataAdapter(command);

                //LLenamos la dataTable con los datos que nos trajimos de la consulta
                DataTable table = new DataTable();
                data.Fill(table);


                //Si la consulta devuelve filas, es porque si existe un usuario que coincida con la info
                if(table.Rows.Count == 1) //Encontro filas(datos)
                {

                    //Row[0][1]
                    //0: Fila deseada
                    //1: Columna deseada
                    //La primera fila y columna es 0

                    //Verificamos si el que entro es usuario o admin

                    //Accedemos a la fila
                    //como queremos acceder a la columna tipo_usuario, le indicamos esto
                    if (table.Rows[0][1].ToString() == "admin")
                    {
                        //MessageBox.Show("Funciono admin");
                        //LLamar a la ventana admin

                        //Mostrar el nombre registrado en la tabla en el nuevo formulario
                        //new Admin(table.Rows[0][0].ToString().Show());

                        Form adminWindow = new AdminView(user);

                        adminWindow.Show();
                    }
                    else if(table.Rows[0][1].ToString() == "usuario")
                    {
                        //Llamar a la ventana para ver los accidentes
                        //Form SeeAccidentWindow = new SeeAccidents(table.Rows[0][0].ToString());

                        Form SeeAccidentWindow = new UserView(user);

                        SeeAccidentWindow.Show();

                    }


                }
                else //No encontro filas
                {
                    MessageBox.Show("Usuario y/o Contraseña Incorrecta");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message.ToString());
            }

            //Cerramos la conexion
            
            Close();
            

        }

        //Metodo para agregar usuarios tanto normales como administradores
        public void AddUser(string name,string password,string type_user)
        {
            Open();

            string query = "insert into usuario (nombre,password,tipo_usuario)" +
                " values ('"+name+"','"+password+"','"+type_user+"')";

            NpgsqlCommand command = new NpgsqlCommand( query, conexion);

            command.ExecuteNonQuery();

            Close();


        }



 


    }
}

