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
    public partial class AddAccidentOld : Form
    {
        //Creacion de conexion
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");
        public AddAccidentOld()
        {
            InitializeComponent();

        }

        //Boton para enviar datos
        private void button1_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Paso 1. Encontrar el ID del usuario que esta realizando el reporte -------------------------------------------------------------------------------------

            int userID;

            string queryUser = "select id_usuario from usuario where nombre = '" + Program.username + "' ";

            NpgsqlCommand commandUser = new NpgsqlCommand(queryUser, miConexion);

            NpgsqlDataAdapter dataUser = new NpgsqlDataAdapter(commandUser);

            DataTable userTable = new DataTable();

            dataUser.Fill(userTable);

            //La consulta solo regresa una columna con el ID, columna 0 y fila 0 porque es la primera
            userID = int.Parse(userTable.Rows[0][0].ToString());


            //Paso 2. Ingresar los datos del conductor -------------------------------------------------------------------------------------
            bool cinturon = false;
            bool seguro = false;
            string estado = string.Empty;

            //Validar radio buttons y comboBox asignarle S,M, etc
            if (radioButton_Si.Checked == true)
            {
                cinturon = true;
            }
            else if (radioButton_No.Checked == true)
            {
                cinturon = false;
            }

            if (radioButton2_SeguroSi.Checked == true)
            {
                seguro = true;
            } else if (radioButton2_SeguroNo.Checked == true)
            {
                seguro = false;
            }

            if (comboBox_Estado.Text == "Ebrio")
            {
                estado = "E";
            } else if (comboBox_Estado.Text == "Sobrio")
            {
                estado = "S";
            } else if (comboBox_Estado.Text == "Drogado")
            {
                estado = "D";
            }

            string queryConductor = "insert into conductor values (@numero,@nombre,@cinturon,@edad,@seguro,@estado)";

            NpgsqlCommand commandConductor = new NpgsqlCommand(queryConductor, miConexion);

            commandConductor.Parameters.AddWithValue("numero", int.Parse(textBox_nLicencia.Text));
            commandConductor.Parameters.AddWithValue("nombre", textBox_cName.Text);
            commandConductor.Parameters.AddWithValue("cinturon", cinturon);
            commandConductor.Parameters.AddWithValue("edad", int.Parse(textBox_edad.Text));
            commandConductor.Parameters.AddWithValue("seguro", seguro);
            commandConductor.Parameters.AddWithValue("estado", estado);

            commandConductor.ExecuteNonQuery();

            //Paso 3. Ingresar datos del Vehiculo -------------------------------------------------------------------------------------

            string queryVehiculo = "insert into vehiculo values (@matricula,@modelo,@tipo,@asegurado,@numeroLicenciaConductor)";

            //Validar radio buttons del seguro
            bool asegurado = false;

            if (radioButton_SeguroSi.Checked == true)
            {
                asegurado = true;
            }
            else if (radioButton_SeguroNo.Checked == true)
            {
                asegurado = false;
            }

            NpgsqlCommand commandVehiculo = new NpgsqlCommand(queryVehiculo, miConexion);

            commandVehiculo.Parameters.AddWithValue("matricula", textBox_matricula.Text);
            commandVehiculo.Parameters.AddWithValue("modelo", textBox_modelo.Text);
            commandVehiculo.Parameters.AddWithValue("tipo", comboBox_tipovehiculo.Text);
            commandVehiculo.Parameters.AddWithValue("asegurado", asegurado);
            commandVehiculo.Parameters.AddWithValue("numeroLicenciaConductor", int.Parse(textBox_nLicencia.Text));

            commandVehiculo.ExecuteNonQuery();

            //Paso 4. Ingresar datos del pasajero -------------------------------------------------------------------------------------

            string queryPasajero = "insert into pasajero values (@nombre,@cinturon,@cantidad,@asiento,@curp,@matriculaVehiculo)";

            bool pasajeroCinturon = false;

            if (radioButton_pasajeroSi.Checked == true)
            {
                pasajeroCinturon = true;
            }
            else if (radioButton_pasajeroNo.Checked == true)
            {
                pasajeroCinturon = false;
            }

            NpgsqlCommand commandPasajero = new NpgsqlCommand(queryPasajero, miConexion);

            commandPasajero.Parameters.AddWithValue("nombre", textBox_pasajeroNombre.Text);
            commandPasajero.Parameters.AddWithValue("cinturon", pasajeroCinturon);
            commandPasajero.Parameters.AddWithValue("cantidad", int.Parse(textBox_pasajeroCantidad.Text));
            commandPasajero.Parameters.AddWithValue("asiento", int.Parse(textBox_pasajeroAsiento.Text));
            commandPasajero.Parameters.AddWithValue("curp", textBox1_pasajeroCURP.Text);
            commandPasajero.Parameters.AddWithValue("matriculaVehiculo", textBox_matricula.Text);

            commandPasajero.ExecuteNonQuery();

            //Paso 5. Ingresar datos de la ubicacion -------------------------------------------------------------------------------------

            string queryUbicacion = "insert into ubicacion (estado,municipio,colonia,calle) values(@estado,@municipio,@colonia,@calle)";

            NpgsqlCommand commandUbicacion = new NpgsqlCommand(queryUbicacion, miConexion);

            commandUbicacion.Parameters.AddWithValue("estado", textBox_Estado.Text);
            commandUbicacion.Parameters.AddWithValue("municipio", textBox_Municipio.Text);
            commandUbicacion.Parameters.AddWithValue("colonia", textBox_Colonia.Text);
            commandUbicacion.Parameters.AddWithValue("calle", textBox_Calle.Text);

            commandUbicacion.ExecuteNonQuery();

            //con CURRVAl, como el ID es serial, esto nos devuelve el ID del del registro que acabamos de ingresar
            //Saco aqui el ID porque CURRVAL solo funciona con la consulta que se acaba de ejecutar
            int ubicacionID;
            string queryUbicacionID = "select currval('ubicacion_id_seq')";

            NpgsqlCommand commandUbicacionID = new NpgsqlCommand(queryUbicacionID, miConexion);

            NpgsqlDataAdapter dataUbicacion = new NpgsqlDataAdapter(commandUbicacionID);

            DataTable ubicacionTable = new DataTable();

            dataUbicacion.Fill(ubicacionTable);

            //La consulta solo regresa una columna con el ID, columna 0 y fila 0 porque es la primera
            ubicacionID = int.Parse(ubicacionTable.Rows[0][0].ToString());

            //Paso 6. Ingresar datos del oficial -------------------------------------------------------------------------------------

            string queryOficial = "insert into oficial values (@numPlaca,@cargo,@nombre)";

            NpgsqlCommand commandOficial = new NpgsqlCommand(queryOficial, miConexion);

            commandOficial.Parameters.AddWithValue("numPlaca", int.Parse(textBox_placa.Text));
            commandOficial.Parameters.AddWithValue("cargo", textBox_cargo.Text);
            commandOficial.Parameters.AddWithValue("nombre", textBox_nombreOficial.Text);

            commandOficial.ExecuteNonQuery();

            //Paso 7. Ingresar informacion de las afectaciones -------------------------------------------------------------------------------------

            string queryConsecuencia = "insert into consecuencia values (@Vialidad,@Vehiculo,@Conductor,@Pasajero)";

            NpgsqlCommand commandConsecuencia = new NpgsqlCommand(queryConsecuencia, miConexion);

            commandConsecuencia.Parameters.AddWithValue("Vialidad", textBox_vialidad.Text);
            commandConsecuencia.Parameters.AddWithValue("Vehiculo", textBox_vehiculo.Text);
            commandConsecuencia.Parameters.AddWithValue("Conductor", textBox_conductor.Text);
            commandConsecuencia.Parameters.AddWithValue("Pasajero", textBox_pasajero.Text);

            commandConsecuencia.ExecuteNonQuery();

            //Hacemos que nos devuelva el ID del registro que acabamos de insertar

            int consecuenciaID;
            string queryConsecuenciaID = "select currval('consecuencia_id_seq')";

            NpgsqlCommand commandConsecuenciaID = new NpgsqlCommand(queryConsecuenciaID, miConexion);

            NpgsqlDataAdapter dataConsecuencia = new NpgsqlDataAdapter(commandConsecuenciaID);

            DataTable consecuenciaTable = new DataTable();

            dataConsecuencia.Fill(consecuenciaTable);

            consecuenciaID = int.Parse(consecuenciaTable.Rows[0][0].ToString());

            //Paso 8. Ingresar datos del accidente -------------------------------------------------------------------------------------

            //Para esto necesitamos 4 llaves foraneas ID_Usuario,ID_Ubicacion,ID_Oficial,ID_Consecuencia

            string queryAccidente = "insert into accidente (hora,fecha,id_usuario,id_ubicacion,id_oficial,id_consecuencia) values (@hora,@fecha,@id_usuario,@id_ubicacion,@id_oficial,@id_consecuencia)";

            NpgsqlCommand commandAccidente = new NpgsqlCommand(queryAccidente, miConexion);
            commandAccidente.Parameters.AddWithValue("hora", DateTime.Parse(hora.Text));
            commandAccidente.Parameters.AddWithValue("fecha",DateTime.Parse(fecha.Text));
            commandAccidente.Parameters.AddWithValue("id_usuario", userID);
            commandAccidente.Parameters.AddWithValue("id_ubicacion", ubicacionID);
            commandAccidente.Parameters.AddWithValue("id_oficial", int.Parse(textBox_placa.Text));
            commandAccidente.Parameters.AddWithValue("id_consecuencia", consecuenciaID);

            commandAccidente.ExecuteNonQuery();

            /* Orden para eliminar tabla x tabla
             * select * from usuario; --1
                select * from conductor; --2
                select * from vehiculo; --3
                select * from pasajero; --4
                select * from ubicacion; --5
                select * from oficial; --6
                select * from consecuencia; --7
                select * from accidente; --8
            */

            MessageBox.Show("Accidente registrado, Gracias por el reporte " + Program.username + " :)");

   
            miConexion.Close();
        }
    }
}
