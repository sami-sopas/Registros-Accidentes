using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoGriselda2._0
{
    public partial class StatsUser : Form
    {
        //Creamos la conexion
        NpgsqlConnection miConexion = new NpgsqlConnection("Server = localhost;" +
                                                 "User Id = postgres;" +
                                                 "Password = usuario;" +
                                                 "Database = accidentes");

        public StatsUser()
        {
            InitializeComponent();

            CargarDatos();

            CargarDataGrid();

        }

        private void CargarDataGrid()
        {
            ///PDF
            miConexion.Open();

            string query = "select ubicacion.estado as \"Estado\"," +
                           "TO_CHAR(fecha, 'dd-mm-yyyy') as \"Fecha\"," +
                           "to_char(hora,'HH24:MI') as \"Hora\"," +
                           "vehiculo.matricula as \"Matricula\"," +
                           "conductor.nombre as \"Responsable\"," +
                           "oficial.nombre as \"Oficial\" " +
                           "from accidente" +
                           " join usuario on accidente.id_usuario = usuario.id_usuario" +
                           " join ubicacion on accidente.id_ubicacion = ubicacion.id" +
                           " join oficial on accidente.id_oficial = oficial.num_placa" +
                           " join conductor on accidente.id_conductor = conductor.num_licencia" +
                           " join vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            dataGridPdf.DataSource = dataTable;

            miConexion.Close();

            ///EXCEL

            miConexion.Open();

            string query2 = "select \r\nTO_CHAR(fecha, 'dd-mm-yyyy') as \"Fecha\",\r\nto_char(hora,'HH24:MI') as \"Hora\",\r\nusuario.nombre as \"Reportado por\",\r\nubicacion.estado as \"Estado\",\r\nubicacion.municipio as \"Municipio\",\r\nubicacion.colonia as \"Colonia\",\r\nubicacion.calle as \"Calle\",\r\noficial.num_placa as \"Numero de placa oficial\",\r\noficial.nombre as \"Nombre oficial\",\r\noficial.cargo as \"Cargo oficial\",\r\nconductor.num_licencia as \"Numero de licencia conductor\",\r\nconductor.nombre as \"Conductor\",\r\nconductor.cinturon as \"Cinturon\",\r\nconductor.edad as \"Edad\",\r\nconductor.seguro as \"Seguro\",\r\nconductor.estado as \"Estado\",\r\nvehiculo.matricula as \"Matricula vehiculo\",\r\nvehiculo.modelo as \"Modelo\",\r\nvehiculo.tipo as \"Tipo\",\r\nvehiculo.asegurado as \"Asegurado\",\r\npasajero.nombre as \"Nombre pasajero\",\r\npasajero.cinturon as \"Cinturon\",\r\npasajero.cantidad as \"No Pasajeros\",\r\npasajero.asiento as \"Num Asiento\",\r\npasajero.curp as \"CURP pasajero\",\r\nconsecuencia.vialidad as \"Vialidad afectada\",\r\nconsecuencia.pasajero as \"Pasajero afectado\",\r\nconsecuencia.conductor as \"Conductor\",\r\nconsecuencia.vehiculo as \"Vehiculo afectado\"\r\n\r\nfrom accidente\r\njoin usuario on accidente.id_usuario = usuario.id_usuario\r\njoin ubicacion on accidente.id_ubicacion = ubicacion.id\r\njoin oficial on accidente.id_oficial = oficial.num_placa\r\njoin conductor on accidente.id_conductor = conductor.num_licencia\r\njoin vehiculo on accidente.id_conductor = vehiculo.num_licencia_conductor\r\njoin pasajero on vehiculo.matricula = pasajero.matricula_carro\r\njoin consecuencia on accidente.id_consecuencia = consecuencia.id";

            NpgsqlDataAdapter adapter2 = new NpgsqlDataAdapter(query2, miConexion);

            DataTable dataTable2 = new DataTable();

            adapter2.Fill(dataTable2);

            dataGridExcel.DataSource = dataTable2;

            miConexion.Close();

        }

        void CargarDatos()
        {
            miConexion.Open();

            ///MOSTRAR LA GRAFICA DE BARRAS

            string query = "SELECT ub.estado, count(ac.id) as cantidad " +
                " FROM accidente ac " +
                "  JOIN ubicacion ub " +
                "   ON ub.id = ac.id_ubicacion " +
                "    GROUP BY ub.estado";
           
            DataTable table = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, miConexion);

            adapter.Fill(table);

            chart1.DataSource = table;
            chart1.Series["Series1"].LegendText = "Accidentes registrados por estados";
            chart1.Series["Series1"].XValueMember = "estado";
            chart1.Series["Series1"].YValueMembers = "cantidad";


            ///MOSTRAR ESTADISTICAS DE LADO IZQUIERDA

            //NUMERO TOTAL DE ACCIDENTES

            string query1 = "select count(*) from accidente";

            NpgsqlCommand command1 = new NpgsqlCommand(query1, miConexion);

            NpgsqlDataAdapter dataAdapter1 = new NpgsqlDataAdapter(command1);

            DataTable table1 = new DataTable();

            dataAdapter1.Fill(table1);

            txtAccidentes.Text = table1.Rows[0][0].ToString();

            //USUARIOS Y ADMINISTRADORES REGISTRADOS

            string query2 = "select tipo_usuario,count(*) from usuario group by tipo_usuario order by tipo_usuario";

            NpgsqlCommand command2 = new NpgsqlCommand(query2, miConexion);

            NpgsqlDataAdapter dataAdapter2 = new NpgsqlDataAdapter(command2);

            DataTable table2 = new DataTable();

            dataAdapter2.Fill(table2);

            txtUsers.Text = table2.Rows[0][1].ToString();
            txtAdmins.Text = table2.Rows[1][1].ToString();

            miConexion.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "AccidentesViales.pdf";

            //Convertimos la pagina web a cadena
            string pagina = Properties.Resources.plantilla.ToString();

            //Para generar numeros aletarios
            Random r = new Random();

            pagina = pagina.Replace("@USUARIO", Program.username);
            pagina = pagina.Replace("@DOCUMENTO", r.Next(10000, 99999).ToString());
            pagina = pagina.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));
            pagina = pagina.Replace("@No", r.Next(10000000, 99999999).ToString());

            string filas = string.Empty;

            //Lee todas las columnas del datagRid invisible
            foreach (DataGridViewRow row in dataGridPdf.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Estado"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Fecha"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Hora"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Matricula"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Responsable"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Oficial"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            pagina = pagina.Replace("@FILAS", filas);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                //nos permite guardar el archivo
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create)) //archivo de memoria
                {
                    //Definimos caracteristicas del documento (tamaño A4 y margenes)
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    //abrimos el documento
                    pdfDoc.Open();

                    //Incluimos una frase en el documento
                    pdfDoc.Add(new Phrase(""));

                    //Agregar logo al archivo pdf (imagen)
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.logo_kawaii_emprendimiento_divertido_moderno_gatito, System.Drawing.Imaging.ImageFormat.Png);

                    img.ScaleToFit(120, 120);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 80);
                    pdfDoc.Add(img);


                    //Incrustar html en el documento
                    using (StringReader sr = new StringReader(pagina))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();

                    stream.Close();
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Instanciamos la aplicacion
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Reportes de Accidentes";

            for (int i = 1; i < dataGridExcel.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridExcel.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dataGridExcel.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridExcel.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridExcel.Rows[i].Cells[j].Value.ToString();
                }
            }

            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = "AccidentesViales";
            saveFileDialoge.DefaultExt = ".xlsx";
            if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }

            app.Quit();
        }
    }
}
