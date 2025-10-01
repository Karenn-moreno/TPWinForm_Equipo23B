using dominio;
using negocio; //acceder al objeto 
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
    public partial class Form1 : Form
    {
        private ImagenNegocio imagenNeg = new ImagenNegocio();
        private List<Articulo> listaArticulos;

        public Form1()
        {
            InitializeComponent();
        }

        //Filtro avanzado
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cargar(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los artículos: {ex.Message}", "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");


        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow == null) return;

            Articulo seleccionado = dgvArticulo.CurrentRow.DataBoundItem as Articulo;
            if (seleccionado == null) return;

            // Limpiar antes de agregar nuevas imágenes
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
                    catch (Exception ex)  
                    {
                        // Logueo el error y cargo la imagen por defecto
                        Console.WriteLine($"Error al cargar imagen: {img.UrlImagen}. Detalle: {ex.Message}");
                        pb.Load("https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png");
                    }

                    fpImagen.Controls.Add(pb);
                }

            }


        }

        private void cargar()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.Listar();   // Traer todos los artículos

                // Traer todas las imágenes
                List<Imagen> listaImagenes = imagenNeg.Listar();

                if (listaArticulos != null && listaImagenes != null)
                {
                    foreach (var art in listaArticulos)
                    {
                        art.Imagenes = new List<Imagen>();

                        var imgs = listaImagenes
                            .Where(i => i.Articulo != null && i.Articulo.Id == art.Id)
                            .ToList();

                        if (imgs.Count > 0)
                            art.Imagenes.AddRange(imgs);
                    }
                }

                dgvArticulo.DataSource = null;
                dgvArticulo.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los artículos desde la base de datos.\nDetalle: {ex.Message}",
                    "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fpImagen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEleminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow == null || dgvArticulo.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            Articulo seleccionado = dgvArticulo.CurrentRow.DataBoundItem as Articulo;
            if (seleccionado == null)
            {
                MessageBox.Show("El elemento seleccionado no es un artículo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Confirmar antes de eliminar
            DialogResult respuesta = MessageBox.Show(
                $"¿Desea eliminar el artículo '{seleccionado.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(seleccionado.Id);

                    MessageBox.Show("Artículo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar el artículo '{seleccionado.Nombre}'. Detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnVerDetalle_Click(object sender, EventArgs e)
        {

            if (dgvArticulo.CurrentRow != null)
            {
                try
                {
                    // Obtener el ID del artículo seleccionado
                    Articulo articuloSeleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    int idArticulo = articuloSeleccionado.Id;


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
            if (dgvArticulo.CurrentRow == null || dgvArticulo.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo para modificar.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 2. Intentar castear el artículo
            Articulo seleccionado = dgvArticulo.CurrentRow.DataBoundItem as Articulo;
            if (seleccionado == null)
            {
                MessageBox.Show("El elemento seleccionado no es un artículo válido.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // 3. Abrir el formulario de modificación
            try
            {
                FormAgregarArticulo modificar = new FormAgregarArticulo(seleccionado);
                modificar.ShowDialog();
                cargar(); // refrescar grilla después de modificar
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo modificar el artículo '{seleccionado.Nombre}'.\nDetalle: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //boton filtro normal
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                List<Articulo> listaFiltrada;
                string filtro = txtFiltro.Text;

                if (filtro != "")
                {
                    listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
                    if (listaFiltrada.Count == 0)
                    {
                        MessageBox.Show("No se encontraron artículos que coincidan con el filtro.",
                                        "Información",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                else
                {
                    listaFiltrada = listaArticulos;
                }

                dgvArticulo.DataSource = null;
                dgvArticulo.DataSource = listaFiltrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar el filtro por nombre.\nDetalle: {ex.Message}",
                    "Error de filtro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        

        //Buscar avanzado
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (cboCampo.SelectedItem == null || cboCriterio.SelectedItem == null)
                {
                    MessageBox.Show(
                        "Por favor seleccione campo y criterio antes de buscar.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                // Validación 
                if (campo == "Precio")
                {
                    if (string.IsNullOrEmpty(filtro))
                    {
                        MessageBox.Show(
                            "Por favor ingrese un valor numérico para el precio.",
                            "Advertencia",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    decimal precio;
                    if (!decimal.TryParse(filtro, out precio))
                    {
                        MessageBox.Show(
                            "El filtro de precio debe ser un número válido.",
                            "Advertencia",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                // Traer artículos filtrados
                listaArticulos = negocio.filtrar(campo, criterio, filtro);

                
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
                MessageBox.Show($"Error al realizar la búsqueda con campo '{cboCampo.SelectedItem}' y criterio '{cboCriterio.SelectedItem}'.\nDetalle: {ex.Message}",
                "Error de búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

