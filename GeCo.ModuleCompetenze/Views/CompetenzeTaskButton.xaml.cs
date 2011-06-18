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
using GeCo.ModuleCompetenze.ViewModels;

namespace GeCo.ModuleCompetenze.Views
{
    /// <summary>
    /// Interaction logic for DipendentiTaskButton.xaml
    /// </summary>
    public partial class CompetenzeTaskButton : UserControl
    {
        public CompetenzeTaskButton(CompetenzeTaskButtonVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
