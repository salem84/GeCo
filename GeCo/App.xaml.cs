using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using GeCo.ViewModel;
using System.IO;

namespace GeCo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool expired = TimeBomb();
            if (expired)
                throw new Exception("tm");

            //Configuro la directory dove sarà salvato il DB
            SetupDataDirectory();

            MainWindow window = new MainWindow();

            // Create the ViewModel to which 
            // the main window binds.
            var viewModel = new MainWindowViewModel();

            // When the ViewModel asks to be closed, 
            // close the window.
            viewModel.RequestClose += delegate
            {
                window.Close();
            };

            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down 
            // the element tree.
            window.DataContext = viewModel;

            window.Show();
        }

        private bool TimeBomb()
        {
            if (DateTime.Now > new DateTime(2011, 6, 30))
                return true;
            else
                return false;
        }

        private void SetupDataDirectory()
        {
            // This is our connection string: Data Source=|DataDirectory|\Chinook40.sdf
            // Set the data directory to the users %AppData% folder
            // So the Chinook40.sdf file must be placed in:  C:\\Users\\<Username>\\AppData\\Roaming\\

            //string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationInfo.Company, ApplicationInfo.ProductName);
            string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GeCo");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        }
       
    }
}
