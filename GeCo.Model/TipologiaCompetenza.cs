using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeCo.Model
{ 
    public class TipologiaCompetenza : BaseIdentityModel<TipologiaCompetenza>
    {
        public string MacroGruppo { get; set; } //Competenza tecnica, psicologica, HR
        public string Titolo { get; set; } //Foundational, Strategic
    }
}