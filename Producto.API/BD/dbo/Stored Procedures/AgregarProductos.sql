CREATE PROCEDURE AgregarProductos
	-- Add the parameters for the stored procedure here
	 @Id AS UNIQUEIDENTIFIER
     ,@IdSubCategoria AS UNIQUEIDENTIFIER
     ,@Nombre AS VARCHAR(MAX)
     ,@Descripcion AS VARCHAR(MAX)
     ,@Precio AS DECIMAL(18,0)
     ,@Stock AS INT
     ,@CodigoBarras AS VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    BEGIN TRANSACTION
        INSERT INTO [dbo].[Producto]
               ([Id]
               ,[IdSubCategoria]
               ,[Nombre]
               ,[Descripcion]
               ,[Precio]
               ,[Stock]
               ,[CodigoBarras])
         VALUES
               (@Id
               ,@IdSubCategoria
               ,@Nombre
               ,@Descripcion
               ,@Precio
               ,@Stock 
               ,@CodigoBarras)
               SELECT @Id
    COMMIT TRANSACTION
END