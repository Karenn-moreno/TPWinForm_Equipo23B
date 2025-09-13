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
        //nuevo articulo
        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) " +
                 "VALUES ('" + nuevo.Codigo + "', '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', " + nuevo.Precio +
                ", @IdMarca, @IdCategoria)" + "INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (SCOPE_IDENTITY(), @ImagenUrl);");

                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.Imagenes);
                datos.ejecutarAccion();

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
