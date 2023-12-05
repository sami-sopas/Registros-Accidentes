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
    public partial class AddAccident4 : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = usuario;" +
                                                 "Database = accidentes");

        int nLicenciaConductor;
        int userID;

        public AddAccident4(int nLicenciaConductor,int userID)
        {
            InitializeComponent();

            this.nLicenciaConductor = nLicenciaConductor;
            this.userID = userID;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Paso 5. Ingresar informacion de las afectaciones -------------------------------------------------------------------------------------

            string queryConsecuencia = "insert into consecuencia values (@Vialidad,@Vehiculo,@Conductor,@Pasajero)";

            NpgsqlCommand commandConsecuencia = new NpgsqlCommand(queryConsecuencia, miConexion);

            commandConsecuencia.Parameters.AddWithValue("Vialidad", txtVialidad.Text);
            commandConsecuencia.Parameters.AddWithValue("Vehiculo", txtVehiculo.Text);
            commandConsecuencia.Parameters.AddWithValue("Conductor",txtConductor.Text);
            commandConsecuencia.Parameters.AddWithValue("Pasajero", txtPasajero.Text);

            commandConsecuencia.ExecuteNonQuery();

            //Hacemos que nos devuelva el ID del registro que acabamos de insertar

            int consecuenciaID;
            string queryConsecuenciaID = "select currval('consecuencia_id_seq')";

            NpgsqlCommand commandConsecuenciaID = new NpgsqlCommand(queryConsecuenciaID, miConexion);

            NpgsqlDataAdapter dataConsecuencia = new NpgsqlDataAdapter(commandConsecuenciaID);

            DataTable consecuenciaTable = new DataTable();

            dataConsecuencia.Fill(consecuenciaTable);

            consecuenciaID = int.Parse(consecuenciaTable.Rows[0][0].ToString());

            miConexion.Close();

            //Cerramos la ventana actual y abrimos la del siguiente formulario
            this.Close();

            Form form = new AddAccident5(nLicenciaConductor,consecuenciaID,userID);

            form.ShowDialog();
        }
    }
}
