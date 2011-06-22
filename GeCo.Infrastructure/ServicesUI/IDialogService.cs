using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.Infrastructure.ServicesUI
{
    public interface IDialogService
    {
        string[] GetOpenFileDialog(string title, string filter);
        string GetSaveFileDialog(string title, string filter);
    }
}
