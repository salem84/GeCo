using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeCo.Model
{
    public class Competenza : BaseIdentityModel<Competenza>
    {
        public string Titolo { get; set; } //Normative tecniche, Normative qualità
        public string Descrizione { get; set; }
        public int Peso { get; set; }
        public int TipologiaCompetenzaId { get; set; }
        public virtual TipologiaCompetenza TipologiaCompetenza { get; set; }
    }
}