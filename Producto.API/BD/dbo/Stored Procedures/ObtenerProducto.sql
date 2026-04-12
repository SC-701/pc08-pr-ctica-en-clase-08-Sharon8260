CREATE PROCEDURE ObtenerProducto
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        P.Id,
        P.IdSubCategoria AS SubCategoria,
        P.Nombre,
        P.Descripcion,
        C.Id AS IdCategoria,
        C.Nombre AS Categoria
    FROM Producto P
    INNER JOIN Categorias C 
        ON P.IdSubCategoria = C.Id
    WHERE P.Id = @Id
END