using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo;
using GeCo.DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using GeCo.BLL.AlgoritmoRicerca;
using System.Windows.Input;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using Microsoft.Practices.ServiceLocation;
using GeCo.BLL.Services;
using GeCo.Infrastructure;

namespace GeCo.ModuleDipendenti.ViewModels
{
    public class ConfrontoDipendenteDetailsVM : Workspace
    {
        #region PROPRIETA'

        protected override string containerName { get { return Names.MODULE_NAME; } }

        /// <summary>
        /// Macrogruppo selezionato nella combobox (Tecniche, Comportamentali, HR)
        /// </summary>
        private string _macroGruppoSelected;
        public string MacroGruppoSelected
        {
            get { return _macroGruppoSelected; }
            set
            {
                _macroGruppoSelected = value;
                RaisePropertyChanged("MacroGruppoSelected");
                
                //Aggiorno la listview delle competenze del dipendente
                UpdateConoscenzeGroup();
            }
        }

        /// <summary>
        /// Istanza del soggetto osservato
        /// </summary>
        private Anagrafica _osservato;
        public Anagrafica Osservato
        {
            get { return _osservato; }
            set 
            { 
                _osservato = value;
                RaisePropertyChanged("Osservato");
                AvviaConfronto();
                //UpdateConoscenzeGroup();    
            }
        }


        /// <summary>
        /// Istanza del soggetto atteso
        /// </summary>
        private Anagrafica _atteso;
        public Anagrafica Atteso
        {
            get { return _atteso; }
            set
            {
                _atteso = value;
                RaisePropertyChanged("Atteso");
                AvviaConfronto();
                //UpdateConoscenzeGroup();
            }
        }      
        
        //è come un soggetto, ma contiene due valori per competenze
        private ConfrontoSoggetti _confrontoSoggetti;
        public ConfrontoSoggetti ConfrontoSoggetti
        {
            get { return _confrontoSoggetti; }
            set
            {
                if (_confrontoSoggetti != value)
                {
                    _confrontoSoggetti = value;
                    UpdateConoscenzeGroup();
                    RaisePropertyChanged("ConfrontoSoggetti");
                }
            }
        }
        




        /// <summary>
        /// CollectionView delle conoscenze raggruppate per tipologia (e filtrate per macrogruppo)
        /// </summary>
        private ICollectionView _conoscenzePerTipologia;
        public ICollectionView ConoscenzePerTipologia
        {
            get { return _conoscenzePerTipologia; }
            set
            {
                if (_conoscenzePerTipologia == value) return;
                _conoscenzePerTipologia = value;
                RaisePropertyChanged("ConoscenzePerTipologia");
            }
        }


        /// <summary>
        /// Lista dei macrogruppi
        /// </summary>
        public List<string> MacroGruppi 
        { 
            get 
            {
                return Tipologiche.Macrogruppi.GetAll();
            } 
        }

        #endregion //PROPRIETA'



        
        /// <summary>
        /// Costruttore senza parametri
        /// </summary>
        public ConfrontoDipendenteDetailsVM()
        {
            DisplayTabName = "Dettagli confronto";
            //StartBackgroundAutoProgress(CreaNuovoDipendente);

            Atteso = null;
            Osservato = null;
            ConfrontoSoggetti = null;
        }


        private void AvviaConfronto()
        {
            //Mi sono ritornate quelle in comune?
            /*var conoscenzeComuni = Atteso.Conoscenze.Intersect(Osservato.Conoscenze, x => x.CompetenzaId);

            var confronto = from c in conoscenzeComuni
                            select new ConfrontoConoscenzaCompetenza()
                            {
                                Competenza = c.Competenza,
                                LivelloConoscenzaAtteso = 
                            };*/

            //Non ho settato i valori, non posso fare il confronto
            if (Osservato != null && Atteso != null)
            {

                var confrontoSog = new ConfrontoSoggetti();
                confrontoSog.Conoscenze = new List<ConfrontoConoscenzaCompetenza>();

                foreach (var conoscenzaAtteso in Atteso.Conoscenze)
                {
                    var conoscenzaOsservato = Osservato.Conoscenze.SingleOrDefault(c => c.CompetenzaId == conoscenzaAtteso.CompetenzaId);

                    if (conoscenzaOsservato != null)
                    {
                        ConfrontoConoscenzaCompetenza confronto = new ConfrontoConoscenzaCompetenza();
                        confronto.Competenza = conoscenzaAtteso.Competenza;
                        confronto.LivelloConoscenzaAtteso = conoscenzaAtteso.LivelloConoscenza;
                        confronto.LivelloConoscenzaOsservato = conoscenzaOsservato.LivelloConoscenza;
                        confrontoSog.Conoscenze.Add(confronto);
                    }

                }

                ConfrontoSoggetti = confrontoSog;
            }
        }
        

        
        //private void LoadDipendente()
        //{
           
        //    var service = ServiceLocator.Current.GetInstance<IDipendentiServices>();
        //    Osservato = service.CaricaDipendente(_dipendenteId);


        //    //Può essere null magari perchè ho cancellato quell'entità ed è rimasta aperta la scheda
        //    if (Osservato == null)
        //    {
        //        //TODO
        //        Stato = "Utente non trovato";
        //    }
            
        //}






        
        //private void AvviaConfronto()
        //{
        //    //RisultatiFiguraPerDipendenteViewModel visualizza = new RisultatiFiguraPerDipendenteViewModel(dipendente);
        //    var confrontoMaster = ServiceLocator.Current.GetInstance<ConfrontoDipendenteMasterVM>();
        //    confrontoMaster.Dipendente = Osservato;
        //    confrontoMaster.AddToShell();
        //}

        


       /// <summary>
        /// Metodo richiamato quando cambio la selezione del macrogruppo (aggiorno le conoscenze visualizzate)
        /// </summary>
        private void UpdateConoscenzeGroup()
        {
            if (ConfrontoSoggetti != null)
            {
                var conoscenzeList = ConfrontoSoggetti.Conoscenze.Where(c => c.Competenza.TipologiaCompetenza.MacroGruppo == MacroGruppoSelected).OrderBy(c => c.Competenza.Id);
                ConoscenzePerTipologia = CollectionViewSource.GetDefaultView(conoscenzeList);
                ConoscenzePerTipologia.GroupDescriptions.Add(new PropertyGroupDescription()
                {

                    PropertyName = "Competenza.TipologiaCompetenza.Titolo"
                });
            }

        }

        
    }

    public class ConfrontoSoggetti
    {
        public List<ConfrontoConoscenzaCompetenza> Conoscenze { get; set; }
    }

    public class ConfrontoConoscenzaCompetenza
    {
        public Competenza Competenza { get; set; }
        public LivelloConoscenza LivelloConoscenzaAtteso { get; set; }
        public LivelloConoscenza LivelloConoscenzaOsservato { get; set; }
    }

}
