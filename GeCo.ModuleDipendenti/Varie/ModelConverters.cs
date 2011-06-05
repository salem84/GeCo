using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using GeCo.DAL;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using GeCo.BLL.Services;

namespace GeCo.Converters
{
    //Vedere se usare il Multibinding... devo comunque inserirli da DB
    public class LivelloConoscenzaConverter : IValueConverter
    {

        //Mi arriva il LivelloConoscenza e devo restituire il Valore
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int id = System.Convert.ToInt32(value);

            //using (PavimentalContext context = new PavimentalContext())
            //    return context.LivelliConoscenza.Single(lc => lc.Id == id).Valore;
            var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            int livelloValore = service.GetLivelliConoscenza().Single(lc => lc.Id == id).Valore;
            return livelloValore;
        }

        //Mi arriva il valore letto dalla TextBox e devo restituire l'oggetto
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //questo è il valore
            int valore = System.Convert.ToInt16(value);

            //LivelloConoscenza livello;
            int livelloId;

            //using (PavimentalContext context = new PavimentalContext())
            //    livelloId = context.LivelliConoscenza.Single(lc => lc.Valore == valore).Id;

            var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
            livelloId = service.GetLivelliConoscenza().Single(lc => lc.Valore == valore).Id;
            

            return livelloId;
        }
    }

    public class LivelloConoscenzaValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            /*Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(value.ToString());
            return m.Success;*/
            string input = value as string;
            string[] validi = { "0", "1", "2", "3", "4" };
            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Vuoto");
            }
            else if (!validi.Contains(input))
            {
                return new ValidationResult(false, "Non valido");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
