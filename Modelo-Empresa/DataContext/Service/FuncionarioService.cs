using Modelo_Empresa.DataContext.Repository;
using Modelo_Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataContext.Service
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepository _repository;

        public FuncionarioService(FuncionarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<FuncionarioModel> ListarFuncionarios()
        {
            return _repository.GetAll();
        }

        //public FuncionarioModel ObterFuncionarioPorId(int id)
        //{
        //    return _repository.ObterPorId(id);
        //}

        public void AdicionarFuncionario(string nome, int salario, string departamento, string projeto1, string projeto2)
        {
            var novoFuncionario = new FuncionarioModel()
            {
                Nome = nome,
                Salario = salario,
                Departamento = departamento,
                Projeto1 = projeto1,
                Projeto2 = projeto2
            };

            _repository.Add(novoFuncionario);
        }

        public void AtualizarFuncionario(FuncionarioModel funcionario)
        {
            _repository.Update(funcionario);
        }

        public void RemoverFuncionario(FuncionarioModel funcionario)
        {
            _repository.Remove(funcionario);
        }
    }

}
