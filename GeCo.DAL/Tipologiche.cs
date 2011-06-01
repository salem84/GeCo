using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.DAL
{
    public class Tipologiche
    {
        //Macrogruppi
        public const string MG_HR = "HR";
        public const string MG_COMPORTAMENTALE = "COMPORTAMENTALE";
        public const string MG_TECNICO = "TECNICO";

        /// <summary>
        /// TIPOLOGIE COMPETENZE
        /// </summary>
        public class TipologiaCompetenza
        {
            //Tecniche 
            public const string FOUNDATIONAL = "Foundational";
            public const string STRATEGIC_SUPPORT = "Strategic Support";
            public const string COMPETITIVE_ADVANTAGE = "Competitive Advantage";

            //Comportamentali
            public const string MANAGERIALI = "Manageriali";
            public const string RELAZIONALI = "Relazionali";
            public const string COGNITIVE = "Cognitive";
            public const string REALIZZATIVE = "Realizzative";

            //HR
            public const string ASSESSMENT = "Assessment";
            public const string CONSIDERAZIONI_GESTIONALI = "Considerazioni Gestionali";
        }
        

        /// <summary>
        /// LIVELLO CONOSCENZA
        /// </summary>
        public class Livello
        {
            public const string INSUFFICIENTE = "Insufficiente";
            public const string SUFFICIENTE = "Sufficiente";
            public const string DISCRETO = "Discreto";
            public const string BUONO = "Buono";
            public const string OTTIMO = "Ottimo";
        }

        /// <summary>
        /// NOME PARAMETRI
        /// </summary>
        public class Parametro
        {
            public const string PMAX_HR = "PunteggioMaxHR";
            public const string PMAX_COMPORTAMENTALI = "PunteggioMaxComportamentali";
            public const string PMAX_TECN_STRATEGIC = "PunteggioMaxTecnStrategic";
            public const string PMAX_TECN_COMPETITIVE = "PunteggioMaxTecnCompetitive";
            public const string PERCENTUALE_SOGLIA_FOUNDATIONAL = "PercentualeSogliaFoundational";
            
        }
        
    }
}
