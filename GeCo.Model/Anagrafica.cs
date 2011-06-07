using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.Model
{
    //E' l'entità astratta base per Anagrafica e FiguraProfessionale
    public abstract class Anagrafica : BaseIdentityModel<Anagrafica>
    {
        public virtual ICollection<ConoscenzaCompetenza> Conoscenze { get; set; }

        public Anagrafica()
        {
            Conoscenze = new List<ConoscenzaCompetenza>();
        }
    }
}
