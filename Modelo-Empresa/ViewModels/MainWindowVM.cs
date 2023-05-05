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

        public Opcoes OpcaoSelecionada { get; set; }
        public ObservableCollection<FuncionarioModel> listaFuncionarios { get; set; }
        public ObservableCollection<ProjetoModel> listaProjetos { get; set; }

        public ICommand Adicionar { get; private set; }
        public ICommand Remover { get; private set; }
        public ICommand Atualizar { get; private set; }

        public FuncionarioModel FuncionarioSelecionado { get; set; }
        public ProjetoModel ProjetoSelecionado { get; set; }

       
        public MainWindowVM()
        {
            _connection = new PostgresDb();
            listaFuncionarios = new ObservableCollection<FuncionarioModel>(_connection.ListarFuncionarios());
            listaProjetos = new ObservableCollection<ProjetoModel>(_connection.ListarProjetos());
            InicializarCommands();
        }


        public void InicializarCommands()
        {
            if (OpcaoSelecionada == (Opcoes)1 )
            {
                Adicionar = new RelayCommand((object _) =>
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

                });

                Remover = new RelayCommand((object _) =>
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

                Atualizar = new RelayCommand((object _) =>
                {
                    FuncionarioV projetoWindow = new FuncionarioV();
                    projetoWindow.DataContext = FuncionarioSelecionado;
                    bool? resultadoDialog = projetoWindow.ShowDialog();
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

                });
            }
            else if (OpcaoSelecionada == (Opcoes)2)
            {
                Adicionar = new RelayCommand((object _) =>
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

                });

                Remover = new RelayCommand((object _) =>
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

                Atualizar = new RelayCommand((object _) =>
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

                });
            }
            else
            {
              //  desabilitar botoes
            }
        }


       

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
