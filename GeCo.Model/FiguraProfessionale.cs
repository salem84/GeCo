using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeCo.Model
{
    //Contiene le posizioni lavorative (capo area, muratore,...)
    public class FiguraProfessionale : Anagrafica
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public Area Area { get; set; }
        
        //public int IndiceTotale
        //{
        //    get { return this.Conoscenze.Sum(c => c.Indice); }
        //}
        //public ICollection<Anagrafica> Dipendenti { get; set; }
    }  
}