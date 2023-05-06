using FontAwesome.Sharp; //Referencia de los iconos
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGriselda2._0
{
    public partial class UserView : Form
    {
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null; //Indica el formulario activo en el panel
        public UserView(string name)
        {
            InitializeComponent();
            bienvenido.Text = "Bienvenido " + name;
            labelMenu.Text = string.Empty;
        }

        //Este metodo recibe el menu el cual ha sido clickeado y el formulario que se desea mostrar
        private void abrirFormulario(IconMenuItem menu,Form formulario)
        {
            if(MenuActivo != null) //Si en el menu activo hay uno seleccionado
            {
                //Para que cada menu seleccionado se pone de color blanco
                MenuActivo.BackColor = menu.BackColor;
            }

            //Menu a mostrar, que se marque de un color diferente
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            //Mostrar el formulario correcto
            if(FormularioActivo != null) //Si ya hay uno abierto
            {
                FormularioActivo.Close();
            }

            //el form que quiero que se muestre
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            //formulario.BackColor = Color.White;

            contenedor.Controls.Add(formulario); //agregar el formulario al contenedor
            formulario.Show();

        }


        //Boton para ver accidentes
        private void iconMenuItem1_Click_1(object sender, EventArgs e)
        {
            //Casteamos el sender (menu seleccionado) y pasamos el form de ver los accidentes
            abrirFormulario((IconMenuItem)sender, new AccidentsUser());
            labelMenu.Text = "Reportes de Accidentes";
        }

        //Boton para ver el mapa
        private void iconMenuItem2_Click_1(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Map());
            labelMenu.Text = "Mapa";
        }

        //Boton para ver la lista de oficiales
        private void iconMenuItem3_Click_1(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new OfficersUser());
            labelMenu.Text = "Lista de Oficiales";
        }


        //Boton para ver las estadisticas
        private void iconMenuItem4_Click_1(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new StatsUser());
            labelMenu.Text = "Reportes";
        }

        //Boton para ver el acerca de

        private void iconMenuItem5_Click_1(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new AboutUs());
            labelMenu.Text = "Sobre Nosotros";
        }

        //Evento cuando cierra la ventana, se vuelva abrir el del login
        private void UserView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = new Login();

            form.Show();
        }
    }
}
