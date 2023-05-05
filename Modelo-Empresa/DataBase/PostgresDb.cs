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
        private string connectionString = "Host=localhost; Port=5432; Username=postgres; password=030395; Database=Empresa-Luz";


        public PostgresDb()
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }


        public bool AdicionarFuncionario(FuncionarioModel funcionario)
        {
            try
            {
                string sql = @"INSERT INTO Funcionario (cpf, nomefunc, salario, departamento, projeto1, projeto2)
                          VALUES (@cpf, @nomefunc, @salario, @departamento, @projeto1, @projeto2)";

                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);


                command.Parameters.AddWithValue("cpf", funcionario.Cpf);
                command.Parameters.AddWithValue("nomefunc", funcionario.Nome);
                command.Parameters.AddWithValue("salario", funcionario.Salario);
                command.Parameters.AddWithValue("departamento", funcionario.Departamento);
                command.Parameters.AddWithValue("projeto1", funcionario.Projeto1);
                command.Parameters.AddWithValue("Projeto2", funcionario.Projeto2);

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
                string sql = "DELETE FROM Funcionario WHERE cpf = @cpf";

                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);
                command.Parameters.AddWithValue("@cpf", funcionario.Cpf);

                int result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Não foi possível deletar funcionário!");
            }
        }

        public bool AtualizarFuncionario(FuncionarioModel funcionario)
        {
            string sql = @"UPDATE Funcionario
                   SET nomefunc = @nomefunc, salario = @salario, departamento = @departamento, projeto1 = @projeto1, projeto2 = @projeto2 
                   WHERE cpf = @cpf";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("nomefunc", funcionario.Nome);
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

        public List<FuncionarioModel> ListarFuncionarios()
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
                string sql = @"INSERT INTO Projeto (nome, dataInicio, dataFim, observacao)
                      VALUES (@nome, @dataInicio, @dataFim, @observacao)";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("nome", projeto.Nome);
                    command.Parameters.AddWithValue("dataInicio", projeto.DataInicio);
                    command.Parameters.AddWithValue("dataFim", projeto.DataFim);
                    command.Parameters.AddWithValue("observacao", projeto.Observacao);

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
                command.Parameters.AddWithValue("nome", projeto.Nome);

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
                        dataIinicio = @dataInicio, dataFim = @dataFim,
                       observacao = @observacao WHERE nome = @nome";

                using NpgsqlCommand command = new NpgsqlCommand(sql, _connection);

                command.Parameters.AddWithValue("novoNome", projeto.Nome);
                command.Parameters.AddWithValue("DataInicio", projeto.DataInicio);
                command.Parameters.AddWithValue("DataFim", projeto.DataFim);
                command.Parameters.AddWithValue("Observacao", projeto.Observacao);
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro ao atualizar projeto!");
            }
        }

        public List<ProjetoModel> ListarProjetos()
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
                            projeto.DataInicio = Convert.ToDateTime(reader["dataInicio"]);
                            projeto.DataFim = Convert.ToDateTime(reader["dataFim"]);
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
