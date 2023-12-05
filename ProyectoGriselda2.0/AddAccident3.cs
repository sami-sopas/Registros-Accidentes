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
    public partial class AddAccident3 : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = usuario;" +
                                                 "Database = accidentes");

        string matricula_carro = string.Empty;
        int userID;
        int nLicenciaConductor;

        public AddAccident3(string matricula,int nLicenciaConductor,int userID)
        {
            InitializeComponent();
            this.matricula_carro = matricula; //Recibimos la llave foranea que es la matricula del carro
            this.userID = userID;
            this.nLicenciaConductor = nLicenciaConductor;

            radioButton_pasajeroSi.Checked = true;
            button2.Checked = false;
           
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            miConexion.Open();

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

            NpgsqlCommand commandPasajero = new NpgsqlCommand(queryPasajero, miConexion);

            commandPasajero.Parameters.AddWithValue("nombre", txtNombre.Text);
            commandPasajero.Parameters.AddWithValue("cinturon", pasajeroCinturon);
            commandPasajero.Parameters.AddWithValue("cantidad", int.Parse(txtCantidad.Text));
            commandPasajero.Parameters.AddWithValue("asiento", asiento);
            commandPasajero.Parameters.AddWithValue("curp", txtCurp.Text);
            commandPasajero.Parameters.AddWithValue("matriculaVehiculo", matricula_carro); //Llave foranea

            commandPasajero.ExecuteNonQuery();

            miConexion.Close();

            //Cerramos la ventana actual y abrimos la del siguiente formulario
            this.Close();

            Form form = new AddAccident4(nLicenciaConductor,userID);

            form.ShowDialog();
        }

        //Eventos para que solo se pueda activar un boton
        private void button2_CheckedChanged(object sender, EventArgs e)
        {
            if (button2.Checked)
            {
                button3.Checked = false;
                button4.Checked = false;
                button5.Checked = false;
            }
        }


        private void button3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (button3.Checked)
            {
                button2.Checked = false;
                button4.Checked = false;
                button5.Checked = false;
            }
        }

        private void button4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (button4.Checked)
            {
                button2.Checked = false;
                button3.Checked = false;
                button5.Checked = false;
            }

        }

        private void button5_CheckedChanged_1(object sender, EventArgs e)
        {
            if (button5.Checked)
            {
                button2.Checked = false;
                button3.Checked = false;
                button4.Checked = false;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
