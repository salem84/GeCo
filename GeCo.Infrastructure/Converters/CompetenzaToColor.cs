using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using GeCo.Infrastructure;
using System.Windows.Media;
using D = System.Drawing;

namespace GeCo.Converters
{
    public class CompetenzaToColor : IMultiValueConverter
    {
        /*public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string titoloCompetenza = value.ToString();
            SolidColorBrush brush = new SolidColorBrush();
            D.Color color;

            switch(titoloCompetenza)
            {
                case Tipologiche.TipologiaCompetenza.HR_DISCREZIONALI:
                    color = ColoriPalette.HR_DISCREZIONALI;
                    break;
                case Tipologiche.TipologiaCompetenza.HR_C_RELAZIONALI:
                case Tipologiche.TipologiaCompetenza.HR_C_REALIZZATIVE:
                case Tipologiche.TipologiaCompetenza.HR_C_MANAGERIALI:
                case Tipologiche.TipologiaCompetenza.HR_C_COGNITIVE:
                    color = ColoriPalette.HR_COMPORTAMENTALI;
                    break;
                
                default:
                    color = D.Color.Black;
                    break;
            }

            brush.Color = Color.FromRgb(color.R, color.G, color.B);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }*/
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string titoloCompetenza = values[0].ToString();
            string macrogruppo = values[1].ToString();
            SolidColorBrush brush = new SolidColorBrush();
            D.Color color;

            if (macrogruppo == Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE)
            {
                color = ColoriPalette.HR_DISCREZIONALI;
            }
            else if (macrogruppo == Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE)
            {
                color = ColoriPalette.HR_COMPORTAMENTALI;
            }
            else if (macrogruppo == Tipologiche.Macrogruppi.MG_COMPORTAMENTALE)
            {
                color = ColoriPalette.COMPORTAMENTALI;
            }
            else if (macrogruppo == Tipologiche.Macrogruppi.MG_TECNICO)
            {
                if (titoloCompetenza == Tipologiche.TipologiaCompetenza.T_STRATEGIC_SUPPORT)
                {
                    color = ColoriPalette.TECN_STRATEGIC;
                }
                else if (titoloCompetenza == Tipologiche.TipologiaCompetenza.T_COMPETITIVE_ADVANTAGE)
                {
                    color = ColoriPalette.TECN_COMPETITIVE;
                }
                else //if (titoloCompetenza == Tipologiche.TipologiaCompetenza.T_FOUNDATIONAL)
                {
                    color = ColoriPalette.TECNICHE;
                }
            }
            else
            {
                color = D.Color.Black;
            }

            brush.Color = Color.FromRgb(color.R, color.G, color.B);
            return brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
