﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace GeCo.DAL
{
    //Contiene gli utenti collegati ai rispettivi valori delle competenze
    public class Dipendente : Anagrafica
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataNascita { get; set; }
        
        //public int IndiceTotale
        //{
        //    get { return this.Conoscenze.Sum(c => c.Indice); }
        //}

        public Dipendente()
        {
            Conoscenze = new ObservableCollection<ConoscenzaCompetenza>();
        }
    }
}
