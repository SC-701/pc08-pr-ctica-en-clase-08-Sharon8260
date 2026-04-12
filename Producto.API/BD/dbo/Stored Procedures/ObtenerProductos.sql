CREATE PROCEDURE ObtenerProductos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.Id,
        P.Nombre,
        P.Descripcion,
        P.Precio,
        P.Stock,
        P.CodigoBarras,
        P.IdSubCategoria,
        SC.Nombre AS NombreSubCategoria
    FROM Producto P
    LEFT JOIN SubCategorias SC 
        ON P.IdSubCategoria = SC.Id
END