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

namespace GeCo.ModuleDipendenti.Views
{
    /// <summary>
    /// Interaction logic for DipendentiRibbonTab.xaml
    /// </summary>
    public partial class DipendentiRibbonTab : RibbonTab, IRegionMemberLifetime
    {
        public DipendentiRibbonTab()
        {
            InitializeComponent();
        }

        #region IRegionMemberLifetime Members

        public bool KeepAlive
        {
            get { return false; }
        }

        #endregion
    }
}
