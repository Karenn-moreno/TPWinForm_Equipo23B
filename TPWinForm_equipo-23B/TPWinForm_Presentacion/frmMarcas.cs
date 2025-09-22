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
    public partial class frmMarcas : Form
    {
        public frmMarcas()
        {
            InitializeComponent();
            CargarMarcas();
        }


        private void CargarMarcas()
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                dgvGestionMarcas.DataSource = negocio.listar(); // listar de MarcaNegocio
                dgvGestionMarcas.Columns["Id"].Visible = false;
                dgvGestionMarcas.Columns["Descripcion"].HeaderText = "Marca";
                dgvGestionMarcas.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                string descripcion = txtMarcas.Text.Trim();

                if (string.IsNullOrEmpty(descripcion))
                {
                    MessageBox.Show("Debe ingresar una Marca");
                    return;
                }

                Marca nuevaMarca = new Marca();
                nuevaMarca.Descripcion = descripcion;

                MarcaNegocio negocio = new MarcaNegocio();
                bool agregado = negocio.InsertarSiNoExiste(nuevaMarca);

                if (agregado)
                {
                    MessageBox.Show("La marca se agrego correctamente");
                }
                else
                {
                    MessageBox.Show("La marca ya existe");
                }

                txtMarcas.Clear();
                CargarMarcas(); // actualiza el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void lblTituloMarca_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca seleccionada;

            try
            {
                DialogResult respuesta = MessageBox.Show( "¿Desea eliminar la marca seleccionada?","Confirmar eliminación",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    seleccionada = (Marca)dgvGestionMarcas.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionada.Id);
                    CargarMarcas(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void btnModificarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGestionMarcas.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una marca para modificar.");
                    return;
                }

                // obtengo la marca seleccionada
                Marca seleccionada = (Marca)dgvGestionMarcas.CurrentRow.DataBoundItem;
                seleccionada.Descripcion = txtMarcas.Text.Trim();

                if (string.IsNullOrEmpty(seleccionada.Descripcion))
                {
                    MessageBox.Show("Debe ingresar una marca");
                    return;
                }

                MarcaNegocio negocio = new MarcaNegocio();
                bool modificado = negocio.Modificar(seleccionada); // devuelve bool

                if (modificado)
                {
                    MessageBox.Show("La marca se modifico correctamente.");
                    txtMarcas.Clear();
                    CargarMarcas();
                    dgvGestionMarcas.ClearSelection(); //aseguro de limpiar
                }
                else
                {
                    MessageBox.Show("Ya existe otra marca con ese nombre, no se agrego la modificacion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
