using Abstracciones.Interfaces.DA;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly SqlConnection _conexionBaseDatos;

        public RepositorioDapper(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _conexionBaseDatos=new SqlConnection(_configuracion.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerRepositorio()
        {
            return _conexionBaseDatos;
        }
    }
}
