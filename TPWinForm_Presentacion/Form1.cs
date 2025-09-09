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
            listaArticulos = negocio.Listar();   // Traer artículos una sola vez
            dgvArticulo.DataSource = listaArticulos;


            // Traer imágenes
            List<Imagen> listaImagenes = imagenNeg.Listar();


            if (listaArticulos != null && listaImagenes != null)
            {
                foreach (var art in listaArticulos)
                {
                    var imgs = listaImagenes
                        .Where(i => i.Articulo != null && i.Articulo.Id == art.Id)
                        .ToList();

                    if (imgs.Count > 0)
                        art.Imagenes.AddRange(imgs);
                }
            }

            // Mostrar artículos en DataGridView
            dgvArticulo.DataSource = listaArticulos;
            //dgvArticulo.Columns["Imagenes"].Visible = false; // opcional
        }


        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {

            //if (dgvArticulo.CurrentRow == null) return;

            if (dgvArticulo.CurrentRow == null) return;

            Articulo seleccionado = dgvArticulo.CurrentRow.DataBoundItem as Articulo;
            if (seleccionado == null) return; // <- seguridad extra

          
            // Limpiar FlowLayoutPanel antes de agregar nuevas imágenes
            fpImagen.Controls.Clear();

            // Agregar todas las imágenes del artículo
            foreach (var img in seleccionado.Imagenes)
            {
                PictureBox pb = new PictureBox();
                pb.Width = 120;
                pb.Height = 120;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                try
                {
                    pb.Load(img.UrlImagen);
                }
                catch
                {
                    // Imagen por defecto si falla
                    pb.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
                }

                fpImagen.Controls.Add(pb);
            }

        }
    }
}
