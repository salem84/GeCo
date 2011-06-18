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
using Microsoft.Windows.Controls.Ribbon;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using GeCo.BLL.Services;

namespace GeCo.Shell.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : RibbonWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        private void btnInizializza_Click(object sender, RoutedEventArgs e)
        {
            string message = "L'operazione cancellerà tutte le informazioni sul database. Sei sicuro?";
            string title = "Ripristino DB";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage images = MessageBoxImage.Warning;
            if (MessageBox.Show(message, title,buttons, images) == MessageBoxResult.Yes)
            {
                var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
                container.RegisterType<IDbAdmin, DbAdmin>();
                var dbAdmin = container.Resolve<IDbAdmin>();
                dbAdmin.InizializzaDb();



                container.RegisterType<IDipendentiServices, DipendentiServices>();
                var dipServices = container.Resolve<IDipendentiServices>();
                var d = dipServices.CaricaDipendente(8);
            }

            MessageBox.Show("Completato", title, MessageBoxButton.OK);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
