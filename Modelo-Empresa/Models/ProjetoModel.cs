using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Empresa.Models
{
    public class ProjetoModel 
    {
        private string _nome;
        private DateTime _dataInicio;
        private DateTime _dataFim;
        private string _observacao;

        public ProjetoModel()
        {

        }

        public ProjetoModel(string nome, DateTime dataIn, DateTime dataFim, string obs)
        {
            _nome = nome;
            _dataInicio = dataIn;
            _dataFim = dataFim;
            _observacao = obs;
        }

        
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public DateTime DataInicio
        {
            get { return _dataInicio; }
            set { _dataInicio = value; }
        }
        public DateTime DataFim
        {
            get { return _dataFim; }
            set { _dataFim = value; }
        }
        public string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }


    }
}
