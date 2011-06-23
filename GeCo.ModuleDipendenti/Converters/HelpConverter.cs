using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using GeCo.Infrastructure;
using GeCo.ModuleDipendenti.ViewModels;

namespace GeCo.Converters
{
    public class HelpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var activeWorkspace = IoC.GetActiveWorkspace<DipendentiWorkspaceContainerVM>();
            string idActiveWorkspace = activeWorkspace.IdWorkspace;
            var panel = (HelpPanel) Application.Current.Resources[idActiveWorkspace];

            switch(value.ToString())
            {
                case "Titolo":
                    return panel.Titolo;
                case "Testo":
                    return panel.Testo;
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
