﻿using System;
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
        public int PMAX_HrDiscrezionali { get; set; }
        public int PMAX_HrComportamentali { get; set; }

        #endregion

        public string Nome { get; set; }
        public int Id { get; set; }

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

        #region PERCENTUALI HR - COMPORTAMENTALI - TECNICHE STRATEGIC - TECNICHE COMPETITIVE - TECNICHE FOUNDATIONAL
        
        public float PunteggioHrDiscrezionali
        {
            get
            {
                if (Idoneo || DEBUG)
                {
                    if(PunteggioAtteso.HrDiscrezionali != 0)
                        return (float)PunteggioOsservato.HrDiscrezionali / PunteggioAtteso.HrDiscrezionali * PMAX_HrDiscrezionali;
                }
                
                return 0;
            }
        }


        public float PunteggioHrComportamentali
        {
            get
            {
                if (Idoneo || DEBUG)
                {
                    if (PunteggioAtteso.HrComportamentali != 0)
                        return (float)PunteggioOsservato.HrComportamentali / PunteggioAtteso.HrComportamentali * PMAX_HrComportamentali;
                }

                return 0;
            }
        }


        public float PunteggioComportamentali
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

        public float PunteggioTecnStrategic
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

        public float PunteggioTecnCompetitiveAdv
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

        public float PunteggioTotale
        {
            get { return PunteggioHrDiscrezionali + PunteggioHrComportamentali + PunteggioComportamentali + PunteggioTecnStrategic + PunteggioTecnCompetitiveAdv; }
        }


        //Il punteggio per le Tecniche Foundational non rientrano nel totale
        public float PunteggioTecnFoundational
        {
            get
            {
                if (PunteggioAtteso.TecnFoundational != 0)
                    return (float)PunteggioOsservato.TecnFoundational / PunteggioAtteso.TecnFoundational;

                return 0;
            }
        }

        #endregion //PERCENTUALI

        #endregion //PROPRIETA' CALCOLATE AUTOMATICAMENTE
    }
}
