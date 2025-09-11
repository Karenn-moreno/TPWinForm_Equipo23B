using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio; //acceder al objeto 


namespace TPWinForm_Presentacion
{
    public partial class Form1 : Form
    {
        private ImagenNegocio imagenNeg = new ImagenNegocio();
        private List<Articulo> listaArticulos;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.Listar();   // Traer todos los artículos

            // Traer todas las imágenes
            List<Imagen> listaImagenes = imagenNeg.Listar();

            if (listaArticulos != null && listaImagenes != null)
            {
                foreach (var art in listaArticulos)
                {
                    // Inicializar la lista de imágenes del artículo
                    art.Imagenes = new List<Imagen>();

                    // Relacionar solo las imágenes que tengan el objeto Articulo asignado
                    var imgs = listaImagenes
                        .Where(i => i.Articulo != null && i.Articulo.Id == art.Id)
                        .ToList();

                    if (imgs.Count > 0)
                        art.Imagenes.AddRange(imgs);
                }
            }

            // Muestra los artículos en DataGridView
            dgvArticulo.DataSource = listaArticulos;
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow == null) return;

            Articulo seleccionado = dgvArticulo.CurrentRow.DataBoundItem as Articulo;
            if (seleccionado == null) return;

            // Limpiar FlowLayoutPanel antes de agregar nuevas imágenes
            fpImagen.Controls.Clear();

            // Si el artículo no tiene imágenes, mostrar imagen por defecto
            if (seleccionado.Imagenes.Count == 0)
            {
                PictureBox pb = new PictureBox
                {
                    Width = 120,
                    Height = 120,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                pb.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
                fpImagen.Controls.Add(pb);
            }
            else
            {
                // Mostrar todas las imágenes asociadas
                foreach (var img in seleccionado.Imagenes)
                {
                    PictureBox pb = new PictureBox
                    {
                        Width = 120,
                        Height = 120,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    try
                    {
                        pb.Load(img.UrlImagen);
                    }
                    catch
                    {
                        // Imagen por defecto si falla la URL
                        pb.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
                    }

                    fpImagen.Controls.Add(pb);
                }

            }


        }

        private void fpImagen_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
