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
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        //Checkbox agregar contraseña
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
            }
        }

        //Boton cancelar
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Boton registro
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            PostgresConnection miConexion = new PostgresConnection();

            //Validamos campos vacios
            if(textBox_user.Text == string.Empty ||
                textBox1.Text == string.Empty ||
                  textBox2.Text == string.Empty)
            {
                MessageBox.Show("El formulario no puede tener campos vacios","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            //Validamos que las contraseñas estan bien
            if(textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden","Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            miConexion.AddUser(textBox_user.Text, textBox1.Text, "usuario");

            //Cerramos la ventana de registro
            this.Close();
        }
    }
}
