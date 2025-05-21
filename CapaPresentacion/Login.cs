using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text == "" || txtContrasenia.Text == "")
            {
                MessageBox.Show("Debe ingresar ambos datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Usuario oUsuario = new CN_Usuario().listarUsuarios().Where(u => u.Documento == txtDocumento.Text && u.Clave == txtContrasenia.Text).FirstOrDefault();

                if (oUsuario != null)
                {
                    Inicio form = new Inicio(oUsuario);

                    form.Show();

                    this.Hide();

                    form.FormClosing += formClosing;
                }
                else
                {
                    MessageBox.Show("No se encontro el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
          

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            txtDocumento.Text = "";

            txtContrasenia.Text = "";

            this.Show();
        }
    }
}
