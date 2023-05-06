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

//Liberias para el pdf
using iTextSharp.text; 
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Runtime.CompilerServices;

namespace ProyectoGriselda2._0
{
    public partial class Map : Form
    {
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = proyectogriselda.postgres.database.azure.com;" +
                                                         "User Id = postgres;" +
                                                         "Password = Admin1234;" +
                                                         "Database = accidentes");



        public Map()
        {
            InitializeComponent();

            //Codigo para que el pin no tenga fondo y se vea como PNG
            pictureBox1.Controls.Add(pin);
            pin.Location = new Point(0, 0);
            pin.BackColor = Color.Transparent;

        }

        //Variables necesarias chistosas
        string query = string.Empty;

        NpgsqlDataAdapter adapter;

        DataTable table;


        //Baja California Norte
        private void botonCircular2_Click(object sender, EventArgs e)
        {
            aparecerBotones(); //Funcion que "reinicia" los botones, los manda todos para el frente

            pin.Location = new Point(47, 200); //Localizamos el pin en la localizacion del estado

            botonCircular2.SendToBack(); //Quita el boton para que solo se quede el Pin

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Baja California';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Baja California";
        }

        //Baja California Sur
        private void botonCircular3_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(95, 305);

            botonCircular3.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Baja California Sur';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Baja California Sur";
        }

        //Sonora
        private void botonCircular5_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(155, 255);

            botonCircular5.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Sonora';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Sonora";
        }

        //Chihuahua
        private void botonCircular6_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(250, 283);

            botonCircular6.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Chihuahua';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Chihuahua";
        }

        //Coahuilda
        private void botonCircular7_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(337, 330);

            botonCircular7.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Coahuila';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Coahuila";
        }

        //Nuevo Leon
        private void botonCircular8_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(393, 360);

            botonCircular8.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Nuevo León';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Nuevo León";
        }

        //Tamaulipas
        private void botonCircular9_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(417, 403);

            botonCircular9.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Tamaulipas';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Tamaulipas";
        }

        //Sinaloa
        private void botonCircular10_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(200, 350);

            botonCircular10.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Sinaloa';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Sinaloa";
        }

        //Durango
        private void botonCircular11_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(285, 380);

            botonCircular11.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Durango';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Durango";
        }

        //Zacatecas
        private void botonCircular12_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(323, 410);

            botonCircular12.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Zacatecas';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Zacatecas";
        }

        //San Luis Potosi
        private void botonCircular13_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(374, 442);

            botonCircular13.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'San Luis Potosí';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "San Luis Potosí";
        }

        //Nayarit
        private void botonCircular14_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(275, 460);

            botonCircular14.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Nayarit';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Nayarit";

        }

        //Aguascalientes
        private void botonCircular15_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(332, 446);

            botonCircular15.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Aguascalientes';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Aguascalientes";
        }

        //Jalisco
        private void botonCircular1_Click_1(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(300, 490); //Localizamos el pin en la localizacion del estado

            botonCircular1.SendToBack(); //Quita el boton para que solo se quede el Pin

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Jalisco';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            //comboBox_Filtro.Text = "Jalisco";

        }

        //Guanajuato
        private void botonCircular16_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(360, 480);

            botonCircular16.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Guanajuato';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Guanajuato";
        }


        //Querétaro
        private void botonCircular17_Click_1(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(384, 480);

            botonCircular17.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Querétaro';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Querétaro";
        }

        //Hidalgo
        private void botonCircular18_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(406, 485);

            botonCircular18.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Hidalgo';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Hidalgo";
        }

        //Colima
        private void botonCircular19_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(295, 520);

            botonCircular19.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Colima';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Colima";
        }

        //Michoacan
        private void botonCircular20_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(340, 520);

            botonCircular20.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Michoacán';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Michoacán";
        }

        //Estado de Mexico
        private void botonCircular21_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(390, 510);

            botonCircular21.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Estado de México';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Estado de México";
        }

        //Ciudad de Mexico
        private void botonCircular22_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(404, 514);

            botonCircular22.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Ciudad de México';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Ciudad de México";
        }

        //Guerrero
        private void botonCircular24_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(385, 560);

            botonCircular24.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Guerrero';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Guerrero";
        }

        //Morelos
        private void botonCircular25_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(408, 530);

            botonCircular25.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Morelos';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Morelos";
        }

        //Tlaxcala
        private void botonCircular23_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(429, 514);

            botonCircular23.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Tlaxcala';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Tlaxcala";
        }

        //Veracruz
        private void botonCircular4_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(505, 548);

            botonCircular4.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Veracruz';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Veracruz";
        }

        //Oaxaca
        private void botonCircular26_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(470, 580);

            botonCircular26.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Oaxaca';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Oaxaca";
        }

        //Tabasco
        private void botonCircular28_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(555, 545);

            botonCircular28.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Tabasco';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Tabasco";
        }

        //Chiapas
        private void botonCircular29_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(580, 580);

            botonCircular29.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Chiapas';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Chiapas";
        }

        //Campeche
        private void botonCircular30_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(615, 520);

            botonCircular30.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Campeche';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Campeche";
        }

        //Yucatan
        private void botonCircular31_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(640, 480);

            botonCircular31.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Yucatán';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Yucatán";
        }

        //Quintana Roo
        private void botonCircular32_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(655, 512);

            botonCircular32.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Quintana Roo';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Quintana Roo";
        }

        //Puebla
        private void botonCircular33_Click(object sender, EventArgs e)
        {
            aparecerBotones();

            pin.Location = new Point(440, 530);

            botonCircular33.SendToBack();

            miConexion.Open();

            query = "select estado as \"Estado\",municipio as \"Municipio\",colonia as \"Colonia\",calle as \"Calle\" from ubicacion where estado = 'Puebla';";

            adapter = new NpgsqlDataAdapter(query, miConexion);

            table = new DataTable();

            adapter.Fill(table);

            dataGridView1.DataSource = table;

            miConexion.Close();

            comboBox_Filtro.Text = "Puebla";
        }

        //si apreta otro boton, se ejecuta esta funcion para que se regrese el boton que
        //fue enviado para atras y que asi vuelva a salir adelante
        private void aparecerBotones()
        {
            botonCircular1.BringToFront();
            botonCircular2.BringToFront();
            botonCircular3.BringToFront();
            botonCircular5.BringToFront();
            botonCircular6.BringToFront();
            botonCircular7.BringToFront();
            botonCircular8.BringToFront();
            botonCircular9.BringToFront();
            botonCircular10.BringToFront();
            botonCircular11.BringToFront();
            botonCircular12.BringToFront();
            botonCircular13.BringToFront();
            botonCircular14.BringToFront();
            botonCircular15.BringToFront();
            botonCircular16.BringToFront();
            botonCircular17.BringToFront();
            botonCircular18.BringToFront();
            botonCircular19.BringToFront();
            botonCircular20.BringToFront();
            botonCircular21.BringToFront();
            botonCircular22.BringToFront();
            botonCircular24.BringToFront();
            botonCircular25.BringToFront();
            botonCircular23.BringToFront();
            botonCircular4.BringToFront();
            botonCircular26.BringToFront();
            botonCircular28.BringToFront();
            botonCircular29.BringToFront();
            botonCircular30.BringToFront();
            botonCircular31.BringToFront();
            botonCircular32.BringToFront();
            botonCircular33.BringToFront();
        }

        //Evento para cuando seleccione otro estado del comboBox
        private void comboBox_Filtro_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox_Filtro.Text)
            {
                case "Aguascalientes":
                    botonCircular15_Click(null, null);
                    break;

                case "Baja California":
                    botonCircular2_Click(null, null);
                    break;

                case "Baja California Sur":
                    botonCircular3_Click(null, null);
                    break;

                case "Campeche":
                    botonCircular30_Click(null, null);
                    break;

                case "Chiapas":
                    botonCircular29_Click(null, null);
                    break;

                case "Chihuahua":
                    botonCircular6_Click(null, null);
                    break; ;

                case "Ciudad de México":
                    botonCircular22_Click(null, null);
                    break;

                case "Coahuila":
                    botonCircular7_Click(null, null);
                    break;

                case "Colima":
                    botonCircular19_Click(null, null);
                    break;

                case "Durango":
                    botonCircular11_Click(null, null);
                    break;

                case "Estado de México":
                    botonCircular21_Click(null, null);
                    break;

                case "Guanajuato":
                    botonCircular16_Click(null, null);
                    break;

                case "Guerrero":
                    botonCircular24_Click(null, null);
                    break;

                case "Hidalgo":
                    botonCircular18_Click(null, null);
                    break;

                case "Jalisco":
                    botonCircular1_Click_1(null, null);
                    break;

                case "Michoacán":
                    botonCircular20_Click(null, null);
                    break;

                case "Morelos":
                    botonCircular25_Click(null, null);
                    break;

                case "Nayarit":
                    botonCircular14_Click(null, null);
                    break;

                case "Nuevo León":
                    botonCircular8_Click(null, null);
                    break;

                case "Oaxaca":
                    botonCircular26_Click(null, null);
                    break;

                case "Puebla":
                    botonCircular33_Click(null, null);
                    break;

                case "Querétaro":
                    botonCircular17_Click_1(null, null);
                    break;

                case "Quintana Roo":
                    botonCircular32_Click(null, null);
                    break;

                case "San Luis Potosí":
                    botonCircular13_Click(null, null);
                    break;

                case "Sinaloa":
                    botonCircular10_Click(null, null);
                    break;

                case "Sonora":
                    botonCircular5_Click(null, null);
                    break;

                case "Tabasco":
                    botonCircular28_Click(null, null);
                    break;

                case "Tamaulipas":
                    botonCircular9_Click(null, null);
                    break;

                case "Tlaxcala":
                    botonCircular23_Click(null, null);
                    break;

                case "Veracruz":
                    botonCircular4_Click(null, null);
                    break;

                case "Yucatán":
                    botonCircular31_Click(null, null);
                    break;

                case "Zacatecas":
                    botonCircular12_Click(null, null);
                    break;

            }


        }
    }
    
}
