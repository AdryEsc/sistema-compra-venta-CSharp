using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaPresentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        //Instancia para validar textbox
        ValidacionTextBox validacionTextBox = new ValidacionTextBox();

        public frmUsuarios()
        {
            InitializeComponent();
            
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            //Cargamos el combobox de estado
            cbEstado.Items.Add(new OpcionComboBox() { valor = 1, texto = "Activo" });
            cbEstado.Items.Add(new OpcionComboBox() { valor = 0, texto = "No Activo" });

            cbEstado.DisplayMember = "texto";
            cbEstado.ValueMember = "valor";
            cbEstado.SelectedIndex = 0;

            //Cargamos el combobox de rol
            List<Rol> listaRoles = new CN_Rol().listarRoles();
            foreach (Rol item in listaRoles) { 
                cbRol.Items.Add(new OpcionComboBox() { valor = item.Id, texto = item.Descripcion });
            }

            cbRol.DisplayMember = "texto";
            cbRol.ValueMember = "valor";
            cbRol.SelectedIndex = 0;

            //Cargamos los usuarios a la tabla
            List<Usuario> listaUsuarios = new CN_Usuario().listarUsuarios();
            foreach (Usuario item in listaUsuarios)
            {
                dgvUsuarios.Rows.Add(new object[] {"",item.Id,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                item.oRol.Id,item.oRol.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto usuario con los datos del formulario
            Usuario user = new Usuario()
            {
                Id = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtContrasenia.Text,
                oRol = new Rol() {Id = Convert.ToInt32(((OpcionComboBox)cbRol.SelectedItem).valor)},
                Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            int idUsuarioGenerado = new CN_Usuario().registrarUsuario(user, out mensaje);

            if (idUsuarioGenerado != 0)
            {
                dgvUsuarios.Rows.Add(new object[] {"",idUsuarioGenerado,txtDocumento.Text,txtNombreCompleto.Text,txtCorreo.Text,txtContrasenia.Text,
                ((OpcionComboBox)cbRol.SelectedItem).valor.ToString(),((OpcionComboBox)cbRol.SelectedItem).texto.ToString(),
                ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString(),((OpcionComboBox)cbEstado.SelectedItem).texto.ToString(),
                });

                limpiar();
            }
            else {
                MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }   
        }

        private void limpiar() {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtContrasenia.Text = "";
            txtConfirmarContasenia.Text = "";
            cbRol.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;
            txtBusquedaNombre.Text = "";
            txtBusquedaDni.Text = "";

            txtDocumento.Select();

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        //Evento para pintar el boton de click
        private void dgvUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }

            if (e.ColumnIndex == 0) {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.check1.Width;
                var h = Properties.Resources.check1.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check1, new Rectangle(x,y,w,h));
                e.Handled = true;
            }
        }

        //Evento cuando se hace click en una fila de la tabla
        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "btnSeleccionar") {
                int indice = e.RowIndex;

                if (indice >= 0) {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvUsuarios.Rows[indice].Cells["id"].Value.ToString();
                    txtDocumento.Text = dgvUsuarios.Rows[indice].Cells["documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvUsuarios.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvUsuarios.Rows[indice].Cells["correo"].Value.ToString();
                    txtContrasenia.Text = dgvUsuarios.Rows[indice].Cells["contrasenia"].Value.ToString();
                    txtConfirmarContasenia.Text = dgvUsuarios.Rows[indice].Cells["contrasenia"].Value.ToString();

                    cbRol.Text = dgvUsuarios.Rows[indice].Cells["rol"].Value.ToString();
                    cbEstado.Text = dgvUsuarios.Rows[indice].Cells["estado"].Value.ToString();

                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto usuario con los datos del formulario
            Usuario user = new Usuario()
            {
                Id = Convert.ToInt32(txtId.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtContrasenia.Text,
                oRol = new Rol() { Id = Convert.ToInt32(((OpcionComboBox)cbRol.SelectedItem).valor) },
                Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            if (user.Id == 0)
            {
                MessageBox.Show("Debe seleccionar de la tabla el usuario a modificar");
            }

            if (user.Documento == "" || user.NombreCompleto == "" || user.Correo == "" || user.Clave == "")
            {
                MessageBox.Show("Para modificar un usuario, deben estar completos todos los campos de texto");
            }
            else {
                DialogResult resul = MessageBox.Show("Esta seguro que desea modificar al usuario","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (resul == DialogResult.Yes) {
                    bool modificado = new CN_Usuario().actualizarUsuario(user, out mensaje);
                    if (modificado == true) {
                        DataGridViewRow row = dgvUsuarios.Rows[Convert.ToInt32(txtIndice.Text)];
                        row.Cells["id"].Value = txtId.Text;
                        row.Cells["documento"].Value = txtDocumento.Text;
                        row.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                        row.Cells["correo"].Value = txtCorreo.Text;
                        row.Cells["contrasenia"].Value = txtContrasenia.Text;
                        row.Cells["idRol"].Value = ((OpcionComboBox)cbRol.SelectedItem).valor.ToString();
                        row.Cells["rol"].Value = ((OpcionComboBox)cbRol.SelectedItem).texto.ToString();
                        row.Cells["estadoValor"].Value = ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString();
                        row.Cells["estado"].Value = ((OpcionComboBox)cbEstado.SelectedItem).texto.ToString();
                        MessageBox.Show("Usuario actualizado con exito","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        limpiar();
                    }
                }
                else {
                    MessageBox.Show("No se realizo ningun cambio","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto usuario con los datos del formulario
            Usuario user = new Usuario()
            {
                Id = Convert.ToInt32(txtId.Text),
                
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                //Correo = txtCorreo.Text,
                //Clave = txtContrasenia.Text,
                //oRol = new Rol() { Id = Convert.ToInt32(((OpcionComboBox)cbRol.SelectedItem).valor) },
                //Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            if (user.Id == 0)
            {
                MessageBox.Show("Debe seleccionar de la tabla el usuario a eliminar");
            }
            else
            {
                DialogResult resul = MessageBox.Show("Esta seguro que desea eliminar al usuario: " + user.NombreCompleto.ToString() + 
                    " DNI: " + user.Documento.ToString(), "Mensaje", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (resul == DialogResult.Yes)
                {
                    
                    bool eliminado = new CN_Usuario().eliminarUsuario(user, out mensaje);
                    if (eliminado == true)
                    {
                        MessageBox.Show("Usuario elimnado con exito (establecido como'No Activo')", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvUsuarios.DataSource = null;
                        dgvUsuarios.Rows.Clear();

                        //Cargamos los usuarios a la tabla
                        List<Usuario> listaUsuarios = new CN_Usuario().listarUsuarios();
                        foreach (Usuario item in listaUsuarios)
                        {
                            dgvUsuarios.Rows.Add(new object[] {"",item.Id,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                            item.oRol.Id,item.oRol.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

                        }

                        limpiar();
                    
                    }
                    
                }
                else
                {
                    MessageBox.Show("No se realizo ningun cambio","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    limpiar();
                }
            }
        }

        private void txtBusquedaNombre_TextChanged(object sender, EventArgs e)
        {

            dgvUsuarios.DataSource = null;
            dgvUsuarios.Rows.Clear();

            string caracter = txtBusquedaNombre.Text;

            //Cargamos los usuarios a la tabla
            List<Usuario> listaUsuarios = new CN_Usuario().buscarUsuarioPorNombre(caracter);
            foreach (Usuario item in listaUsuarios)
            {
                dgvUsuarios.Rows.Add(new object[] {"",item.Id,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                item.oRol.Id,item.oRol.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

            }

        }

        private void txtBusquedaNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtNombreCompleto_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacionTextBox.soloLetras(e);
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacionTextBox.soloNumeros(e);
        }

        private void txtBusquedaDni_TextChanged(object sender, EventArgs e)
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Rows.Clear();

            string caracter = txtBusquedaDni.Text;

            //Cargamos los usuarios a la tabla
            List<Usuario> listaUsuarios = new CN_Usuario().buscarUsuarioPorDni(caracter);
            foreach (Usuario item in listaUsuarios)
            {
                dgvUsuarios.Rows.Add(new object[] {"",item.Id,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                item.oRol.Id,item.oRol.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

            }
        }
    }//Fin clase frmUsuario
}
