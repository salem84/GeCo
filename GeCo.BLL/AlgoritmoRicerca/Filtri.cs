using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeCo.Model;
using GeCo.Infrastructure;

namespace GeCo.BLL.AlgoritmoRicerca
{
    public class Filtri
    {
        /// <summary>
        /// Il metodo controlla se il ruoloInEsame è un ruolo maggiore (più importante) di quello di riferimento.
        /// Es. 
        /// 1) Assistente Capo Cantiere Infrastrutture
        /// 2) Capo Cantiere Infrastrutture
        /// 3) Direttore di Cantiere Infrastrutture
        /// 
        /// Se sto ricercando un Capo Cantiere(ruoloA)[3], dovrò
        ///  * scartare i Direttori (ruoloB) [4]
        ///  * prendere Assistenti o Capi Cantiere (ruoloB) [2-3]
        ///  
        /// Se sto cercando un Direttore (ruoloDiRiferimento), dovrò
        ///  * scartare tutti
        /// </summary>
        /// <param name="ruoloA"></param>
        /// <param name="ruoloB"></param>
        public static bool ScartaB_MaggioriDi_A(Ruolo ruoloA, Ruolo ruoloB)
        {
            var filiali = GetFiliali();

            foreach (var filiale in filiali)
            {
                //se mi torna null vuol dire che non sono della stessa filiale, e vado avanti con gli altri controlli
                bool? res = FiltraPerFiliale(ruoloA, ruoloB, filiale);
                if (res.HasValue)
                    return res.Value;

            }
            

            return true;
        }

        private static Dictionary<string, int>[] GetFiliali()
        {
            var filiali = new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    {"Capo Cantiere Manutenzione", 3},
                    {"Direttore Cantiere Manutenzione", 4},
                },

                new Dictionary<string, int>()
                {
                    {"Capo Cantiere Infrastrutture", 3},
                    {"Direttore Cantiere Infrastrutture", 4},
                }
            };
            return filiali;
        }

        private static bool? FiltraPerFiliale(Ruolo ruoloDiRiferimento, Ruolo ruoloInEsame, Dictionary<string, int> filiale)
        {
            //Controllo che appartengano alla stessa filiale
            if (filiale.ContainsKey(ruoloDiRiferimento.Titolo) && filiale.ContainsKey(ruoloInEsame.Titolo))
            {
                int vRif = filiale[ruoloDiRiferimento.Titolo];
                int vEsame = filiale[ruoloInEsame.Titolo];

                if (vEsame > vRif)
                    return false;
                else
                    return true;
            }
            return null;
        }
    }
}
