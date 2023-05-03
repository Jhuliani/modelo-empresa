using Microsoft.EntityFrameworkCore;
using Modelo_Empresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataContext
{
    public class MyContext : DbContext
    {
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<ProjetoModel> Projetos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConnection.SqlServerConnection);
        }



        public void AdicionarFuncionario(FuncionarioModel funcionario)
        {
            string sql = "INSERT INTO FuncionarioModel (nomefunc, salario, departamento, projeto1, projeto2) " +
                         "VALUES (@nome, @salario, @departamento, @projeto1, @projeto2)";
            List<object> parametros = new List<object>();
            parametros.Add(funcionario.Nome);
            parametros.Add(funcionario.Salario);
            parametros.Add(funcionario.Departamento);
            parametros.Add(funcionario.Projeto1);
            parametros.Add(funcionario.Projeto2);

            this.Database.ExecuteSqlRaw(sql, parametros.ToArray());
        }

        public bool UpdateFuncionario(FuncionarioModel funcionario)
        {
            string sql = "UPDATE FuncionarioModel " +
                "SET nomefunc = @NovoNome, salario = @Salario, departamento = @Departamento, projeto1 = @Projeto1, projeto2 = @Projeto2 " +
                "WHERE nomefunc = @Nome";

            try
            {
                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@NovoNome", funcionario.Nome));
                    command.Parameters.Add(new SqlParameter("@Salario", funcionario.Salario));
                    command.Parameters.Add(new SqlParameter("@Departamento", funcionario.Departamento));
                    command.Parameters.Add(new SqlParameter("@Projeto1", funcionario.Projeto1));
                    command.Parameters.Add(new SqlParameter("@Projeto2", funcionario.Projeto2));
                    this.Database.OpenConnection();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't update data from database!!");
            }
        }

        public void RemoverFuncionario(string nome)
        {
            string sql = "DELETE FROM FuncionarioModel WHERE nomefunc = @nome";
            this.Database.ExecuteSqlRaw(sql, new SqlParameter("@nome", nome));
        }

        public void AdicionarProjeto(ProjetoModel projeto)
        {
            string sql = "INSERT INTO ProjetoModel (nomeproj, dataInicio, dataFim, observacao) " +
                         "VALUES (@nome, @dataInicio, @dataFim, @obs)";
            List<object> parametros = new List<object>();
            parametros.Add(projeto.Nome);
            parametros.Add(projeto.DataInicio);
            parametros.Add(projeto.DataFim);
            parametros.Add(projeto.Observacao);

            this.Database.ExecuteSqlRaw(sql, parametros.ToArray());
        }

        public bool UpdateProjeto(ProjetoModel projeto)
        {
            string sql = "UPDATE ProjetoModel " +
                "SET nomeproj = @nome, dataInicio = @dataInicio, dataFim = @dataFim, observacao = @Obs " +
                "WHERE nomeproj = @nome";

            try
            {
                using (var command = this.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@NovoNome", projeto.Nome));
                    command.Parameters.Add(new SqlParameter("@dataInicio", projeto.DataInicio));
                    command.Parameters.Add(new SqlParameter("@dataFim", projeto.DataFim));
                    command.Parameters.Add(new SqlParameter("@Obs", projeto.Observacao));

                    this.Database.OpenConnection();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nCouldn't update data from database!!");
            }
        }

        public void RemoverProjeto(string nome)
        {
            string sql = "DELETE FROM ProjetoModel WHERE nomeproj = @nome";
            this.Database.ExecuteSqlRaw(sql, new SqlParameter("@nome", nome));
        }
    }
}
