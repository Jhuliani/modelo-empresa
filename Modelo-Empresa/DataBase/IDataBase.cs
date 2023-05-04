using Modelo_Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataBase
{
    public interface IDataBase 
    {
        List<FuncionarioModel> ListarFuncionarios();
        bool AdicionarFuncionario(FuncionarioModel funcionario);
        bool AtualizarFuncionario(FuncionarioModel funcionario);
        bool RemoverFuncionario(FuncionarioModel funcionario);

        List<ProjetoModel> ListarProjetos();
        bool AdicionarProjeto(ProjetoModel projeto);
        bool AtualizarProjeto(ProjetoModel projeto);
        bool RemoverProjeto(ProjetoModel projeto);
    }
}
