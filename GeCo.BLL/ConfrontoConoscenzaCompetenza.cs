using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;

namespace GeCo.BLL
{
    public class ConfrontoConoscenzaCompetenza
    {
        public Competenza Competenza { get; set; }
        public LivelloConoscenza LivelloConoscenzaAtteso { get; set; }
        public LivelloConoscenza LivelloConoscenzaOsservato { get; set; }
    }
}
