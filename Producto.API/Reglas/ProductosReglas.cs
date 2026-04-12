using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<decimal> ConvertirCRCaUSD(decimal precioCRC)
        {
            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVenta();
            return await ConvertirCRCaUSD(precioCRC, tipoCambio);
        }

        public Task<decimal> ConvertirCRCaUSD(decimal precioCRC, decimal tipoCambio)
        {
            if (tipoCambio <= 0)
                throw new Exception("Tipo de cambio inválido");

            var precioUSD = precioCRC / tipoCambio;

            return Task.FromResult(Math.Round(precioUSD, 2));
        }
    }
}