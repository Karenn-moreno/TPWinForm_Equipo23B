using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPWinForm_Presentacion
{
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvGestionCategorias.DataSource = negocio.listar();
                dgvGestionCategorias.Columns["Id"].Visible = false;
                dgvGestionCategorias.Columns["Descripcion"].HeaderText = "Categoria";
                dgvGestionCategorias.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                string descripcion = txtCategoria.Text.Trim();

                if (string.IsNullOrEmpty(descripcion))
                {
                    MessageBox.Show("Debe ingresar una Categoria");
                    return;
                }

                if (descripcion.Length > 150)
                {
                    MessageBox.Show("La descripcion no puede superar los 150 caracteres");
                    return;
                }

                Categoria nuevaCategoria = new Categoria();
                nuevaCategoria.Descripcion = descripcion;

                CategoriaNegocio negocio = new CategoriaNegocio();
                bool agregado = negocio.AgregarSiNoExiste(nuevaCategoria);

                if (agregado)
                {
                    MessageBox.Show("La categoria se agrego exitosamente");
                }
                else
                {
                    MessageBox.Show("La categoria ya existe");
                }

                txtCategoria.Clear();
                CargarCategorias(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGestionCategorias.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una descripcion para modificar.");
                    return;
                }

                Categoria seleccionada = (Categoria)dgvGestionCategorias.CurrentRow.DataBoundItem;
                seleccionada.Descripcion = txtCategoria.Text.Trim();

                if (string.IsNullOrEmpty(seleccionada.Descripcion))
                {
                    MessageBox.Show("Debe ingresar una categoria");
                    return;
                }

                if (seleccionada.Descripcion.Length > 150)
                {
                    MessageBox.Show("La descripcion no puede superar los 150 caracteres");
                    return;
                }

                CategoriaNegocio negocio = new CategoriaNegocio();
                bool modificado = negocio.ModificarCategoria(seleccionada); 

                if (modificado)
                {
                    MessageBox.Show("La categoria se modifico exitosamente");
                    txtCategoria.Clear();
                    CargarCategorias();
                    dgvGestionCategorias.ClearSelection(); 
                }
                else
                {
                    MessageBox.Show("Ya existe otra categoria con ese nombre, no se agrego la modificacion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria seleccionada;

            try
            {
                DialogResult rta = MessageBox.Show("¿Desea eliminar la categoria seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (rta == DialogResult.Yes)
                {
                    seleccionada = (Categoria)dgvGestionCategorias.CurrentRow.DataBoundItem;
                    negocio.EliminarCategoria(seleccionada.Id);
                    CargarCategorias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
