select *from ARTICULOS

SELECT a.Id, a.Nombre, m.Descripcion AS Marca, c.Descripcion AS Categoria
FROM ARTICULOS a
JOIN MARCAS m ON a.IdMarca = m.Id
JOIN CATEGORIAS c ON a.IdCategoria = c.Id;