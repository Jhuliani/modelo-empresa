using Modelo_Empresa.DataBase;
using Modelo_Empresa.Models;
using Modelo_Empresa.Services;
using Modelo_Empresa.Views;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Modelo_Empresa.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private IDataBase _connection;

        public ObservableCollection<FuncionarioModel> listaFuncionarios { get; set; }
        public ObservableCollection<ProjetoModel> listaProjetos { get; set; }

        public ICommand AdicionarFuncionario { get; private set; }
        public ICommand RemoverFuncionario { get; private set; }
        public ICommand AtualizarFuncionario { get; private set; }
        public ICommand AbrirFuncionariosCommand { get; private set; }
        public ICommand AbrirProjetosCommand { get; private set; }
        public ICommand AdicionarProjeto { get; private set; }
        public ICommand RemoverProjeto { get; private set; }
        public ICommand AtualizarProjeto { get; private set; }


       // public FuncionarioModel FuncionarioSelecionado { get; set; }
        public ProjetoModel ProjetoSelecionado { get; set; }

        private FuncionarioModel _funcionarioSelecionado;
        public FuncionarioModel FuncionarioSelecionado
        {
            get { return _funcionarioSelecionado; }
            set
            {
                _funcionarioSelecionado = value;
                Notifica(nameof(FuncionarioSelecionado));
                OnFuncionarioSelecionado();
            }
        }


        public MainWindowVM()
        {
            _connection = new PostgresDb();
            listaFuncionarios = new ObservableCollection<FuncionarioModel>(_connection.ListarFuncionarios());
            listaProjetos = new ObservableCollection<ProjetoModel>(_connection.ListarProjetos());
            AbrirProjetosCommand = new RelayCommand(AbrirProjetos);
            AbrirFuncionariosCommand = new RelayCommand(AbrirFuncionarios);
            InicializarFuncionarioCommands();
            InicializarProjetosCommands();
            CarregarDadosFuncionarios();
        }


        public void OnFuncionarioSelecionado()
        {
            if (FuncionarioSelecionado != null)
            {
                Nome = FuncionarioSelecionado.Nome;
                Salario = FuncionarioSelecionado.Salario;
                Departamento = FuncionarioSelecionado.Departamento;
                Projeto1 = FuncionarioSelecionado.Projeto1;
                Projeto2 = FuncionarioSelecionado.Projeto2;
            }
        }

        public void InicializarFuncionarioCommands()
        {
            AdicionarFuncionario = new RelayCommand((object _) =>
            {
                FuncionarioModel novoFuncionario = new FuncionarioModel();

                try
                {
                    _connection.AdicionarFuncionario(novoFuncionario);
                    listaFuncionarios.Clear();
                    listaFuncionarios = new ObservableCollection<FuncionarioModel>(_connection.ListarFuncionarios());
                    Notifica(nameof(listaFuncionarios));
                    MessageBox.Show("Funcionário inserido");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir Funcionário \n"
                    + ex.Message);
                }

            });

            RemoverFuncionario = new RelayCommand((object _) =>
            {
                if (FuncionarioSelecionado != null)
                {
                    try
                    {
                        _connection.RemoverFuncionario(FuncionarioSelecionado);
                        listaFuncionarios.Clear();
                        listaFuncionarios = new ObservableCollection<FuncionarioModel>(_connection.ListarFuncionarios());
                        Notifica(nameof(listaFuncionarios));
                        MessageBox.Show("Funcionário excluído!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao deletar Funcionário \n"
                            + ex.Message);
                    }
                }
            });

            AtualizarFuncionario = new RelayCommand((object _) =>
            {
                try
                {
                    _connection.AtualizarFuncionario(FuncionarioSelecionado);
                    listaFuncionarios.Clear();
                    listaFuncionarios = new ObservableCollection<FuncionarioModel>(_connection.ListarFuncionarios());
                    Notifica(nameof(listaFuncionarios));
                    MessageBox.Show("Funcionário atualizado!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar Funcionário \n"
                        + ex.Message);
                }


            });
        }


        public void InicializarProjetosCommands()
        {
            AdicionarProjeto = new RelayCommand((object _) =>
            {
                ProjetoModel novoProjeto = new ProjetoModel();
                try
                {
                    _connection.AdicionarProjeto(novoProjeto);
                    listaProjetos.Clear();
                    listaProjetos = new ObservableCollection<ProjetoModel>(_connection.ListarProjetos());
                    Notifica(nameof(listaProjetos));
                    MessageBox.Show("Projeto inserido");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir Projeto \n"
                    + ex.Message);
                }

            });

            RemoverProjeto = new RelayCommand((object _) =>
            {
                if (ProjetoSelecionado != null)
                {
                    try
                    {
                        _connection.RemoverProjeto(ProjetoSelecionado);
                        listaProjetos.Clear();
                        listaProjetos = new ObservableCollection<ProjetoModel>(_connection.ListarProjetos());
                        Notifica(nameof(listaProjetos));
                        MessageBox.Show("Projeto excluído!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao deletar Projeto \n"
                            + ex.Message);
                    }
                }
            });

            AtualizarProjeto = new RelayCommand((object _) =>
            {

                try
                {
                    _connection.AtualizarProjeto(ProjetoSelecionado);
                    listaProjetos.Clear();
                    listaProjetos = new ObservableCollection<ProjetoModel>(_connection.ListarProjetos());
                    Notifica(nameof(listaProjetos));
                    MessageBox.Show("Projeto atualizado!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar Projeto \n"
                        + ex.Message);
                }

            });

        }
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

        private void CarregarDadosFuncionarios()
        {

            var dados = _connection.ListarFuncionarios();
            foreach (var dado in dados)
            {
                listaFuncionarios.Add(dado);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
