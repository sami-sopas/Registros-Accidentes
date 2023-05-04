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
    public partial class Login : Form
    {
        //Instanciamos la clase conexion
        PostgresConnection miConexion = new PostgresConnection();
        public Login()
        {
            InitializeComponent();
            Program.logueado = false; //Cada que se ejecute la ventana, volvemos a no logueado
            Program.username = string.Empty; //Lo mismo para el nombre de usuario

            textBox_user.Focus(); //Empezar el programa con teclado en usuario
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form SeeAccidentsWindow = new UserView();

            this.Hide();
            SeeAccidentsWindow.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Form SeeAccidents = new UserView();

            miConexion.logear(textBox_user.Text, textBox1.Text);

            ///EN PROGRAM.CS DECLARE UNA VARIABLE GLOBAL PARA SABER SI ESTA LOGUEADO UN USUARIO O NO
            Program.logueado = true;
            Program.username = textBox_user.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form AddUserWindow = new AddUser();

            AddUserWindow.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Form SeeAccidents = new UserView();

                miConexion.logear(textBox_user.Text, textBox1.Text);

                this.Hide(); //Esconder la ventana de login

                ///EN PROGRAM.CS DECLARE UNA VARIABLE GLOBAL PARA SABER SI ESTA LOGUEADO UN USUARIO O NO
                Program.logueado = true;
                Program.username = textBox_user.Text;


                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }


        private void textBox_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox1.Focus();
                e.SuppressKeyPress = true;
            }
        }

        //Funcion para desenmascarar la contraseña
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar= true;
            }
        }

        //Para cuando cierre el formulario del login: Darle finalizacion al programa en general
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
