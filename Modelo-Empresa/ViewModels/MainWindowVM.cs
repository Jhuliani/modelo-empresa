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

        public ICommand Adicionar { get; private set; }
        public ICommand Remover { get; private set; }
        public ICommand Atualizar { get; private set; }

        public FuncionarioModel FuncionarioSelecionado { get; set; }
        public ProjetoModel ProjetoSelecionado { get; set; }
        public Opcoes OpcaoSelecionada { get; set; }


        public MainWindowVM()
        {
            _connection = new PostgresDb();
            listaFuncionarios = new ObservableCollection<FuncionarioModel>(_connection.ListarFuncionarios());
            listaProjetos = new ObservableCollection<ProjetoModel>(_connection.ListarProjetos());
            InicializarCommands();
        }


        public void InicializarCommands()
        {

            Adicionar = new RelayCommand((object _) =>
            {
                if (OpcaoSelecionada == Opcoes.Funcionarios)
                {
                    FuncionarioModel novoFuncionario = new FuncionarioModel();
                    FuncionarioV projetoWindow = new FuncionarioV();
                    projetoWindow.DataContext = novoFuncionario;
                    bool? resultadoDialog = projetoWindow.ShowDialog();
                    if (resultadoDialog.HasValue && resultadoDialog.Value == true)
                    {
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
                    }
                }
                else if (OpcaoSelecionada == Opcoes.Projetos)
                {
                    ProjetoModel novoProjeto = new ProjetoModel();
                    ProjetoV projetoWindow = new ProjetoV();
                    projetoWindow.DataContext = novoProjeto;
                    bool? resultadoDialog = projetoWindow.ShowDialog();
                    if (resultadoDialog.HasValue && resultadoDialog.Value == true)
                    {
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
                    }
                }

            }, (object _) => OpcaoSelecionada == Opcoes.Funcionarios || OpcaoSelecionada == Opcoes.Projetos);

            Remover = new RelayCommand((object _) =>
            {
                if (OpcaoSelecionada == Opcoes.Funcionarios)
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
                }
                else if (OpcaoSelecionada == Opcoes.Projetos)
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
                }

            }, (object _) => OpcaoSelecionada == Opcoes.Funcionarios || OpcaoSelecionada == Opcoes.Projetos);

            Atualizar = new RelayCommand((object _) =>
            {
                if (OpcaoSelecionada == Opcoes.Funcionarios)
                {
                    FuncionarioV funcionarioWindow = new FuncionarioV();
                    funcionarioWindow.DataContext = FuncionarioSelecionado;
                    bool? resultadoDialog = funcionarioWindow.ShowDialog();
                    if (resultadoDialog.HasValue && resultadoDialog.Value == true)
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
                    }
                }
                else if (OpcaoSelecionada == Opcoes.Projetos)
                {
                    ProjetoV projetoWindow = new ProjetoV();
                    projetoWindow.DataContext = ProjetoSelecionado;
                    bool? resultadoDialog = projetoWindow.ShowDialog();
                    if (resultadoDialog.HasValue && resultadoDialog.Value == true)
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
                    }
                }

            }, (object _) => OpcaoSelecionada == Opcoes.Funcionarios || OpcaoSelecionada == Opcoes.Projetos);

        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
