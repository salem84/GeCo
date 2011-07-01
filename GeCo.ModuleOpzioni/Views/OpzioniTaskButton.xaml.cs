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
using GeCo.ModuleOpzioni.ViewModels;

namespace GeCo.ModuleOpzioni.Views
{
    /// <summary>
    /// Interaction logic for DipendentiTaskButton.xaml
    /// </summary>
    public partial class OpzioniTaskButton : UserControl
    {
        public OpzioniTaskButton(OpzioniTaskButtonVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
