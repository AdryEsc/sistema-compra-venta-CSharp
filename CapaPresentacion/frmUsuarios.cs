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

            //Cargamos combobox de busqueda
            foreach (DataGridViewColumn columna in dgvUsuarios.Columns) {
                if (columna.Visible == true && columna.Name != "btnSeleccionar") {
                    cbBusqueda.Items.Add(new OpcionComboBox() { valor = columna.Name, texto = columna.HeaderText });
                }
            }

            cbBusqueda.DisplayMember = "texto";
            cbBusqueda.ValueMember = "valor";
            cbBusqueda.SelectedIndex = 0;

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
            /*
            dgvUsuarios.Rows.Add(new object[] {"",txtId.Text,txtDocumento.Text,txtNombreCompleto.Text,txtCorreo.Text,txtContrasenia.Text,
            ((OpcionComboBox)cbRol.SelectedItem).valor.ToString(),((OpcionComboBox)cbRol.SelectedItem).texto.ToString(),
            ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString(),((OpcionComboBox)cbEstado.SelectedItem).texto.ToString(),
            });
            */

            limpiar();
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

                    /*
                    foreach (OpcionComboBox oc in cbRol.Items) {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dgvUsuarios.Rows[indice].Cells["idRol"].Value)) {
                            int indiceComboBox = cbRol.Items.IndexOf(oc);
                            cbRol.SelectedIndex = indiceComboBox;
                            break;
                        }
                    }*/



                    /*
                    foreach (OpcionComboBox oc in cbEstado.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dgvUsuarios.Rows[indice].Cells["estadoValor"].Value))
                        {
                            int indiceComboBox = cbEstado.Items.IndexOf(oc);
                            cbEstado.SelectedIndex = indiceComboBox;
                            break;
                        }
                    }*/
                }
            }
        }
    }
}
