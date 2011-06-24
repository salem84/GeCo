using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
//using GeCo.DAL;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Exp = System.Linq.Expressions;
using LinqKit;
using System.Windows.Data;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using GeCo.BLL.Services;
using Microsoft.Practices.ServiceLocation;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class RicercaDipendentiViewModel : Workspace, INotifyPropertyChanged
    {
        #region PROPRIETA'

        protected override string containerName { get { return Names.MODULE_NAME; } }
        public override string IdWorkspace { get { return Names.VIEW_RICERCA; } }

        public string RicercaMatricola { get; set; }
        public string RicercaNome { get; set; }
        public string RicercaCognome { get; set; }
        public DateTime? RicercaDataNascita { get; set; }

        private List<Dipendente> _risultati;
        public List<Dipendente> Risultati
        {
            get { return _risultati; }
            set
            {
                if (_risultati == value) return;
                _risultati = value;
                RaisePropertyChanged("Risultati");
            }
        }

        public Dipendente SelectedItem { get; set; }

        //Azione che viene lanciata quando faccio click su un elemento della listview
        //Viene settata nella Shell
        /*public Action<Dipendente> VisualizzaDipendenteAction
        {
            set
            {
                DoubleClickCommand = new RelayCommand(() => value(SelectedItem));
            }
        }*/
        //private PavimentalDb context = new PavimentalDb();

        #endregion //PROPRIETA'

        public RelayCommand CercaCommand { get; set; }
        public RelayCommand DoubleClickCommand { get; set; }

        private IDipendentiServices _services;

        public RicercaDipendentiViewModel(IDipendentiServices services)
        {
            DisplayTabName = "Ricerca Dipendente";

            _services = services;

            CercaCommand = new RelayCommand(Cerca, () => !string.IsNullOrEmpty(RicercaMatricola) ||
                !string.IsNullOrEmpty(RicercaNome) || 
                !string.IsNullOrEmpty(RicercaCognome) ||
                RicercaDataNascita != null);

            DoubleClickCommand = new RelayCommand(VisualizzaDettaglioDipendente);
        }

        

        protected void Cerca()
        {
            Expression<Func<Dipendente, bool>> complete = PredicateBuilder.True<Dipendente>();
            Expression<Func<Dipendente, bool>> exprMatricola = a => a.Matricola == RicercaMatricola;
            Expression<Func<Dipendente, bool>> exprNome = a => a.Nome.Contains(RicercaNome);
            Expression<Func<Dipendente, bool>> exprCognome = a => a.Cognome.Contains(RicercaCognome);
            Expression<Func<Dipendente, bool>> exprData = a => a.DataNascita == RicercaDataNascita;

            if (!string.IsNullOrEmpty(RicercaMatricola))
            {
                complete = complete.And(exprMatricola);
            }
            else
            {
                if (!string.IsNullOrEmpty(RicercaNome))
                {
                    complete = complete.And(exprNome);
                }
                if (!string.IsNullOrEmpty(RicercaCognome))
                {
                    complete = complete.And(exprCognome);
                }
                if (RicercaDataNascita != null)
                {
                    complete = complete.And(exprData);
                }
            }

            //AsExpendable Preso da LinqKit
            /*using (PavimentalContext context = new PavimentalContext())
            {
                Risultati = context.Dipendenti.AsExpandable().Where(complete).ToList();
            }*/

            
            //Risultati = services.GetDipendenti().Cast<Dipendente>().Where(complete).ToList();
            Risultati = _services.GetDipendenti(complete).ToList();
        }


        private void VisualizzaDettaglioDipendente()
        {
            if (SelectedItem != null)
            {
                DipendenteViewModel dipendenteVM = new DipendenteViewModel(SelectedItem);
                dipendenteVM.AddToShell();
            }
        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return ((DateTime)value).ToShortDateString();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            return DateTime.TryParse(strValue, out resultDateTime) ? resultDateTime : value;
        }
    }
}
