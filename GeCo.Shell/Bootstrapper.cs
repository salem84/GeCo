using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Windows.Controls.Ribbon;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Logging;
using GeCo.Shell.Views;

namespace GeCo.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        private const string ModuleCatalogUri = "/GeCo.Shell;component/ModuleCatalog.xaml";

        /// <summary>
        /// Crea la finestra principale, creando ShellWindow attraverso il container
        /// </summary>
        protected override DependencyObject CreateShell()
        {
            ShellWindow window = this.Container.TryResolve<ShellWindow>();
            return window;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        /// <summary>
        /// Inizializza il catalogo per la creazione dei moduli aggiuntivi
        /// (si potrebbe utilizzare DirectoryModuleCatalog)
        /// </summary>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //Carica tutti i moduli definiti nello XAML
            //return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
            //    new Uri(ModuleCatalogUri, UriKind.Relative));

            var moduleCatalog = new DirectoryModuleCatalog();
            moduleCatalog.ModulePath = @".\";
            return moduleCatalog;
        }

        /// <summary>
        /// Devo usare il RegionAdapter per il Ribbon
        /// </summary>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            // Richiamo metodo base
            var mappings = base.ConfigureRegionAdapterMappings();
            if (mappings == null) return null;

            // Aggiungo l'adapter per il ribbon
            mappings.RegisterMapping(typeof(Ribbon), ServiceLocator.Current.GetInstance<RibbonRegionAdapter>());

            return mappings;
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);

            //Nascondo il messaggio di caricamento
            ShellWindow shell = (ShellWindow)this.Shell;
            shell.LoadCompleted();

            //Time bomb
            bool expired = false;// TimeBomb();
            if (expired)
                throw new Exception("tm");
        }


        private bool TimeBomb()
        {
            if (DateTime.Now > new DateTime(2011, 7, 7))
                return true;
            else
                return false;
        }

        

        /// <summary>
        /// Creo il logger
        /// </summary>
        /// <returns></returns>
        //protected override ILoggerFacade CreateLogger()
        //{
        //    return base.CreateLogger();
        //}
    }
}
