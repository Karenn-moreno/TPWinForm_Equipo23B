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
            try
            {
                // Usar la capa de negocio para obtener todos los detalles del artículo
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articuloCompleto = negocio.Listar(idArticulo);

                // Asignar los datos a los controles de la interfaz gráfica
                lblCodigo.Text = "Código: " + articuloCompleto.Codigo;
                lblNombre.Text = "Nombre: " + articuloCompleto.Nombre;
                lblDescripcion.Text = "Descripción: " + articuloCompleto.Descripcion;
                lblMarca.Text = "Marca: " + articuloCompleto.Marca.Descripcion;
                lblCategoria.Text = "Categoría: " + articuloCompleto.Categoria.Descripcion;
                lblPrecio.Text = "Precio: " + articuloCompleto.Precio.ToString("C");

                // Si tienes un PictureBox llamado pbxImagen para mostrar la imagen:
                 if (articuloCompleto.Imagenes.Count > 0)
                {
                    pbxImagen.Load(articuloCompleto.Imagenes[0].UrlImagen);
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
