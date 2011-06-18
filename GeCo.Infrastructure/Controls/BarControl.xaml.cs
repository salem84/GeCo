using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace GeCo.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class BarControl : UserControl
    {
        /*public int HrDiscrezionali
        {
            get { return (int)GetValue(HrDiscrezionaliProperty); }
            set { SetValue(HrDiscrezionaliProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HrDiscrezionali.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HrDiscrezionaliProperty =
            DependencyProperty.Register("HrDiscrezionali", typeof(int), typeof(BarControl), new UIPropertyMetadata(0));*/

        public BarControl()
        {
            this.InitializeComponent();
        }
    }
}