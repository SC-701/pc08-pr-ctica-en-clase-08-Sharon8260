using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }


        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProductos";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio= producto.Precio,
                Stock= producto.Stock,
                CodigoBarras = producto.CodigoBarras

            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            await verificarProductoExiste(Id);
            string query = @"EditarProductos";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras

            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarProductoExiste(Id);
            string query = @"EliminarProductos";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = "ObtenerProductos";

            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(
                query,
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query,
                new { Id = Id });
            return resultadoConsulta.FirstOrDefault();
        }

        private async Task verificarProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("El producto no existe.");
        }
    }
}
