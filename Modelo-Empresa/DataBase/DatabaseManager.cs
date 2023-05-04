using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataBase
{
    public class DatabaseManager
    {
        private readonly NpgsqlConnection _connection;
        
        public void CreateTableIfNotExistsFuncionario()
        {
            var sql = $"CREATE TABLE IF NOT EXISTS FuncionarioModel" +
                      $"(" +
                      $"cpf VARCHAR(14) PRIMARY KEY," +
                      $"nomefunc VARCHAR(200) NOT NULL," +
                      $"salario decimal NOT NULL," +
                      $"departamento VARCHAR(200) NOT NULL," +
                      $"projeto1 VARCHAR(200)," +
                      $"projeto2 VARCHAR(200)," +
                      $"CONSTRAINT cpf_length CHECK (char_length(cpf) = 11)" +
                      $")";

            using (var cmd = new NpgsqlCommand(sql, _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateTableIfNotExistsProjeto()
        {
            var sql = $"CREATE TABLE IF NOT EXISTS ProjetoModel" +
                      $"(" +
                      $"nome VARCHAR(200) PRIMARY KEY," +
                      $"dataInicio TIMESTAMP NOT NULL," +
                      $"dataFim TIMESTAMP NOT NULL," +
                      $"observacao VARCHAR(200)" +
                      $")";

            using (var cmd = new NpgsqlCommand(sql, _connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

    }
}
