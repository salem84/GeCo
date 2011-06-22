using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace GeCo.Infrastructure.ServicesUI
{
    public class DialogService : IDialogService
    {
        public string[] GetOpenFileDialog(string title, string filter)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = true;
            dialog.Title = title;
            dialog.Filter = filter;
            if ((bool)dialog.ShowDialog())
            {
                return dialog.SafeFileNames;
            }
            return new string[0];
        }

        public string GetSaveFileDialog(string title, string filter)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = title;
            dialog.Filter = filter;
            if ((bool)dialog.ShowDialog())
            {
                //return dialog.SafeFileName; //E' solo il nome del file
                return dialog.FileName;
            }
            return "";
        }

    }
}
