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
    public partial class FormAgregarArticulo : Form
    {
        private Articulo articulo = null;
        public FormAgregarArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria=(Categoria)cboCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                //imagen


                // --- Agregar URL de imagen desde el TextBox ---
                string urlImagen = string.IsNullOrWhiteSpace(txtUrlImagen.Text)
                                   ? "https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png"
                                   : txtUrlImagen.Text;

                if (articulo.Imagenes == null)
                    articulo.Imagenes = new List<Imagen>();

                articulo.Imagenes.Clear(); // Limpiar lista 
                articulo.Imagenes.Add(new Imagen { UrlImagen = urlImagen });



                if (articulo.Id == 0)
                {

                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }


                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //despegables
        private void FormAgregarArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboCategoria.DataSource = categoriaNegocio.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        //ver
        private void txtMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

       }

        private void pbImagen_Click(object sender, EventArgs e)
        {

        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtUrlImagen.Text);
        }

        private void CargarImagen(string url)
        {
            try
            {
                pbImagen.Load(url);
            }
            catch
            {
                
                pbImagen.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
            }
        }

    }
}
    

