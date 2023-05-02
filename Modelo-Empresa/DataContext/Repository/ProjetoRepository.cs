using Modelo_Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataContext.Repository
{
    public class ProjetoRepository
    {
        private readonly MyContext _context;

        public ProjetoRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<ProjetoModel> Listar()
        {
            return _context.Projetos.ToList();
        }

        public ProjetoModel ObterPorNome(string nome)
        {
            return _context.Projetos.FirstOrDefault(p => p.Nome == nome);
        }

        public void Adicionar(ProjetoModel projeto)
        {
            _context.Projetos.Add(projeto);
            _context.SaveChanges();
        }

        public void Atualizar(ProjetoModel projeto)
        {
            _context.Projetos.Update(projeto);
            _context.SaveChanges();
        }

        public void Remover(ProjetoModel proj)
        {
            var projeto = ObterPorNome(proj.Nome);
            _context.Projetos.Remove(projeto);
            _context.SaveChanges();
        }
        
    }

}
