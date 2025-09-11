using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> Listar()
        {
            List<Imagen> lista = new List<Imagen>();
            SqlConnection conexion = new SqlConnection("Server=.\\SQLEXPRESS; Initial Catalog=CATALOGO_P3_DB; Integrated Security=true;");
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandText =  @"
    SELECT i.Id, i.IdArticulo, i.ImagenUrl,
       a.Nombre,
       m.Descripcion AS Marca,
       c.Descripcion AS Categoria
  FROM IMAGENES i
  INNER JOIN ARTICULOS a ON i.IdArticulo = a.Id
  INNER JOIN MARCAS m ON a.IdMarca = m.Id
  INNER JOIN CATEGORIAS c ON a.IdCategoria = c.Id";

            //SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES

            conexion.Open();
            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                Imagen img = new Imagen();
                img.Id = (int)lector["Id"];
                img.UrlImagen = (string)lector["ImagenUrl"];
                img.Articulo = new Articulo { Id = (int)lector["IdArticulo"] };
                lista.Add(img);
            }

            conexion.Close();
            return lista;
        }
    }
}
