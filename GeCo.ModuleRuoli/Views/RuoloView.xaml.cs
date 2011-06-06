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
using System.Diagnostics;

namespace GeCo.ModuleRuoli.Views
{
    /// <summary>
    /// Interaction logic for FiguraProfessionaleView.xaml
    /// </summary>
    public partial class RuoloView : UserControl
    {
        public RuoloView()
        {
            //BindingErrorListener.Listen(m => MessageBox.Show(m));
            //BindingErrorListener.Listen(m => { Debug.Indent(); Debug.WriteLine(m); Debug.Flush(); });
            InitializeComponent();
        }
    }
}
