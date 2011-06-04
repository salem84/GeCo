using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeCo.Model
{
    //4 Valori: Livello 1 (Basso) - Livello 2 (Medio) - Livello 3 (Buono) - Livello 4 (Ottimo)
    public class LivelloConoscenza : BaseIdentityModel<LivelloConoscenza>
    {
        public string Titolo { get; set; }
        public int Valore { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is LivelloConoscenza)
            {
                return (obj as LivelloConoscenza).Id == this.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}