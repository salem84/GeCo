using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace GeCo.DAL
{
    public class ConoscenzaCompetenza : BaseIdentityModel<ConoscenzaCompetenza>
    {
        public int CompetenzaId { get; set; }
        public Competenza Competenza { get; set; }
        public int LivelloConoscenzaId { get; set; }
        public LivelloConoscenza LivelloConoscenza { get; set; }

        public int ConoscitoreId { get; set; }
        public virtual Anagrafica Conoscitore { get; set; }
        //public int Indice 
        //{
        //    get { return Competenza.Peso * this.LivelloConoscenza.Valore; }
        //}
    }
}