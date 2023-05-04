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
    public partial class AddAccident6 : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = admin;" +
                                                 "Database = accidentes");


        //Variables (llaves foraneas)
        int userID;
        int oficialID;
        int consecuenciaID;
        int conductorID;

        public AddAccident6(int conductorID,int consecuenciaID,int oficialID,int userID)
        {
            InitializeComponent();

            this.userID = userID;
            this.oficialID = oficialID;
            this.consecuenciaID = consecuenciaID;
            this.conductorID = conductorID;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            miConexion.Open();

            //JUNTE UBICACION CON ACCIDENTE EN REFERENCIA AL AddAccidentOld

            //Paso 7. Ingresar datos de la ubicacion -------------------------------------------------------------------------------------

            string queryUbicacion = "insert into ubicacion (estado,municipio,colonia,calle) values(@estado,@municipio,@colonia,@calle)";

            NpgsqlCommand commandUbicacion = new NpgsqlCommand(queryUbicacion, miConexion);

            commandUbicacion.Parameters.AddWithValue("estado", comboBox_Estado.Text);
            commandUbicacion.Parameters.AddWithValue("municipio", txtMunicipio.Text);
            commandUbicacion.Parameters.AddWithValue("colonia", txtColonia.Text);
            commandUbicacion.Parameters.AddWithValue("calle", txtCalle.Text);

            commandUbicacion.ExecuteNonQuery();

            //con CURRVAl, como el ID es serial, esto nos devuelve el ID del del registro que acabamos de ingresar
            //Saco aqui el ID  del accidente que acabos de ingresar porque CURRVAL solo funciona con la consulta que se acaba de ejecutar
            int ubicacionID;
            string queryUbicacionID = "select currval('ubicacion_id_seq')";

            NpgsqlCommand commandUbicacionID = new NpgsqlCommand(queryUbicacionID, miConexion);

            NpgsqlDataAdapter dataUbicacion = new NpgsqlDataAdapter(commandUbicacionID);

            DataTable ubicacionTable = new DataTable();

            dataUbicacion.Fill(ubicacionTable);

            //La consulta solo regresa una columna con el ID, columna 0 y fila 0 porque es la primera
            ubicacionID = int.Parse(ubicacionTable.Rows[0][0].ToString());

            //Paso 8. Ingresar datos del accidente -------------------------------------------------------------------------------------

            //Para esto necesitamos 5 llaves foraneas ID_Usuario,ID_Ubicacion,ID_Oficial,ID_Consecuencia,ID_Conductor

            string queryAccidente = "insert into accidente (hora,fecha,id_usuario,id_ubicacion,id_oficial,id_consecuencia,id_conductor) values (@hora,@fecha,@id_usuario,@id_ubicacion,@id_oficial,@id_consecuencia,@id_conductor)";

            NpgsqlCommand commandAccidente = new NpgsqlCommand(queryAccidente, miConexion);
            commandAccidente.Parameters.AddWithValue("hora", DateTime.Parse(hora.Text));
            commandAccidente.Parameters.AddWithValue("fecha", DateTime.Parse(fecha.Text));
            commandAccidente.Parameters.AddWithValue("id_usuario",userID);
            commandAccidente.Parameters.AddWithValue("id_ubicacion", ubicacionID);
            commandAccidente.Parameters.AddWithValue("id_oficial", oficialID);
            commandAccidente.Parameters.AddWithValue("id_consecuencia", consecuenciaID);
            commandAccidente.Parameters.AddWithValue("id_conductor", conductorID);
            
            commandAccidente.ExecuteNonQuery();

            miConexion.Close();

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

            MessageBox.Show("Accidente registrado, Gracias por el reporte " + Program.username + " :)","Registro exitoso",MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            //Cerramos la ventana actual y abrimos la del siguiente formulario
            this.Close();

        }

        
    }
}
