using Microsoft.Extensions.Configuration;
using System.IO;

namespace Modelo_Empresa.DataBase
{
    public static class ConfigurationReader
    {
        public static string GetConnectionString(string name)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            return configuration.GetConnectionString(name);
        }
    }
}
