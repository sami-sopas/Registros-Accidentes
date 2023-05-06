using FontAwesome.Sharp;
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
    public partial class AdminView : Form
    {
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null; //Indica el formulario activo en el panel

        public AdminView(string name)
        {
            InitializeComponent();
            bienvenido.Text = "Bienvenido Administrador " + name;
            labelMenu.Text = string.Empty;
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //Este metodo recibe el menu el cual ha sido clickeado y el formulario que se desea mostrar
        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null) //Si en el menu activo hay uno seleccionado
            {
                //Para que cada menu seleccionado se pone de color blanco
                MenuActivo.BackColor = menu.BackColor;
            }

            //Menu a mostrar, que se marque de un color diferente
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            //Mostrar el formulario correcto
            if (FormularioActivo != null) //Si ya hay uno abierto
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

        //Boton icono para el form de accidentes
        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            //Casteamos el sender (menu seleccionado) y pasamos el form de ver los accidentes
            abrirFormulario((IconMenuItem)sender, new AccidentsAdmin());
            labelMenu.Text = "Reportes de Accidentes";
        }

        //Boton icono para el form de usuarios
        private void iconMenuItem3_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Users());
            labelMenu.Text = "Lista de usuarios";
        }

        //Boton icono para el form de oficiales
        private void iconMenuItem2_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new OfficersUser());
            labelMenu.Text = "Lista de oficiales";

        }

        //Boton icono para el form de vehiculos
        private void iconMenuItem4_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Vehicles());
            labelMenu.Text = "Lista de vehiculos";
        }

        //Boton icono para el form de conductores
        private void iconMenuItem5_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Drivers());
            labelMenu.Text = "Lista de conductores";
        }

        //Boton icono para el form de pasajeros
        private void iconMenuItem6_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Passengers());
            labelMenu.Text = "Lista de pasajeros";
        }

        //Boton icono para el form de estadisticas
        private void iconMenuItem7_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new StatsAdmin());
            labelMenu.Text = "Estadisticas";
        }

        //Boton icono para el form de acerca de
        private void iconMenuItem8_Click(object sender, EventArgs e)
        {
           abrirFormulario((IconMenuItem)sender, new AboutUs());
            labelMenu.Text = "Sobre nosotros";
        }

        //Boton icono para form de ubicaciones
        private void iconMenuItem9_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Locations());
            labelMenu.Text = "Ubicaciones";
        }

        //Boton icono para form de consecuencias / afectasiones
        private void iconMenuItem10_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new Consequences());
            labelMenu.Text = "Consecuencias";
        }

        //Evento para cuando cierre la ventana, se abra de nuevo la de login
        private void AdminView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = new Login();

            form.Show();
        }
    }
}
