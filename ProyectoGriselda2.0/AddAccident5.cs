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
    public partial class AddAccident5 : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = usuario;" +
                                                 "Database = accidentes");

        int nLicenciaConductor;
        int consecuenciaID;
        int userID;


        public AddAccident5(int nLicenciaConductor,int consecuenciaID, int userID)
        {
            InitializeComponent();

            this.nLicenciaConductor = nLicenciaConductor;
            this.consecuenciaID = consecuenciaID;
            this.userID = userID;

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            miConexion.Open();

            //Paso 6. Ingresar datos del oficial -------------------------------------------------------------------------------------

            string queryOficial = "insert into oficial values (@numPlaca,@cargo,@nombre)";

            NpgsqlCommand commandOficial = new NpgsqlCommand(queryOficial, miConexion);

            int numPlaca = int.Parse(txtNumPlaca.Text);

            commandOficial.Parameters.AddWithValue("numPlaca", numPlaca);
            commandOficial.Parameters.AddWithValue("cargo", txtCargo.Text);
            commandOficial.Parameters.AddWithValue("nombre", txtNombre.Text);

            commandOficial.ExecuteNonQuery();

            miConexion.Close();

            //Cerramos la ventana actual y abrimos la del siguiente formulario
            this.Close();

            Form form = new AddAccident6(nLicenciaConductor,consecuenciaID,numPlaca,userID);

            form.ShowDialog();
        }

        private void txtNumPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
