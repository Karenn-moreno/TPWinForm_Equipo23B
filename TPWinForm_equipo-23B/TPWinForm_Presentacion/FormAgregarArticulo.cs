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
        
        public FormAgregarArticulo(Articulo articulo)// Nuevo constructor para modificar un artículo existente
        {
            InitializeComponent();
            this.articulo = articulo;
            this.Text = "Modificar Artículo"; // Cambia el título de la ventana
            txtCodigo.ReadOnly = true; //Deja el codigo visible pero al apretar la función modificar no lo permite cambiar ni eliminar

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ArticuloNegocio negocio1 = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;


                //precio validacion
                decimal precio;
                try
                {
                    precio = decimal.Parse(txtPrecio.Text);
                }
                catch
                {
                    MessageBox.Show("Ingrese un precio válido.");
                    return;
                }

                if (precio <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a 0.");
                    return;
                }

                articulo.Precio = precio; //si paso las validaciones

                ///validacion codigo
                if ((articulo.Id == 0) && negocio.ExisteCodigo(txtCodigo.Text.Trim()))
                {
                    MessageBox.Show("El código ya existe. Ingrese otro.");
                    return;
                }



                // Marca y Categoría usando SelectedValue y Text
                articulo.Marca = new Marca
                {
                    Id = (int)cboMarca.SelectedValue,
                    Descripcion = cboMarca.Text
                };
                articulo.Categoria = new Categoria
                {
                    Id = (int)cboCategoria.SelectedValue,
                    Descripcion = cboCategoria.Text
                };

                //   --- Agregar URL de imagen desde el TextBox ---
                string urlImagen = string.IsNullOrWhiteSpace(txtUrlImagen.Text)
                                ? "https://redthread.uoregon.edu/files/original/affd16fd5264cab9197da4cd1a996f820e601ee4.png"
                                 : txtUrlImagen.Text;

                articulo.Imagenes.Clear();
                if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text))
                {
                    articulo.Imagenes.Add(new Imagen { UrlImagen = txtUrlImagen.Text });
                }




                if (articulo.Id == 0)
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");

                   
                       
                    }
                    else
                    {
                        negocio.modificar(articulo);
                        MessageBox.Show("Modificado exitosamente");
                    }

                    Close();

                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //despegables
        private void FormAgregarArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                // --- Asignar DataSource y propiedades del ComboBox ---
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.ValueMember = "Id";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();

                    
                    if (((List<Marca>)cboMarca.DataSource).Any(m => m.Id == articulo.Marca.Id))
                        cboMarca.SelectedValue = articulo.Marca.Id;

                    if (((List<Categoria>)cboCategoria.DataSource).Any(c => c.Id == articulo.Categoria.Id))
                        cboCategoria.SelectedValue = articulo.Categoria.Id;


                    // Cargar imagen
                    if (articulo.Imagenes.Count > 0)
                    {
                        txtUrlImagen.Text = articulo.Imagenes[0].UrlImagen;
                        CargarImagen(txtUrlImagen.Text);
                    }
                }
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


        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblImagen_Click(object sender, EventArgs e)
        {

        }

    }
}
    

