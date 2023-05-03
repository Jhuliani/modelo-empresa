using Modelo_Empresa.DataContext;
using Modelo_Empresa.DataContext.Repository;
using Modelo_Empresa.DataContext.Service;
using Modelo_Empresa.Models;
using Modelo_Empresa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Modelo_Empresa.Views
{
    /// <summary>
    /// Lógica interna para FuncionarioV.xaml
    /// </summary>
    public partial class FuncionarioV : Window
    {
        public FuncionarioV()
        {
            InitializeComponent();
            //MyContext context = new MyContext();
            //FuncionarioRepository func = new FuncionarioRepository(context);
            //ProjetoRepository proj = new ProjetoRepository(context);
            //FuncionarioService funcionario = new FuncionarioService(func);
            //ProjetoService projeto = new ProjetoService(proj);
            DataContext = new FuncionarioModel();
        }
    }
}
