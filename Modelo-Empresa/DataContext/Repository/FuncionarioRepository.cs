using Microsoft.EntityFrameworkCore;
using Modelo_Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.DataContext.Repository
{
    public class FuncionarioRepository
    {
        private readonly MyContext _context;

        public FuncionarioRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<FuncionarioModel> GetAll()
        {
            return _context.Funcionarios.ToList();
        }

        public FuncionarioModel GetById(int id)
        {
            return _context.Funcionarios.Find(id);
        }

        public void Add(FuncionarioModel funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
        }

        public void Update(FuncionarioModel funcionario)
        {
            _context.Entry(funcionario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(FuncionarioModel func)
        {
            var funcionario = _context.Funcionarios.Find( func);
            _context.Funcionarios.Remove(funcionario);
            _context.SaveChanges();
        }

        //public IEnumerable<FuncionarioModel> GetByDepartamento(string departamento)
        //{
        //    string sql = "SELECT * FROM Funcionarios WHERE Departamento = @departamento";
        //    return _context.Funcionarios.FromSqlRaw(sql, departamento).ToList();
        //}
    }

}
