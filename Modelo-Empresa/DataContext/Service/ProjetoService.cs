using Modelo_Empresa.DataContext.Repository;
using Modelo_Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataContext.Service
{
    public class ProjetoService
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetoService(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public List<ProjetoModel> ObterTodos()
        {
            return _projetoRepository.Listar().ToList();
        }

        public ProjetoModel ObterPorNome(string nome)
        {
            return _projetoRepository.ObterPorNome(nome);
        }

        public void AdicionarProjeto(string nome, DateTime dataIn, DateTime datafim, string obs)
        {
            var projeto = new ProjetoModel()
            {
                Nome = nome,
                DataInicio = dataIn,
                DataFim = datafim,
                Observacao = obs
            };

            _projetoRepository.Adicionar(projeto);
        }

        public void AtualizarProjeto(ProjetoModel projeto)
        {
            _projetoRepository.Atualizar(projeto);
        }

        public void RemoverProjeto(ProjetoModel projeto)
        {
            _projetoRepository.Remover(projeto);
        }
    }

}
