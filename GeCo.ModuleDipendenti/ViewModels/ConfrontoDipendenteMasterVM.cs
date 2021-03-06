﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.BLL.AlgoritmoRicerca;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using GeCo.Infrastructure;
using GeCo.BLL.Services;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Command;
using GeCo.BLL;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using GeCo.Infrastructure.Events;

namespace GeCo.ModuleDipendenti.ViewModels
{
    //Agganciato alla vista VisualizzaDipendente per effettuare la ricerca
    //delle figure professionali a partire da un dipendente
    public class ConfrontoDipendenteMasterVM : Workspace
    {
        protected override string containerName { get { return Names.MODULE_NAME; } }
        public override string IdWorkspace { get { return Names.VIEW_CONFRONTO_MASTER; } }

        private IEnumerable<RisultatoRicerca> _risultati;
        public IEnumerable<RisultatoRicerca> Risultati
        {
            get { return _risultati; }
            set
            {
                if (_risultati != value)
                {
                    _risultati = value;
                    RaisePropertyChanged("Risultati");
                }
            }
        }

        private Dipendente _dipendente;
        public Dipendente Dipendente 
        { 
            get
            {
                return _dipendente;
            }
            set
            {
                _dipendente = value;
                AvviaAnalisi();
            }
        }

        //public ParametriConfronto ParametriConfronto { get; set; }

        //Variabili utilizzate per la progress e lo status (totali e completate)
        private int FigureProfessionaliTotali { get; set; }
        private int FigureProfessionaliAnalizzate { get; set; }

        public ParametriConfronto ParametriConfronto { get; private set; }

        public ICommand DoubleClickCommand { get; set; }

        public RisultatoRicerca RisultatoSelezionato { get; set; }


        #region GRAFICO
        
        private bool _graficoVisibile;
        public bool GraficoVisibile
        {
            get { return _graficoVisibile; }
            set
            {
                if (_graficoVisibile != value)
                {
                    _graficoVisibile = value;
                    //Disabilito i controlli nel caso in cui sto visualizzando il grafico e viceversa
                    ControlliAbilitati = !value;
                    
                    ShowGrafico(_graficoVisibile);
                    RaisePropertyChanged("GraficoVisibile");
                }
            }
        }

        //Se visualizzo il grafico, disabilito tutto il resto
        private bool _controlliAbilitati;
        public bool ControlliAbilitati
        {
            get { return _controlliAbilitati; }
            set
            {
                if (_controlliAbilitati != value)
                {
                    _controlliAbilitati = value;
                    RaisePropertyChanged("ControlliAbilitati");
                }
            }
        }

        private List<double> _valoriGrafico;
        public List<double> ValoriGrafico
        {
            get { return _valoriGrafico; }
            set
            {
                if (_valoriGrafico != value)
                {
                    _valoriGrafico = value;
                    RaisePropertyChanged("ValoriGrafico");
                }
            }
        }

        private List<string> _labelsGrafico;
        public List<string> LabelsGrafico
        {
            get { return _labelsGrafico; }
            set
            {
                if (_labelsGrafico != value)
                {
                    _labelsGrafico = value;
                    RaisePropertyChanged("LabelsGrafico");
                }
            }
        }


        private List<System.Drawing.Color> _paletteColors;
        public List<System.Drawing.Color> PaletteColors
        {
            get { return _paletteColors; }
            set
            {
                if (_paletteColors != value)
                {
                    _paletteColors = value;
                    RaisePropertyChanged("PaletteColors");
                }
            }
        }

        private string _titoloGrafico;
        public string TitoloGrafico
        {
            get { return _titoloGrafico; }
            set
            {
                if (_titoloGrafico != value)
                {
                    _titoloGrafico = value;
                    RaisePropertyChanged("TitoloGrafico");
                }
            }
        }
        
        
        #endregion


        private bool _filtroRuoliDipendenti;
        public bool FiltroRuoliDipendenti
        {
            get { return _filtroRuoliDipendenti; }
            set
            {
                if (_filtroRuoliDipendenti != value)
                {
                    _filtroRuoliDipendenti = value;
                    AvviaAnalisi();
                    RaisePropertyChanged("FiltroRuoliDipendenti");
                }
            }
        }


        private ICompetenzeServices _competenzeServices;
        private IRicercaServices _ricercaServices;
        private IRuoliServices _ruoliServices;

        public ConfrontoDipendenteMasterVM(ICompetenzeServices compServices, IRuoliServices ruoliServices, IRicercaServices ricercaServices)
        {
            DisplayTabName = "Sostituti";

            _competenzeServices = compServices;
            _ruoliServices = ruoliServices;
            _ricercaServices = ricercaServices;

            DoubleClickCommand = new RelayCommand(VisualizzaConfrontoDetails);
            ParametriConfronto = new ParametriConfronto();           
                        
        }

        private void AvviaAnalisi()
        {
            DisplayTabName = "Sostituti per " + Dipendente.Cognome;

            //TODO rivedere
            //Non ho salvato il dipendente e mi mancano però le conoscenze per calcolare gli indici
            if (Dipendente.Id == 0)
            {
                var livelliConoscenza = _competenzeServices.GetLivelliConoscenza();
                
                foreach (var c in Dipendente.Conoscenze)
                {
                    c.LivelloConoscenza = livelliConoscenza.Single(lc => lc.Id == c.LivelloConoscenzaId);
                }   
            }

            //Non faccio l'autoprogress, setto il valore massimo pari al numero di elementi sul db da analizzare
            /*using (PavimentalContext context = new PavimentalContext())
            {
                FigureProfessionaliTotali = context.FigureProfessionali.Count();
            }*/

            //Faccio partire l'algoritmo in background
            StartBackgroundAutoProgress(AvviaAlgoritmoBackground);
        }

       /* private void LoadParametri()
        {
            //Carico i parametri da visualizzare
            ParametriConfronto = new ParametriConfronto();
        }*/

        protected void AvviaAlgoritmoBackground()
        {
            
            //Gli passo la funzione per fare l'aggiornamento del progressivo
            var tempRes = _ricercaServices.CercaRuoloDaDipendente(Dipendente, FiltroRuoliDipendenti);

            //Rielaboro i dati (ordino e nascondo le percentuali)
            Risultati = tempRes.OrderByDescending(r => r.Idoneo).ThenByDescending(r => r.PunteggioTotale);
            ControlliAbilitati = true;
        }

        protected void AggiornaProgress()
        {
            //FigureProfessionaliAnalizzate++;
            //ProgressPercent = FigureProfessionaliAnalizzate / FigureProfessionaliTotali * 100;
            //Stato = string.Format("Completato {0} di {1}", FigureProfessionaliAnalizzate, FigureProfessionaliTotali);

        }

        public void VisualizzaConfrontoDetails()
        {
            ConfrontoDipendenteDetailsVM confrontoDetailsVM = ServiceLocator.Current.GetInstance<ConfrontoDipendenteDetailsVM>();

            Ruolo ruoloSelezionato = _ruoliServices.CaricaRuolo(RisultatoSelezionato.Id);
            confrontoDetailsVM.Atteso = ruoloSelezionato;
            confrontoDetailsVM.Osservato = Dipendente;
            confrontoDetailsVM.AddToShell();
        }

        private void ShowGrafico(bool visible)
        {
            if (visible == true)
            {
                if (RisultatoSelezionato != null)
                {
                    int cifreDecimali = 1;
                    LabelsGrafico = new List<string>(
                        new string[] { 
                            "HR Discrezionali", 
                            "HR Comportamentali", 
                            "Comportamentali", 
                            "Tecniche Strategic Support", 
                            "Tecniche Competitive Advantage" });

                    TitoloGrafico = string.Format("Confronto tra Dipendente #{0} e Ruolo '{1}'", Dipendente.Matricola, RisultatoSelezionato.Nome);
                    
                    var valori = new List<double>();
                    var colors = new List<System.Drawing.Color>();
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioHrDiscrezionali, cifreDecimali));
                    colors.Add(ColoriPalette.HR_DISCREZIONALI);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioHrComportamentali, cifreDecimali));
                    colors.Add(ColoriPalette.HR_COMPORTAMENTALI);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioComportamentali, cifreDecimali));
                    colors.Add(ColoriPalette.COMPORTAMENTALI);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioTecnStrategic, cifreDecimali));
                    colors.Add(ColoriPalette.TECN_STRATEGIC);
                    valori.Add(Math.Round(RisultatoSelezionato.PunteggioTecnCompetitiveAdv, cifreDecimali));
                    colors.Add(ColoriPalette.TECN_COMPETITIVE);

                    PaletteColors = colors;
                    ValoriGrafico = valori;
                    
                    //GraficoVisibile = true;
                    //ControlliAbilitati = false;
                }
            }
            else
            {
                ValoriGrafico = null;
                LabelsGrafico = null;

            }

            //return GraficoVisibile;
        }    
    }
}
