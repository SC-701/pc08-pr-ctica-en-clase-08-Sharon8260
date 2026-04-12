using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.BCCR;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class BancoCentralServicio : ITipoCambioServicio
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguracion _configuracion;

        public BancoCentralServicio(IHttpClientFactory httpClientFactory, IConfiguracion configuracion)
        {
            _httpClientFactory = httpClientFactory;
            _configuracion = configuracion;
        }

        public async Task<decimal> ObtenerTipoCambioVenta()
        {
            var cliente = _httpClientFactory.CreateClient();

            var urlBase = _configuracion.ObtenerValor("BancoCentralCR:UrlBase");
            var token = _configuracion.ObtenerValor("BancoCentralCR:BearerToken");

            cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var fecha = DateTime.Now.ToString("yyyy/MM/dd");

            var endpoint = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            var response = await cliente.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error consultando el BCCR");

            var contenido = await response.Content.ReadAsStringAsync();

            var resultado = JsonSerializer.Deserialize<BCCR>(contenido,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return ExtraerTipoCambio(resultado);
        }

        private decimal ExtraerTipoCambio(BCCR data)
        {
            var valor = data?.Datos?
                .SelectMany(d => d.Indicadores)
                .SelectMany(i => i.Series)
                .FirstOrDefault()?.ValorDatoPorPeriodo;

            if (valor == null)
                throw new Exception("No se encontró tipo de cambio");

            return valor.Value;
        }
    }
}