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
using GeCo;
using GeCo.DAL;
using System.Collections;
using System.Collections.ObjectModel;

namespace GeCo.Controls
{
    /// <summary>
    /// Interaction logic for CompetenzeUC.xaml
    /// </summary>
    public partial class CompetenzeUC : UserControl
    {
        public List<ConoscenzaCompetenza> KnowHowDipendente
        {
            get { return (List<ConoscenzaCompetenza>)GetValue(KnowHowDipendenteProperty); }
            set { SetValue(KnowHowDipendenteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KnowHowDipendente.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KnowHowDipendenteProperty =
            DependencyProperty.Register("KnowHowDipendente", typeof(List<ConoscenzaCompetenza>), typeof(CompetenzeUC),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(KnowHowDipendentePropertyChanged)));

        
        

        public CompetenzeUC()
        {

            //BindingErrorListener.Listen(m => MessageBox.Show(m));
            InitializeComponent();

            //provaedit.DataContext = this;
            //Prova = "3";
            //KnowHowDipendente = new List<ConoscenzaCompetenza>();
            //KnowHowDipendente.Add(new ConoscenzaCompetenza() { Id = 1 });
            //KnowHowDipendente.Add(new ConoscenzaCompetenza() { Id = 2 });
            //KnowHowDipendente.Add(new ConoscenzaCompetenza() { Id = 3 });
        }

        private static void KnowHowDipendentePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            
        }

        private void Salva_Click(object sender, RoutedEventArgs e)
        {
            List<ConoscenzaCompetenzaFlat> listaFlat = lvCompetenze.ItemsSource as List<ConoscenzaCompetenzaFlat>;
            

        }

    }


    [ValueConversion(typeof(List<ConoscenzaCompetenza>), typeof(List<ConoscenzaCompetenzaFlat>))]
    class ConoscenzeFlatter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<ConoscenzaCompetenza> knowHow = value as List<ConoscenzaCompetenza>;

            if (knowHow != null)
            {
                var competenzeFlat = from cc in knowHow
                                     select new ConoscenzaCompetenzaFlat()
                                     {
                                         //IdConoscenzaCompetenza = cc.Id,

                                         TipologiaCompetenza = cc.Competenza.TipologiaCompetenza.Titolo,

                                         IdCompetenza = cc.Competenza.Id,
                                         TitoloCompetenza = cc.Competenza.Titolo,

                                         IdLivelloConoscenza = cc.LivelloConoscenza.Id,
                                         ValoreLivelloConoscenza = cc.LivelloConoscenza.Valore
                                     };


                ObservableCollection<ConoscenzaCompetenzaFlat> ob = new ObservableCollection<ConoscenzaCompetenzaFlat>(competenzeFlat.ToList());
                return ob;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            object a = value;
            return null;
        }
    }

    class Convertitore : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "$" + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string v = value.ToString();
            return null;
        }
    }



    public class ConoscenzaCompetenzaFlat
    {
        public int IdConoscenzaCompetenza { get; set; }

        public string TipologiaCompetenza { get; set; } //Functional,...

        public int IdCompetenza { get; set; }
        public string TitoloCompetenza { get; set; }

        public int IdLivelloConoscenza { get; set; }
        public int ValoreLivelloConoscenza { get; set; }

    }
}
