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
    public partial class AddAccident2 : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = admin;" +
                                                 "Database = accidentes");

        int nLicenciaConductor;
        int userID;


        public AddAccident2(int licencia, int userID)
        {
            InitializeComponent();
            this.nLicenciaConductor = licencia; //Recibimos la llave foranea que es el No. de licencia
            this.userID = userID;

            radioButton_SeguroSi.Checked = true;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            miConexion.Open();

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

            string matricula = textBox_matricula.Text;

            commandVehiculo.Parameters.AddWithValue("matricula", matricula);
            commandVehiculo.Parameters.AddWithValue("modelo", textBox_modelo.Text);
            commandVehiculo.Parameters.AddWithValue("tipo", comboBox_tipovehiculo.Text);
            commandVehiculo.Parameters.AddWithValue("asegurado", asegurado);
            commandVehiculo.Parameters.AddWithValue("numeroLicenciaConductor", nLicenciaConductor); //LLave foranea

            commandVehiculo.ExecuteNonQuery();

            miConexion.Close();

            //Cerramos la ventana actual y abrimos la del siguiente formulario
            this.Close();

            Form form = new AddAccident3(matricula, nLicenciaConductor,userID);

            form.Show();
        }
    }
}
