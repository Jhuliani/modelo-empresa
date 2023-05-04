using Modelo_Empresa.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataBase
{
    public class PostgresDb : IDataBase
    {
        private readonly NpgsqlConnection _connection;
        readonly string connectionString = ConfigurationReader.GetConnectionString("MyConnectionString");


        public PostgresDb(DbConnection connec)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }


        public bool AdicionarFuncionario(FuncionarioModel funcionario)
        {
            try
            {
                string sql = @"INSERT INTO Funcionario (nomefunc,cpf, salario, departamento, projeto1, projeto2)
                          VALUES (@Nome, @Cpf, @Salario, @Departamento, @Projeto1, @Projeto2)";

                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);

                command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                command.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                command.Parameters.AddWithValue("@Salario", funcionario.Salario);
                command.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                command.Parameters.AddWithValue("@Projeto1", funcionario.Projeto1);
                command.Parameters.AddWithValue("@Projeto2", funcionario.Projeto2);

                var result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro ao Inserir funcionário!");
            }
        }

        public bool RemoverFuncionario(FuncionarioModel funcionario)
        {
            try
            {
                string sql = $"DELETE FROM Funcionario WHERE cpf = {funcionario.Cpf}";
                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);
                {
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Não foi possível deletar funcionário!");
            }
        }

        public bool AtualizarFuncionario(FuncionarioModel funcionario)
        {
            string sql = @"UPDATE Funcionario
                   SET nomefunc = @NovoNome, salario = @Salario, departamento = @Departamento, projeto1 = @Projeto1, projeto2 = @Projeto2 
                   WHERE cpf = @Cpf";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("nome", funcionario.Nome);
                    command.Parameters.AddWithValue("cpf", funcionario.Cpf);
                    command.Parameters.AddWithValue("salario", funcionario.Salario);
                    command.Parameters.AddWithValue("departamento", funcionario.Departamento);
                    command.Parameters.AddWithValue("projeto1", funcionario.Projeto1);
                    command.Parameters.AddWithValue("projeto2", funcionario.Projeto2);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Não foi possível atualizar!");
            }

        }

        public IEnumerable<FuncionarioModel> ListarFuncionarios()
        {
            string commandText = "SELECT * FROM Funcionario";
            List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(commandText, _connection))
                {
                    using NpgsqlDataReader reader = command.ExecuteReader();                    

                    while (reader.Read())
                    {
                        var funcionario = new FuncionarioModel
                        {
                            Cpf = reader["cpf"].ToString(),
                            Nome = reader["nomefunc"].ToString(),
                            Salario = Convert.ToInt32(reader["salario"]),
                            Departamento = reader["departamento"].ToString(),
                            Projeto1 = reader["projeto1"].ToString(),
                            Projeto2 = reader["projeto2"].ToString()
                        };

                        funcionarios.Add(funcionario);
                    }                
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro ao listar!");
            }
            return funcionarios;
        }

                
        public bool AdicionarProjeto(ProjetoModel projeto)
        {
            try
            {
                string sql = @"INSERT INTO Projeto (nomeprojeto, datainicio, datafim, observacao)
                      VALUES (@NomeProjeto, @DataInicio, @DataFim, @Observacao)";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@NomeProjeto", projeto.Nome);
                    command.Parameters.AddWithValue("@DataInicio", projeto.DataInicio);
                    command.Parameters.AddWithValue("@DataFim", projeto.DataFim);
                    command.Parameters.AddWithValue("@Observacao", projeto.Observacao);

                    var result = command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Erro ao inserir projeto!");
            }
        }

        public bool RemoverProjeto(ProjetoModel projeto)
        {
            try
            {
                string sql = "DELETE FROM projeto WHERE nome = @nome";

                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);
                command.Parameters.AddWithValue("@nome", projeto.Nome);

                int result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro ao remover projeto!");
            }
        }

        public bool AtualizarProjeto(ProjetoModel projeto)
        {
            try
            {
                string sql = @"UPDATE projeto SET 
                       nome = @Nome, data_inicio = @DataInicio, data_fim = @DataFim,
                       observacao = @Observacao WHERE nome = @Nome";

                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);

                command.Parameters.AddWithValue("@Nome", projeto.Nome);
                command.Parameters.AddWithValue("@DataInicio", projeto.DataInicio);
                command.Parameters.AddWithValue("@DataFim", projeto.DataFim);
                command.Parameters.AddWithValue("@Observacao", projeto.Observacao);
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro ao atualizar projeto!");
            }
        }

        public IEnumerable<ProjetoModel> ListarProjetos()
        {
            try
            {
                string sql = "SELECT * FROM projeto";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<ProjetoModel> projetos = new List<ProjetoModel>();

                        while (reader.Read())
                        {
                            ProjetoModel projeto = new ProjetoModel();

                            projeto.Nome = reader["nome"].ToString();
                            projeto.DataInicio = Convert.ToDateTime(reader["data_inicio"]);
                            projeto.DataFim = Convert.ToDateTime(reader["data_fim"]);
                            projeto.Observacao = reader["observacao"].ToString();

                            projetos.Add(projeto);
                        }

                        return projetos;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro ao listar projetos!");
            }
        }
    }
}
