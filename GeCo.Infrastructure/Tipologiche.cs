using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.Infrastructure
{
    public class Tipologiche
    {
        /// <summary>
        /// Macrogruppi
        /// </summary>
        public class Macrogruppi
        {
            public const string MG_HR_DISCREZIONALE = "HR DISCREZIONALE";
            public const string MG_HR_COMPORTAMENTALE = "HR COMPORTAMENTALE";
            public const string MG_COMPORTAMENTALE = "COMPORTAMENTALE";
            public const string MG_TECNICO = "TECNICO";

            public static List<string> GetAll()
            {
                return new List<string>()  
                { 
                    Tipologiche.Macrogruppi.MG_HR_DISCREZIONALE, 
                    Tipologiche.Macrogruppi.MG_HR_COMPORTAMENTALE,
                    Tipologiche.Macrogruppi.MG_COMPORTAMENTALE, 
                    Tipologiche.Macrogruppi.MG_TECNICO
                };
            }
        }

        /// <summary>
        /// TIPOLOGIE COMPETENZE
        /// </summary>
        public class TipologiaCompetenza
        {
            //Tecniche 
            public const string T_FOUNDATIONAL = "Foundational";
            public const string T_STRATEGIC_SUPPORT = "Strategic Support";
            public const string T_COMPETITIVE_ADVANTAGE = "Competitive Advantage";

            //Comportamentali
            public const string C_MANAGERIALI = "Manageriali";
            public const string C_RELAZIONALI = "Relazionali";
            public const string C_COGNITIVE = "Cognitive";
            public const string C_REALIZZATIVE = "Realizzative";

            //HR Discrezionali
            public const string HR_DISCREZIONALI = "HR Discrezionali";

            //HR Comportamentali
            public const string HR_C_MANAGERIALI = "Manageriali";
            public const string HR_C_RELAZIONALI = "Relazionali";
            public const string HR_C_COGNITIVE = "Cognitive";
            public const string HR_C_REALIZZATIVE = "Realizzative";
        }
        

        /// <summary>
        /// LIVELLO CONOSCENZA
        /// </summary>
        public class Livello
        {
            public const string INSUFFICIENTE = "Insufficiente"; //0
            public const string SUFFICIENTE = "Sufficiente"; //1
            public const string DISCRETO = "Discreto"; //2
            public const string BUONO = "Buono"; //3
            public const string OTTIMO = "Ottimo"; //4
        }

        /// <summary>
        /// NOME PARAMETRI
        /// </summary>
        public class Parametro
        {
            public const string PMAX_HR_DISCREZIONALI = "PunteggioMaxHrDiscrezionali";
            public const string PMAX_HR_COMPORTAMENTALI = "PunteggioMaxHrComportamentali";
            public const string PMAX_COMPORTAMENTALI = "PunteggioMaxComportamentali";
            public const string PMAX_TECN_STRATEGIC = "PunteggioMaxTecnStrategic";
            public const string PMAX_TECN_COMPETITIVE = "PunteggioMaxTecnCompetitive";
            public const string PERCENTUALE_SOGLIA_FOUNDATIONAL = "PercentualeSogliaFoundational";
        }
        
    }
}
