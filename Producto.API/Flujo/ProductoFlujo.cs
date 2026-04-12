using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private readonly IProductoDA _productoDA;
        private readonly IProductoReglas _productoReglas;
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas, ITipoCambioServicio tipoCambioServicio)
        {
            _productoDA = productoDA;
            _productoReglas = productoReglas;
            _tipoCambioServicio = tipoCambioServicio;
        }

        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _productoDA.Agregar(producto);
        }

        public Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return _productoDA.Editar(Id, producto);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _productoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            var productos = (await _productoDA.Obtener())?.ToList();

            if (productos == null || productos.Count == 0)
                return productos;

            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVenta();

            foreach (var p in productos)
            {
                var precioCRC = Convert.ToDecimal(p.Precio);
                var precioUSD = await _productoReglas.ConvertirCRCaUSD(precioCRC, tipoCambio);

                p.PrecioUSD = precioUSD.ToString();
            }

            return productos;
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);

            if (producto == null)
                return null;

            var precioCRC = Convert.ToDecimal(producto.Precio);
            var precioUSD = await _productoReglas.ConvertirCRCaUSD(precioCRC);

            producto.PrecioUSD = precioUSD.ToString();

            return producto;
        }
    }
}
