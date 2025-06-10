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
    public partial class frmProductos : Form
    {
        //Instancia para validar textbox
        ValidacionTextBox validacionTextBox = new ValidacionTextBox();

        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            //Cargamos el combobox de estado
            cbEstado.Items.Add(new OpcionComboBox() { valor = 1, texto = "Activo" });
            cbEstado.Items.Add(new OpcionComboBox() { valor = 0, texto = "No Activo" });

            cbEstado.DisplayMember = "texto";
            cbEstado.ValueMember = "valor";
            cbEstado.SelectedIndex = 0;

            //Cargamos el combobox de categorias
            List<Categoria> listaCategorias = new CN_Categoria().listarCategorias();
            foreach (Categoria item in listaCategorias)
            {
                cbCategoria.Items.Add(new OpcionComboBox() { valor = item.Id, texto = item.Descripcion });
            }

            cbCategoria.DisplayMember = "texto";
            cbCategoria.ValueMember = "valor";
            cbCategoria.SelectedIndex = 0;

            //Cargamos los productos a la tabla
            List<Producto> listaProductos = new CN_Producto().listarProductos();
            foreach (Producto item in listaProductos)
            {
                dgvProductos.Rows.Add(new object[] {"",item.Id,item.Codigo,item.Descripcion,item.Stock,item.PrecioCompra,item.PrecioVenta,
                item.oCategoria.Id,item.oCategoria.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

            }

        }

        private void limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtStock.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            cbEstado.SelectedIndex = 0;
            cbCategoria.SelectedIndex = 0;

            txtCodigo.Select();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto producto con los datos del formulario
            Producto prod = new Producto()
            {
                Id = Convert.ToInt32(txtId.Text),
                Codigo = txtCodigo.Text,
                Descripcion = txtDescripcion.Text.ToUpper(),
                Stock = Convert.ToInt32(txtStock.Text),
                PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text),
                PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                oCategoria = new Categoria() { Id = Convert.ToInt32(((OpcionComboBox)cbCategoria.SelectedItem).valor) },
                Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            int idProductoGenerado = new CN_Producto().registrarProducto(prod, out mensaje);

            if (idProductoGenerado != 0)
            {
                /*
                dgvProductos.Rows.Add(new object[] {"",idProductoGenerado,txtCodigo.Text,txtDescripcion.Text.ToUpper(),txtStock.Text,txtPrecioCompra.Text,txtPrecioVenta.Text,
                ((OpcionComboBox)cbCategoria.SelectedItem).valor.ToString(),
                ((OpcionComboBox)cbCategoria.SelectedItem).texto.ToString(),
                ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString(),
                ((OpcionComboBox)cbEstado.SelectedItem).texto.ToString(),
                });
                */

                dgvProductos.DataSource = null;
                dgvProductos.Rows.Clear();

                //Cargamos los productos a la tabla
                List<Producto> listaProductos = new CN_Producto().listarProductos();
                foreach (Producto item in listaProductos)
                {
                    dgvProductos.Rows.Add(new object[] {"",item.Id,item.Codigo,item.Descripcion,item.Stock,item.PrecioCompra,item.PrecioVenta,
                            item.oCategoria.Id,item.oCategoria.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

                }

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

        private void dgvProductos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvProductos.Rows[indice].Cells["id"].Value.ToString();
                    txtCodigo.Text = dgvProductos.Rows[indice].Cells["codigo"].Value.ToString();
                    txtDescripcion.Text = dgvProductos.Rows[indice].Cells["descripcion"].Value.ToString();
                    txtStock.Text = dgvProductos.Rows[indice].Cells["stock"].Value.ToString();
                    txtPrecioCompra.Text = dgvProductos.Rows[indice].Cells["precioCompra"].Value.ToString();
                    txtPrecioVenta.Text = dgvProductos.Rows[indice].Cells["precioVenta"].Value.ToString();

                    cbCategoria.Text = dgvProductos.Rows[indice].Cells["categoria"].Value.ToString();
                    cbEstado.Text = dgvProductos.Rows[indice].Cells["estado"].Value.ToString();

                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            //Creamos el objeto producto con los datos del formulario
            Producto prod = new Producto()
            {
                Id = Convert.ToInt32(txtId.Text),
                Codigo = txtCodigo.Text,
                Descripcion = txtDescripcion.Text.ToUpper(),
                Stock = Convert.ToInt32(txtStock.Text),
                PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text),
                PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),

                oCategoria = new Categoria() { Id = Convert.ToInt32(((OpcionComboBox)cbCategoria.SelectedItem).valor) },
                Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            if (prod.Id == 0)
            {
                MessageBox.Show("Debe seleccionar de la tabla el producto a modificar");
            }
            else if (txtCodigo.Text == "" || txtDescripcion.Text == "" || txtStock.Text == "" || txtPrecioCompra.Text == "" || txtPrecioVenta.Text == "")
            {
                MessageBox.Show("Para modificar un producto, debe completar todos los datos");
            }
            else
            {
                DialogResult resul = MessageBox.Show("Esta seguro que desea modificar el producto", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resul == DialogResult.Yes)
                {
                    bool modificado = new CN_Producto().actualizarProducto(prod, out mensaje);
                    if (modificado == true)
                    {
                        /*
                        DataGridViewRow row = dgvProductos.Rows[Convert.ToInt32(txtIndice.Text)];
                        row.Cells["id"].Value = txtId.Text;
                        row.Cells["codigo"].Value = txtCodigo.Text;
                        row.Cells["descripcion"].Value = txtDescripcion.Text.ToUpper();
                        row.Cells["stock"].Value = txtStock.Text;
                        row.Cells["precioCompra"].Value = txtPrecioCompra.Text;
                        row.Cells["precioVenta"].Value = txtPrecioVenta.Text;
                        row.Cells["idCategoria"].Value = ((OpcionComboBox)cbCategoria.SelectedItem).valor.ToString();
                        row.Cells["categoria"].Value = ((OpcionComboBox)cbCategoria.SelectedItem).texto.ToString();
                        row.Cells["estadoValor"].Value = ((OpcionComboBox)cbEstado.SelectedItem).valor.ToString();
                        row.Cells["estado"].Value = ((OpcionComboBox)cbEstado.SelectedItem).texto.ToString();
                        */

                        dgvProductos.DataSource = null;
                        dgvProductos.Rows.Clear();

                        //Cargamos los productos a la tabla
                        List<Producto> listaProductos = new CN_Producto().listarProductos();
                        foreach (Producto item in listaProductos)
                        {
                            dgvProductos.Rows.Add(new object[] {"",item.Id,item.Codigo,item.Descripcion,item.Stock,item.PrecioCompra,item.PrecioVenta,
                            item.oCategoria.Id,item.oCategoria.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

                        }

                        MessageBox.Show("Producto actualizado con exito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            //Creamos el objeto producto con los datos del formulario
            Producto prod = new Producto()
            {
                Id = Convert.ToInt32(txtId.Text),

                Descripcion = txtDescripcion.Text

                //Estado = Convert.ToInt32(((OpcionComboBox)cbEstado.SelectedItem).valor)
            };

            if (prod.Id == 0)
            {
                MessageBox.Show("Debe seleccionar de la tabla el producto a eliminar");
            }
            else
            {
                DialogResult resul = MessageBox.Show("Esta seguro que desea eliminar el producto: " + prod.Descripcion.ToString(),
                "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resul == DialogResult.Yes)
                {

                    bool eliminado = new CN_Producto().eliminarProducto(prod, out mensaje);
                    if (eliminado == true)
                    {
                        MessageBox.Show("Producto elimnado con exito (establecido como'No Activo')", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvProductos.DataSource = null;
                        dgvProductos.Rows.Clear();

                        //Cargamos los productos a la tabla
                        List<Producto> listaProductos = new CN_Producto().listarProductos();
                        foreach (Producto item in listaProductos)
                        {
                            dgvProductos.Rows.Add(new object[] {"",item.Id,item.Codigo,item.Descripcion,item.Stock,item.PrecioCompra,item.PrecioVenta,
                            item.oCategoria.Id,item.oCategoria.Descripcion,item.Estado,item.Estado == 1 ? "Activo" : "No Activo"});

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

        private void dgvProductos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //this.dgvProductos.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.dgvProductos_RowPrePaint);

            var dgv = dgvProductos;
            var fila = dgv.Rows[e.RowIndex];

            var estado = fila.Cells["estado"].Value?.ToString();

            if (estado == "No Activo") {
                fila.DefaultCellStyle.BackColor = Color.LightBlue;
                //fila.DefaultCellStyle.ForeColor = Color.White;

            }
        }
    }//fin clase
}
