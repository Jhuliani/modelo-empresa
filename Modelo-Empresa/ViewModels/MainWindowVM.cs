﻿using Modelo_Empresa.DataContext.Service;
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
        private readonly FuncionarioService _funcionarioService; 
        private readonly ProjetoService _projetoService;

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


        public FuncionarioModel SelectedFuncionario { get; set; }
        public ProjetoModel SelectedProjeto { get; set; }


        public MainWindowVM(FuncionarioService funcionarioService, ProjetoService projetoService)
        {
            listaFuncionarios = new ObservableCollection<FuncionarioModel>();
            listaProjetos = new ObservableCollection<ProjetoModel>();
            _funcionarioService = funcionarioService;
            _projetoService = projetoService;
            AbrirProjetosCommand = new RelayCommand(AbrirProjetos);
            InicializarFuncionarioCommands();
        }

        public void InicializarFuncionarioCommands()
        {
            AdicionarFuncionario = new RelayCommand((object _) =>
            {
                FuncionarioModel novoFuncionario = new FuncionarioModel();
                FuncionarioV projetoWindow = new FuncionarioV();
                projetoWindow.DataContext = novoFuncionario;
                bool? resultadoDialog = projetoWindow.ShowDialog();
                if (resultadoDialog.HasValue && resultadoDialog.Value == true)
                {
                    try
                    {
                        _funcionarioService.AdicionarFuncionario(novoFuncionario.Nome, novoFuncionario.Salario, novoFuncionario.Departamento, novoFuncionario.Projeto1, novoFuncionario.Projeto2);
                        listaFuncionarios.Clear();
                        listaFuncionarios = new ObservableCollection<FuncionarioModel>(_funcionarioService.ListarFuncionarios());
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

            RemoverFuncionario = new RelayCommand((object _) =>
            {
                if (SelectedFuncionario != null)
                {
                    try
                    {
                        _funcionarioService.RemoverFuncionario(SelectedFuncionario);
                        listaFuncionarios.Clear();
                        listaFuncionarios = new ObservableCollection<FuncionarioModel>(_funcionarioService.ListarFuncionarios());
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
                FuncionarioV projetoWindow = new FuncionarioV();
                projetoWindow.DataContext = SelectedFuncionario;
                bool? resultadoDialog = projetoWindow.ShowDialog();
                if (resultadoDialog.HasValue && resultadoDialog.Value == true)
                {
                    try
                    {
                        _funcionarioService.AtualizarFuncionario(SelectedFuncionario);
                        listaFuncionarios.Clear();
                        listaFuncionarios = new ObservableCollection<FuncionarioModel>(_funcionarioService.ListarFuncionarios());
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


        public void InicializarProjetosCommands()
        {
            AdicionarProjeto = new RelayCommand((object _) =>
            {
                ProjetoModel novoProjeto = new ProjetoModel();
                ProjetoV projetoWindow = new ProjetoV();
                projetoWindow.DataContext = novoProjeto;
                bool? resultadoDialog = projetoWindow.ShowDialog();
                if (resultadoDialog.HasValue && resultadoDialog.Value == true)
                {
                    try
                    {
                        _projetoService.AdicionarProjeto(novoProjeto.Nome, novoProjeto.DataInicio, novoProjeto.DataFim, novoProjeto.Observacao);
                        listaProjetos.Clear();
                        listaProjetos = new ObservableCollection<ProjetoModel>(_projetoService.ObterTodos());
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

            RemoverProjeto = new RelayCommand((object _) =>
            {
                if (SelectedProjeto != null)
                {
                    try
                    {
                        _projetoService.RemoverProjeto(SelectedProjeto);
                        listaProjetos.Clear();
                        listaProjetos = new ObservableCollection<ProjetoModel>(_projetoService.ObterTodos());
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
                ProjetoV projetoWindow = new ProjetoV();
                projetoWindow.DataContext = SelectedProjeto;
                bool? resultadoDialog = projetoWindow.ShowDialog();
                if (resultadoDialog.HasValue && resultadoDialog.Value == true)
                {
                    try
                    {
                        _projetoService.AtualizarProjeto(SelectedProjeto);
                        listaProjetos.Clear();
                        listaProjetos = new ObservableCollection<ProjetoModel>(_projetoService.ObterTodos());
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
