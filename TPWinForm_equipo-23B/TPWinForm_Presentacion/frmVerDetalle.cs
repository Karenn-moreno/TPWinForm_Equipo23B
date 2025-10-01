using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TPWinForm_Presentacion
{
    public partial class frmVerDetalle : Form
    {
        public frmVerDetalle(dominio.Articulo articuloSeleccionado)
        {
            InitializeComponent();

        }
        private int idArticulo;

        // Constructor que recibe solo el ID del artículo
        public frmVerDetalle(int id)
        {
            InitializeComponent();
            this.idArticulo = id;
        }

        private void frmVerDetalle_Load(object sender, EventArgs e)
        {

            MessageBox.Show("ID artículo recibido: " + idArticulo);

            try
            {

                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articuloCompleto = negocio.Listar(idArticulo);
                if (articuloCompleto != null)
                {
                    // Asignar los datos a los controles de la interfaz gráfica
                    lblCodigo.Text = "Codigo: " + articuloCompleto.Codigo;
                    lblNombre.Text = "Nombre: " + articuloCompleto.Nombre;
                    lblDescripcion.Text = "Descripcion: " + articuloCompleto.Descripcion;
                    lblMarca.Text = "Marca: " + articuloCompleto.Marca.Descripcion;
                    lblCategoria.Text = "Categoria: " + articuloCompleto.Categoria.Descripcion;
                    lblPrecio.Text = "Precio: " + articuloCompleto.Precio.ToString("C");

                    // Imagen principal
                    if (articuloCompleto.Imagenes != null && articuloCompleto.Imagenes.Count > 0)
                    {
                        try
                        {
                            pbxImagen.Load(articuloCompleto.Imagenes[0].UrlImagen);
                        }
                        catch
                        {
                            pbxImagen.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
                        }
                    }
                }
                else
                {
                    pbxImagen.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el detalle del artículo: " + ex.Message);
                this.Close();
            }


        }
    }
}
