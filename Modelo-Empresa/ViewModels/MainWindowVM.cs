using Modelo_Empresa.Models;
using Modelo_Empresa.Services;
using Modelo_Empresa.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Modelo_Empresa.ViewModels
{
     class MainWindowVM : INotifyPropertyChanged
    {
       

        public ObservableCollection<FuncionarioModel> listaFuncionarios { get; set; }
        public ObservableCollection<ProjetoModel> listaProjetos { get; set; }
        private FuncionarioModel novoFuncionario;
        public ICommand AdicionarFuncionarioCommand { get; private set; }
        public ICommand AbrirFuncionariosCommand { get; private set; }
        public ICommand AbrirProjetosCommand { get; private set; }


        public MainWindowVM()
        {
            listaFuncionarios = new ObservableCollection<FuncionarioModel>();
            listaProjetos = new ObservableCollection<ProjetoModel>();
           // AdicionarFuncionarioCommand = new RelayCommand(AdicionarFuncionario);
            AbrirFuncionariosCommand = new RelayCommand(AbrirFuncionarios);
            AbrirProjetosCommand = new RelayCommand(AbrirProjetos);
        }

        //private void AdicionarFuncionario(object parameter)
        //{
        //    FuncionarioModel novoFuncionario = new FuncionarioModel
        //    {
        //        Nome,
        //        Salario,
        //        Departamento = Departamento,
        //        Projeto1,
        //        Projeto2
        //    };

        //    listaFuncionarios.Add(novoFuncionario);

        //    Nome = "";
        //    Salario = 0;
        //    Departamento = "";
        //    Projeto1 = "";
        //    Projeto2 = "";
        //    Notifica(nameof(novoFuncionario));
        //}

        private void AbrirFuncionarios(object parameter)
        {
            FuncionarioV funcionarioWindow = new FuncionarioV();
            funcionarioWindow.ShowDialog();
        }

        private void AbrirProjetos(object parameter)
        {
            ProjetoV projetoWindow = new ProjetoV();
            projetoWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(String propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
