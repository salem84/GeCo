using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo;
using GeCo.DAL;
using System.Collections;
using System.Data.Entity;
using GalaSoft.MvvmLight;

namespace GeCo.ViewModel
{
    public class RicercaSostitutoViewModel : WorkspaceViewModel
    {
        private PavimentalDb db;

        public override string DisplayName
        {
            get
            {
                return "Ricerca Sostituto";
            }
        }

        private FigureProfessionali _livelloSelezionato;
        public FigureProfessionali LivelloSelezionato
        {
            get { return _livelloSelezionato; }
            set
            {
                if (_livelloSelezionato == value) return;
                _livelloSelezionato = value;
                //RaisePropertyChanged("LivelloSelezionato");
                LivelliSostitutivi = CalcolaIndiciEOrdina(Livelli).ToList();

                RaisePropertyChanged("LivelliSostitutivi");
            }
        }


        public List<FigureProfessionali> Livelli { get; set; }

        /*private List<LivelloConIndice> _livelliSostitutivi;
        public List<LivelloConIndice> LivelliSostitutivi 
        {
            get { return _livelliSostitutivi; }
            set
            {
                AnagraficaLivello = GetAnagrafica(

                RaisePropertyChanged("AnagraficaLivello");
            }
        }*/
        public List<LivelloConIndice> LivelliSostitutivi { get; set; }


        private LivelloConIndice _livelloSostitutivoSelezionato;
        public LivelloConIndice LivelloSostitutivoSelezionato
        {
            get { return _livelloSostitutivoSelezionato; }
            set
            {
                if (_livelloSostitutivoSelezionato == value) return;
                _livelloSostitutivoSelezionato = value;
                //RaisePropertyChanged("LivelloSelezionato");
                AnagraficaLivello = GetAnagrafica(_livelloSostitutivoSelezionato);

                RaisePropertyChanged("AnagraficaLivello");
            }
        }
        public List<Anagrafica> AnagraficaLivello { get; set; }

        public RicercaSostitutoViewModel()
        {
            if (!IsInDesignMode)
            {
                db = new PavimentalDb();
                Livelli = GetTuttiLivelli().ToList();
            }
        }

        protected IEnumerable<FigureProfessionali> GetTuttiLivelli()
        {
            /*var tuttiLivelli = db.FigureProfessionali
                .Include(l => l.Conoscenze.Select(c => c.Competenza))
                .Include(l => l.Conoscenze.Select(c => c.LivelloConoscenza)).AsEnumerable();

            return tuttiLivelli;*/
            return null;
        }

        protected IEnumerable<LivelloConIndice> CalcolaIndiciEOrdina(IEnumerable<FigureProfessionali> tuttiLivelli)
        {
            int indiceTotaleSelezionato = 0;//LivelloSelezionato.IndiceTotale;

            var livelliConIndice = (from livello in tuttiLivelli
                                    //where livello.IndiceTotale > indiceTotaleSelezionato
                                    select new LivelloConIndice
                                    {
                                        Livello = livello,
                                        Titolo = livello.Titolo,
                                        //IndicePercentuale = (double) livello.IndiceTotale / indiceTotaleSelezionato * 100.0
                                        //IndicePercentuale = (double)indiceTotaleSelezionato / livello.IndiceTotale * 100.0
                                        //IndicePercentuale = (float)livello.IndiceTotale - indiceTotaleSelezionato
                                    }).OrderBy(l => l.IndicePercentuale);

            return livelliConIndice;

        }

        protected List<Anagrafica> GetAnagrafica(LivelloConIndice livelloSostitutivoSelezionato)
        {
            /*var anagrafica = from dipendente in db.Anagrafica
                             where dipendente.Nome.StartsWith("g")
                             
                             select dipendente;*/
            int id = livelloSostitutivoSelezionato.Livello.Id;

            //var anagrafica = db.Anagrafica.Include(a => a.Livelli).Where(a => a.Livelli.Count(l => l.Id == id) != 0).ToList();
            
            
            /*var anagrafica = from dipendente in db.Anagrafica
                             where dipendente.Livelli.Count(l => l.Id == id) != 0
                             select dipendente;

            return anagrafica.ToList();*/
            return new List<Anagrafica>();
        }
    }

    public class LivelloConIndice
    {
        public FigureProfessionali Livello { get; set; }
        public string Titolo { get; set; }
        public float IndicePercentuale { get; set; }
    }
}
