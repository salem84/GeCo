﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using GeCo.BLL.Services;
using GeCo.Model;

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
            var service = ServiceLocator.Current.GetInstance<ICompetenzeServices>();
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

            var service = ServiceLocator.Current.GetInstance<ICompetenzeServices>();
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
            string input = value.ToString();
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


    /*public class RuoloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (!(value is int))
                return null;           
            
            var id = System.Convert.ToInt32(value);
            if (id == 0)
                return null;

            var ruoliService = ServiceLocator.Current.GetInstance<IRuoliServices>();
            //var ruolo = ruoliService.GetRuoli(r => r.Id == id).Single();
            var ruolo = ruoliService.GetRuolo(id);

            return ruolo;
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ruolo = value as Ruolo;
            if (ruolo != null)
                return ruolo.Id;
            else
                return -1;
        }
    }*/
}
