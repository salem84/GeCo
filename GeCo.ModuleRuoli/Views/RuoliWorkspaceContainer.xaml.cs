using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeCo.ModuleRuoli.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace GeCo.ModuleRuoli.Views
{
    /// <summary>
    /// Interaction logic for DipendentiWorkspace.xaml
    /// </summary>
    public partial class RuoliWorkspaceContainer : UserControl
    {
        public RuoliWorkspaceContainer(RuoliWorkspaceContainerVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string a = "";
            var  ba = header.Content;

            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            var vm = container.Resolve<RuoliWorkspaceContainerVM>();
        }
    }
}
