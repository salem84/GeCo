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
using GeCo.ModuleDipendenti.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace GeCo.ModuleDipendenti.Views
{
    /// <summary>
    /// Interaction logic for DipendentiWorkspace.xaml
    /// </summary>
    public partial class DipendentiWorkspaceContainer : UserControl
    {
        public DipendentiWorkspaceContainer(DipendentiWorkspaceContainerVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
