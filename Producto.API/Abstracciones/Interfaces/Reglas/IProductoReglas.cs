using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IProductoReglas
    {
        Task<decimal> ConvertirCRCaUSD(decimal precioCRC);
        Task<decimal> ConvertirCRCaUSD(decimal precioCRC, decimal tipoCambio);
    }
}
