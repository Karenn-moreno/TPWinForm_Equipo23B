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

        //filtro avanzado
        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");


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

        private void cargar()
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

            dgvArticulo.DataSource = null;      // Se limpia el datasourse
            dgvArticulo.DataSource = listaArticulos;
        }

        private void fpImagen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEleminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar el registro?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();//actualiza la grilla
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            // Asegúrate de que el nombre del control sea el mismo en el código y en el diseñador
            if (dgvArticulo.CurrentRow != null)
            {
                try
                {
                    // Obtener el ID del artículo seleccionado
                    Articulo articuloSeleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    int idArticulo = articuloSeleccionado.Id;

                    // Abrir el formulario de detalle
                    frmVerDetalle detalle = new frmVerDetalle(idArticulo);
                    detalle.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo para ver los detalles.");
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAgregarArticulo agregarArticulo = new FormAgregarArticulo();
            agregarArticulo.ShowDialog();

        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                FormAgregarArticulo modificarArticulo = new FormAgregarArticulo(seleccionado);
                modificarArticulo.ShowDialog();
                cargar();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo para modificar.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //boton filtro normal
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listaFiltrada;


        }

        private void lblFiltro_Click(object sender, EventArgs e)
        {

        }

        private void menuGestionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMarcas_Click(object sender, EventArgs e)
        {
            frmMarcas ventana = new frmMarcas();
            ventana.ShowDialog();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategorias ventana = new frmCategorias();
            ventana.ShowDialog();
        }
        //
        private void lblCriterio_Click(object sender, EventArgs e)
        {

        }

        //filtro avanzado
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            cboCriterio.Items.Clear();

            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }
        
       
        


        //buscar avanzado
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (cboCampo.SelectedItem == null || cboCriterio.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione campo y criterio antes de buscar.");
                    return;
                }

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                // Validación extra para Precio
                if (campo == "Precio")
                {
                    if (string.IsNullOrEmpty(filtro))
                    {
                        MessageBox.Show("Por favor ingrese un valor numérico para el precio.");
                        return;
                    }

                    decimal precio;
                    if (!decimal.TryParse(filtro, out precio))
                    {
                        MessageBox.Show("El filtro de precio debe ser un número válido.");
                        return;
                    }
                }

                // Traer artículos filtrados
                listaArticulos = negocio.filtrar(campo, criterio, filtro);

                //  Cargar imágenes relacionadas a esos artículos
                List<Imagen> listaImagenes = imagenNeg.Listar();
                foreach (var art in listaArticulos)
                {
                    art.Imagenes = listaImagenes
                        .Where(i => i.Articulo != null && i.Articulo.Id == art.Id)
                        .ToList();
                }

                dgvArticulo.DataSource = null;
                dgvArticulo.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroAvanzado_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }

