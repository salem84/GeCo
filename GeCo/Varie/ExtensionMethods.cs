using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Controls;
using GeCo;
using GeCo.DAL;
using GeCo.Model;

namespace GeCo.Varie
{
    public static class ExtensionMethods
    {
        public static IEnumerable<ConoscenzaCompetenzaFlat> ToFlat(this IEnumerable<ConoscenzaCompetenza> knowHow)
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
            
            return competenzeFlat.ToList();
        }

        
    }
}
