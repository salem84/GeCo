using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace GeCo.Converters
{
    [ValueConversion(typeof(object), typeof(double))]
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        { 
            double dblValue = (double)value; 
            double scale = Double.Parse(((string)parameter), System.Globalization.CultureInfo.InvariantCulture.NumberFormat); 
            return dblValue * scale; 
        } 

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            throw new NotImplementedException(); 
        }
    }
}
