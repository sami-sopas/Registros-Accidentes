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
    public partial class AddAccident1 : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");

        public AddAccident1()
        {
            InitializeComponent();

            radioButton_Si.Checked = true;
            radioButton_SeguroSi.Checked = true;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
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

            if (radioButton_SeguroSi.Checked == true)
            {
                seguro = true;
            }
            else if (radioButton_SeguroNo.Checked == true)
            {
                seguro = false;
            }

            if (comboBox_Estado.Text == "Ebrio")
            {
                estado = "E";
            }
            else if (comboBox_Estado.Text == "Sobrio")
            {
                estado = "S";
            }
            else if (comboBox_Estado.Text == "Drogado")
            {
                estado = "D";
            }

            string queryConductor = "insert into conductor values (@numero,@nombre,@cinturon,@edad,@seguro,@estado)";

            NpgsqlCommand commandConductor = new NpgsqlCommand(queryConductor, miConexion);

            int nLicencia = int.Parse(textBox_nLicencia.Text);

            commandConductor.Parameters.AddWithValue("numero", nLicencia);
            commandConductor.Parameters.AddWithValue("nombre", textBox_cName.Text);
            commandConductor.Parameters.AddWithValue("cinturon", cinturon);
            commandConductor.Parameters.AddWithValue("edad", int.Parse(textBox_edad.Text));
            commandConductor.Parameters.AddWithValue("seguro", seguro);
            commandConductor.Parameters.AddWithValue("estado", estado);

            commandConductor.ExecuteNonQuery();

            miConexion.Close();

            //Enviamos las llaves foraneas que usaran los siguientes forms
            Form form = new AddAccident2(nLicencia, userID);

            //Cerramos la ventana actual y abrimos la del siguiente formulario
            this.Close();

            form.ShowDialog();
        }

        //Para que solo se puedan escribir numeros
        private void textBox_nLicencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox_edad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
