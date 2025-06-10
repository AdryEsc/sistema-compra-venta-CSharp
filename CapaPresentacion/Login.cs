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
        private string textoReal = "";
        private bool estaOculto = true;


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
                if (auxUsuario != null && auxUsuario.Estado == 0) {
                    MessageBox.Show("Usuario NO ACTIVO, por favor comuniquese con el administrador", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
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

        private void Login_Load(object sender, EventArgs e)
        {
            txtContrasenia.PasswordChar = '*';
        }

        private void btnMostrarContasenia_Click(object sender, EventArgs e)
        {
            if (txtContrasenia.PasswordChar == '*')
            {
                txtContrasenia.PasswordChar = '\0';
                btnMostrarContasenia.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
            }
            else
            {
                txtContrasenia.PasswordChar = '*';
                btnMostrarContasenia.IconChar = FontAwesome.Sharp.IconChar.Eye;
            }

            /*
            if (btnMostrarContasenia.IconChar == FontAwesome.Sharp.IconChar.Eye)
            {
                txtContrasenia.UseSystemPasswordChar = false;
            }
            else {
                //txtContrasenia.UseSystemPasswordChar = true;
                txtContrasenia.PasswordChar = '*';
                btnMostrarContasenia.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
            }
            */
        }

        private void txtContrasenia_TextChanged(object sender, EventArgs e)
        {
            ////Solo actualizar si esta escribiendo directamente
            //if (estaOculto) {
            //    //Evitar bucle al borrar o cambiar textoprogramaticamente
            //    if (txtContrasenia.Text.Length < textoReal.Length) {
            //        textoReal = textoReal.Substring(0,txtContrasenia.Text.Length);
            //    }
            //    else if(txtContrasenia.Text.Length > textoReal.Length) {
            //        string nuevoCaracter = txtContrasenia.Text.Substring(txtContrasenia.Text.Length - 1);
            //        textoReal += nuevoCaracter;
            //    }
            //    //Reemplazar texto visible con asteriscos
            //    txtContrasenia.Text = new string('*',textoReal.Length);
            //    txtContrasenia.SelectionStart = txtContrasenia.Text.Length;
            //}
            //else{
            //    textoReal = txtContrasenia.Text;
            //}
        }
    }
}
