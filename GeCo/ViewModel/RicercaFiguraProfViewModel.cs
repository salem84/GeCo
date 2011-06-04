using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using GeCo.DAL;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Exp = System.Linq.Expressions;
using GeCo.Varie;
using LinqKit;
using System.Windows.Data;
using System.Windows.Input;
using GeCo.Model;

namespace GeCo.ViewModel
{
    public class RicercaFiguraProfViewModel : WorkspaceViewModel, INotifyPropertyChanged
    {
        #region PROPRIETA'

        public string RicercaTitolo { get; set; }
        
        private List<FiguraProfessionale> _risultati;
        public List<FiguraProfessionale> Risultati
        {
            get { return _risultati; }
            set
            {
                if (_risultati == value) return;
                _risultati = value;
                RaisePropertyChanged("Risultati");
            }
        }

        public FiguraProfessionale SelectedItem { get; set; }

        //Azione che viene lanciata quando faccio click su un elemento della listview
        //Viene settata nella Shell
        public Action<FiguraProfessionale> VisualizzaFiguraProfAction
        {
            set
            {
                DoubleClickCommand = new RelayCommand(() => value(SelectedItem));
            }
        }
        //private PavimentalDb context = new PavimentalDb();

        #endregion //PROPRIETA'

        public ICommand CercaCommand { get; set; }
        public ICommand DoubleClickCommand { get; set; }

        public RicercaFiguraProfViewModel()
        {
            DisplayTabName = "Ricerca Figura";

            CercaCommand = new RelayCommand(
                () => Cerca(),
                () => !string.IsNullOrEmpty(RicercaTitolo)
                    );
        }

        protected void Cerca()
        {
            Expression<Func<FiguraProfessionale, bool>> complete = PredicateBuilder.True<FiguraProfessionale>();
            Expression<Func<FiguraProfessionale, bool>> exprNome = a => a.Titolo.Contains(RicercaTitolo);

            if (!string.IsNullOrEmpty(RicercaTitolo))
            {
                complete = complete.And(exprNome);
            }
            
            //AsExpendable Preso da LinqKit
            using (PavimentalContext context = new PavimentalContext())
            {
                Risultati = context.FigureProfessionali.AsExpandable().Where(complete).ToList();
            }
        }
    }

    /*public class DateConverter : IValueConverter
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
    }*/
}
