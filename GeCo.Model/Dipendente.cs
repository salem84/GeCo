using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeCo.Model
{
    //Contiene gli utenti collegati ai rispettivi valori delle competenze
    public class Dipendente : Anagrafica
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string Matricola { get; set; }

        public virtual Ruolo RuoloInAzienda { get; set; }
        public int RuoloInAziendaId { get; set; }

        public Dipendente()
        {
            Conoscenze = new ObservableCollection<ConoscenzaCompetenza>();
        }
    }
}
