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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Windows.Controls.Ribbon;
using GeCo.ModuleRuoli.ViewModels;

namespace GeCo.ModuleRuoli.Views
{
    /// <summary>
    /// Interaction logic for RuoliRibbonTab.xaml
    /// </summary>
    public partial class RuoliRibbonTab : RibbonTab, IRegionMemberLifetime
    {
        public RuoliRibbonTab(RuoliRibbonTabVM viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        #region IRegionMemberLifetime Members

        public bool KeepAlive
        {
            get { return false; }
        }

        #endregion
    }
}
