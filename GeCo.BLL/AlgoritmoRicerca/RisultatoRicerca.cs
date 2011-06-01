using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.BLL.AlgoritmoRicerca
{
    public class RisultatoRicerca
    {
        private const bool DEBUG = true;


        #region PARAMETRI

        /// <summary>
        /// Soglia percentuale di sbarramento per idoneità (da applicare su TECNICHE/FOUNDATIONAL)
        /// </summary>
        public int PERC_SogliaFoundational { get; set; }
        
        /// <summary>
        /// Punteggi Massimi (foglio Summury) per la media pesata
        /// </summary>
        public int PMAX_TecnStrategicSupport { get; set; }
        public int PMAX_TecnCompetitiveAdv { get; set; }
        public int PMAX_Comportamentali { get; set; }
        public int PMAX_Hr { get; set; }

        #endregion

        public string Nome { get; set; }
        public Punteggi PunteggioAtteso { get; set; }
        public Punteggi PunteggioOsservato { get; set; }

        #region PROPRIETA' CALCOLATE AUTOMATICAMENTE
        
        /// <summary>
        /// E' il valore minimo per essere idoneo, viene calcolato sulle competenze TECNICHE/FOUNDATIONAL
        /// </summary>
        public float PunteggioSbarramento
        {
            get { return (float)PunteggioAtteso.TecnFoundational * PERC_SogliaFoundational / 100.0f; }
        }

        /// <summary>
        /// Se il punteggio osservato è almeno uguale alla soglia di sbarramento 
        /// calcolato sulle competenze TECNICHE/FOUNDATIONAL
        /// </summary>
        public bool Idoneo
        {
            get { return PunteggioOsservato.TecnFoundational >= PunteggioSbarramento; }
        }

        #region PERCENTUALI HR - COMPORTAMENTALI - TECNICHE STRATEGIC - TECNICHE COMPETITIVE
        
        public float PercentualeHR
        {
            get
            {
                if (Idoneo || DEBUG)
                {
                    if(PunteggioAtteso.HR != 0)
                        return (float)PunteggioOsservato.HR / PunteggioAtteso.HR * PMAX_Hr;
                }
                
                return 0;
            }
        }

        public float PercentualeComportamentali
        {
            get
            {
                if (Idoneo || DEBUG)
                {
                    if (PunteggioAtteso.Comportamentali != 0)
                        return (float)PunteggioOsservato.Comportamentali / PunteggioAtteso.Comportamentali * PMAX_Comportamentali;
                }
                
                return 0;
            }
        }

        public float PercentualeTecnStrategic
        {
            get
            {
                if (Idoneo || DEBUG)
                {
                    if (PunteggioAtteso.TecnStrategicSupport != 0)
                        return (float)PunteggioOsservato.TecnStrategicSupport / PunteggioAtteso.TecnStrategicSupport * PMAX_TecnStrategicSupport;
                }
                    
                return 0;
            }
        }

        public float PercentualeTecnCompetitiveAdv
        {
            get
            {
                if (Idoneo || DEBUG)
                {
                    if (PunteggioAtteso.TecnCompetitiveAdv != 0)
                        return (float)PunteggioOsservato.TecnCompetitiveAdv / PunteggioAtteso.TecnCompetitiveAdv * PMAX_TecnCompetitiveAdv;
                }
                
                return 0;
            }
        }

        public float PercentualeTotale
        {
            get { return PercentualeHR + PercentualeComportamentali + PercentualeTecnStrategic + PercentualeTecnCompetitiveAdv; }
        }

        #endregion //PERCENTUALI

        #endregion //PROPRIETA' CALCOLATE AUTOMATICAMENTE
    }
}
