﻿using System;
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
using GeCo;
using GeCo.ModuleCompetenze.ViewModels;

namespace GeCo.ModuleCompetenze.Views
{
    /// <summary>
    /// Interaction logic for Livelli.xaml
    /// </summary>
    public partial class CompetenzeView : UserControl
    {
        public CompetenzeView(CompetenzeVM viewModel)
        {
            //BindingErrorListener.Listen(m => MessageBox.Show(m));
            InitializeComponent();
            this.DataContext = viewModel;
        }

        

    }
}
