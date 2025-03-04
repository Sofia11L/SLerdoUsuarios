using Microsoft.Extensions.Configuration;
using System.Configuration;
namespace DL
{
    public class Conexion
    {
        public readonly IConfiguration configuration;

        public Conexion (IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetStringConnection()
        {
            return configuration.GetConnectionString("SLerdoUsuarios");           

        }
    }
}
