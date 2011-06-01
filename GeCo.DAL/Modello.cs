using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace GeCo.DAL
{
    //Contiene gli utenti collegati ai rispettivi valori delle competenze
    [Table("Anagrafica")]
	public class Anagrafica
	{
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataNascita { get; set; }
        //public ICollection<Livelli> Livelli { get; set; }
        public ICollection<ConoscenzaCompetenza> Conoscenze { get; set; }
        public int IndiceTotale
        {
            get { return this.Conoscenze.Sum(c => c.Indice); }
        }

        public Anagrafica()
        {
            Conoscenze = new ObservableCollection<ConoscenzaCompetenza>();
        }
	}

    [Table("FigureProfessionali")]
    //Contiene le posizioni lavorative (capo area, muratore,...)
    public class FigureProfessionali
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public Area Area { get; set; }
        public ICollection<ConoscenzaCompetenza> Conoscenze { get; set; }
        public int IndiceTotale
        {
            get { return this.Conoscenze.Sum(c => c.Indice); }
        }
        //public ICollection<Anagrafica> Dipendenti { get; set; }
    }

    //Sono le aree per le varie posizioni (professional, entry level, ...) rappresentate nella riga in alto dell'Excel
    [Table("Aree")]
    public class Area
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
    }

    [Table("Competenze")]
    public class Competenza
    {
        public int Id { get; set; }
        public string Titolo { get; set; } //Normative tecniche, Normative qualità
        public string Descrizione { get; set; }
        public int Peso { get; set; }
        public int TipologiaCompetenzaId { get; set; }
        public TipologiaCompetenza TipologiaCompetenza { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Competenza)
            {
                return (obj as Competenza).Id == this.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }

    

    [Table("TipologiaCompetenza")]
    public class TipologiaCompetenza
    {
        public int Id { get; set; }
        public string MacroGruppo { get; set; } //Competenza tecnica, psicologica, HR
        public string Titolo { get; set; } //Foundational, Strategic
    }

    /*[ComplexType]
    public class MacroGruppo
    {
        public string Titolo { get; set; }
    }*/


    [Table("ConoscenzeCompetenza")]
    public class ConoscenzaCompetenza
    {
        public int Id { get; set; }
        public int CompetenzaId { get; set; }
        public Competenza Competenza { get; set; }
        public int LivelloConoscenzaId { get; set; }
        public LivelloConoscenza LivelloConoscenza { get; set; }
        public int Indice 
        {
            get { return Competenza.Peso * this.LivelloConoscenza.Valore; }
        }
    }

    //4 Valori: Livello 1 (Basso) - Livello 2 (Medio) - Livello 3 (Buono) - Livello 4 (Ottimo)
    [Table("LivelliConoscenza")]
    public class LivelloConoscenza
    {
        public int Id { get; set; }
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