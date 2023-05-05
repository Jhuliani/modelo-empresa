using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.Models
{    
    public class FuncionarioModel 
    {
        private string _cpf;
        private string _nome;
        private int _salario;
        private string _departamento;
        private string _projeto1;
        private string _projeto2;

        public FuncionarioModel()
        {

        }

        public FuncionarioModel(string cpf, string nome, int salario, string depto, string proj1, string proj2)
        {
            _cpf = _cpf;
            _nome = nome;
            _salario = salario;
            _departamento = depto;
            _projeto1 = proj1;
            _projeto1 = proj2;
        }

        
        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Salario
        {
            get { return _salario; }
            set { _salario = value; }
        }
        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        public string Projeto1
        {
            get { return _projeto1; }
            set { _projeto1 = value; }
        }
        public string Projeto2
        {
            get { return _projeto2; }
            set { _projeto2 = value; }
        }

    }
}
