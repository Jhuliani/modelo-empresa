
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
