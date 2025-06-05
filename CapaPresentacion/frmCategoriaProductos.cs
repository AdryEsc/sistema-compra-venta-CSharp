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
    public partial class frmCategoriaProductos : Form
    {

        //Instancia para validar textbox
        ValidacionTextBox validacionTextBox = new ValidacionTextBox();

        public frmCategoriaProductos()
        {
            InitializeComponent();
        }

        private void frmCategoriaProductos_Load(object sender, EventArgs e)
        {
            //Cargamos el combobox de estado
            cbEstado.Items.Add(new OpcionComboBox() { valor = 1, texto = "Activo" });
            cbEstado.Items.Add(new OpcionComboBox() { valor = 0, texto = "No Activo" });

            cbEstado.DisplayMember = "texto";
            cbEstado.ValueMember = "valor";
            cbEstado.SelectedIndex = 0;

            //Cargamos las categorias a la tabla
            List<Categoria> listaCategorias = new CN_Categoria().listarCategorias();
            foreach (Categoria item in listaCategorias)
            {
                dgvCategorias.Rows.Add(new object[] {"",item.Id,item.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

            }
        }


        private void limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtDescripcion.Text = "";
            cbEstado.SelectedIndex = 0;
            txtBusquedaDescripcion.Text = "";

            txtDescripcion.Select();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto categoria con los datos del formulario
            Categoria categ = new Categoria()
            {
                Id = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            int idCategoriaGenerada = new CN_Categoria().registrarCategoria(categ, out mensaje);

            if (idCategoriaGenerada != 0)
            {
                dgvCategorias.Rows.Add(new object[] {"",idCategoriaGenerada,txtDescripcion.Text,
                ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString(),
                ((OpcionComboBox)cbEstado.SelectedItem).texto.ToString(),
                });

                limpiar();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void dgvCategorias_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.check1.Width;
                var h = Properties.Resources.check1.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check1, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvCategorias.Rows[indice].Cells["id"].Value.ToString();
                    txtDescripcion.Text = dgvCategorias.Rows[indice].Cells["descripcion"].Value.ToString();
                    cbEstado.Text = dgvCategorias.Rows[indice].Cells["estado"].Value.ToString();

                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto usuario con los datos del formulario
            Categoria categ = new Categoria()
            {
                Id = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            if (categ.Id == 0)
            {
                MessageBox.Show("Debe seleccionar de la tabla la categoria a modificar");
            }

            if (categ.Descripcion == "")
            {
                MessageBox.Show("Para modificar una categoria, debe agregar la nueva descripcion");
            }
            else
            {
                DialogResult resul = MessageBox.Show("Esta seguro que desea modificar la categoria", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resul == DialogResult.Yes)
                {
                    bool modificado = new CN_Categoria().actualizarCategoria(categ, out mensaje);
                    if (modificado == true)
                    {
                        DataGridViewRow row = dgvCategorias.Rows[Convert.ToInt32(txtIndice.Text)];
                        row.Cells["id"].Value = txtId.Text;
                        row.Cells["descripcion"].Value = txtDescripcion.Text;
                        row.Cells["estadoValor"].Value = ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString();
                        row.Cells["estado"].Value = ((OpcionComboBox)cbEstado.SelectedItem).texto.ToString();
                        MessageBox.Show("Categoria actualizada con exito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("No se realizo ningun cambio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto usuario con los datos del formulario
            Categoria categ = new Categoria()
            {
                Id = Convert.ToInt32(txtId.Text),

                Descripcion = txtDescripcion.Text
             
                //Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            if (categ.Id == 0)
            {
                MessageBox.Show("Debe seleccionar de la tabla la categoria a eliminar");
            }
            else
            {
                DialogResult resul = MessageBox.Show("Esta seguro que desea eliminar la categoria: " + categ.Descripcion.ToString(), 
                "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resul == DialogResult.Yes)
                {

                    bool eliminado = new CN_Categoria().eliminarCategoria(categ, out mensaje);
                    if (eliminado == true)
                    {
                        MessageBox.Show("Categoria elimnada con exito (establecido como'No Activo')", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvCategorias.DataSource = null;
                        dgvCategorias.Rows.Clear();

                        //Cargamos las categorias a la tabla
                        List<Categoria> listaCategorias = new CN_Categoria().listarCategorias();
                        foreach (Categoria item in listaCategorias)
                        {
                            dgvCategorias.Rows.Add(new object[] { "", item.Id, item.Descripcion, item.Estado, item.Estado == 1 ? "Activo" : "No Activo" });

                        }

                        limpiar();

                    }

                }
                else
                {
                    MessageBox.Show("No se realizo ningun cambio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
            }
        }
    }
}
