using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

//comentario 
namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            //connection
            SqlConnection conexion = new SqlConnection("Server=.\\SQLEXPRESS; Initial Catalog= CATALOGO_P3_DB; Integrated Security=true;");
            //command
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandText = @"
    SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion,
           m.Descripcion AS Marca,
           c.Descripcion AS Categoria,
           a.Precio
    FROM ARTICULOS a
    INNER JOIN MARCAS m ON a.IdMarca = m.Id
    INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id";
            //sql datareader
            SqlDataReader lector;

            conexion.Open();
            lector = comando.ExecuteReader();

            while (lector.Read())
            {
                Articulo articulo = new Articulo();
                articulo.Id = (int)lector["Id"];
                articulo.Codigo = (string)lector["Codigo"];
                articulo.Nombre = (string)lector["Nombre"];
                articulo.Descripcion = (string)lector["Descripcion"];

                articulo.Marca = new Marca();
                articulo.Marca.Descripcion = (string)lector["Marca"];

                articulo.Categoria = new Categoria();
                articulo.Categoria.Descripcion = (string)lector["Categoria"];

                articulo.Precio = (decimal)lector["Precio"];// comentario 2

                lista.Add(articulo);
            }
            conexion.Close();


            return lista;
        }
        public Articulo Listar(int id)// sobrecargamos el método Listar
        {
            AccesoDatos datos = new AccesoDatos();
            Articulo articulo = null;
            try
            {
                datos.setearConsulta(@"
            SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion,
                   m.Descripcion AS Marca,
                   c.Descripcion AS Categoria,
                   a.Precio
            FROM ARTICULOS a
            INNER JOIN MARCAS m ON a.IdMarca = m.Id
            INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id
            WHERE a.Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];

                    articulo.Marca = new Marca();
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    articulo.Imagenes = imagenNegocio.ListarPorArticulo(id);
                }

                return articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        //nuevo articulo
        public void Agregar(Articulo nuevo)
        {
            try
            {


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id=@");
                datos.setearParametro("@",id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }






    }

}
