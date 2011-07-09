using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo;
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
using GeCo.BLL;
using GeCo.Infrastructure.ServicesUI;

namespace GeCo.ModuleRuoli.ViewModels
{
    public class ConfrontoRuoloDetailsVM : Workspace
    {
        #region PROPRIETA'

        protected override string containerName { get { return Names.MODULE_NAME; } }
        public override string IdWorkspace { get { return Names.VIEW_CONFRONTO_DETAILS; } }

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

        public ParametriConfronto ParametriConfronto { get; private set; }

        #endregion //PROPRIETA'


        private IExcelServices _excelServices;
        
        
        public ConfrontoRuoloDetailsVM(IExcelServices excelServices)
        {
            DisplayTabName = "Dettagli confronto";
            ParametriConfronto = new ParametriConfronto();
            //StartBackgroundAutoProgress(CreaNuovoDipendente);

            _excelServices = excelServices;

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
                //Mi serve solo per la visualizzazione (non c'è salvataggio di dati in questo caso, così non mi serve ricaricarla da DB)
                var livelloNullo = new LivelloConoscenza() { Titolo = Tipologiche.Livello.INSUFFICIENTE, Valore = 0 };

                var confrontoSog = new ConfrontoSoggetti();
                confrontoSog.Conoscenze = new List<ConfrontoConoscenzaCompetenza>();

                foreach (var conoscenzaAtteso in Atteso.Conoscenze)
                {
                    var conoscenzaOsservato = Osservato.Conoscenze.SingleOrDefault(c => c.CompetenzaId == conoscenzaAtteso.CompetenzaId);

                    //Il dipendente possiede quella conoscenza
                    if (conoscenzaOsservato != null)
                    {
                        ConfrontoConoscenzaCompetenza confronto = new ConfrontoConoscenzaCompetenza();
                        confronto.Competenza = conoscenzaAtteso.Competenza;
                        confronto.LivelloConoscenzaAtteso = conoscenzaAtteso.LivelloConoscenza;
                        confronto.LivelloConoscenzaOsservato = conoscenzaOsservato.LivelloConoscenza;
                        confrontoSog.Conoscenze.Add(confronto);
                    }
                    //se non la possiede, devo visualizzare un valore nullo
                    else
                    {
                        ConfrontoConoscenzaCompetenza confronto = new ConfrontoConoscenzaCompetenza();
                        confronto.Competenza = conoscenzaAtteso.Competenza;
                        confronto.LivelloConoscenzaAtteso = conoscenzaAtteso.LivelloConoscenza;
                        confronto.LivelloConoscenzaOsservato = livelloNullo;
                        confrontoSog.Conoscenze.Add(confronto);
                    }

                }

                ConfrontoSoggetti = confrontoSog;
            }
        }
        

       


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

        
        public void EsportaExcel()
        {
            IDialogService dialogService = new DialogService();
            string filename = dialogService.GetSaveFileDialog("Salva Report Excel", "Cartella di lavoro di Excel (*.xlsx) | .xlsx");

            //Atteso dovrebbe essere un Dipendente
            var ruolo = Atteso as Ruolo;
            
            //Osservato dovrebbe essere un Ruolo
            var dipendente = Osservato as Dipendente;

            string titolo = string.Format("Confronto tra Ruolo {0} e Dipendente #{1}", ruolo.Titolo, dipendente.Matricola);
            _excelServices.EsportaExcel(filename, titolo, ConfrontoSoggetti.Conoscenze);
        }

    }

    public class ConfrontoSoggetti
    {
        public List<ConfrontoConoscenzaCompetenza> Conoscenze { get; set; }
    }

   

}
