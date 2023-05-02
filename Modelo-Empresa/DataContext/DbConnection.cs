using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataContext
{
    public class DbConnection
    {
        private static string sqlServerConnection = "Data Source=localhost; Initial Catalog=MeuBancoDeDados; Integrated Security=True;";
        public static SqlConnection SqlServerConnection = new SqlConnection(sqlServerConnection);
    }
}
