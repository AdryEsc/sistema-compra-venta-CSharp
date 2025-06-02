using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        //Instancia para validar textbox
        ValidacionTextBox validacionTextBox = new ValidacionTextBox();

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
                Usuario auxUsuario = new CN_Usuario().listarUsuarios().Where(u => u.Documento == txtDocumento.Text && u.Clave == txtContrasenia.Text).FirstOrDefault();
                if (auxUsuario.Estado == 0) {
                    MessageBox.Show("Usuario NO ACTIVO, por favor comuniquese con el administrador", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
                    Usuario oUsuario = new CN_Usuario().listarUsuarios().Where(u => u.Documento == txtDocumento.Text && u.Clave == txtContrasenia.Text && u.Estado == 1).FirstOrDefault();

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
        }
          

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            txtDocumento.Text = "";

            txtContrasenia.Text = "";

            this.Show();
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacionTextBox.soloNumeros(e);
        }
    }
}
