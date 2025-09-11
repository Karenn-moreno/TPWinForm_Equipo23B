select *from ARTICULOS
select *from MARCAS
SELECT a.Id, a.Nombre, m.Descripcion AS Marca, c.Descripcion AS Categoria
FROM ARTICULOS a
JOIN MARCAS m ON a.IdMarca = m.Id
JOIN CATEGORIAS c ON a.IdCategoria = c.Id;

INSERT INTO IMAGENES (IdArticulo, ImagenUrl)
VALUES (1, 'https://http2.mlstatic.com/D_914989-MLA31578002339_072019-C.jpg');


INSERT INTO IMAGENES (IdArticulo, ImagenUrl)
VALUES (1, 'https://www.ubuy.com.ar/productimg/?image=aHR0cHM6Ly9pbWFnZXMtbmEuc3NsLWltYWdlcy1hbWF6b24uY29tL2ltYWdlcy9JLzQxTC1wWWZQT3JTLl9TUzQwMF8uanBn.jpg'),
  (1, 'https://i.blogs.es/e7e2f6/ofi-01/650_1200.jpg')
;
