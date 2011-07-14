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
        ///  
        /// Utilizzato da MOD_RUOLI
        /// </summary>
        /// <param name="ruoloA"></param>
        /// <param name="ruoloB"></param>
        public static bool ValidaA_MinoriUgualiDi_B(Ruolo ruoloA, Ruolo ruoloB)
        {
            var filiali = GetFiliali();

            foreach (var filiale in filiali)
            {
                bool? res = null;
                
                //se mi torna null vuol dire che non sono della stessa filiale, e vado avanti con gli altri controlli
                if (filiale.ContainsKey(ruoloA.Titolo) && filiale.ContainsKey(ruoloB.Titolo))
                {
                    int vA = filiale[ruoloA.Titolo];
                    int vB = filiale[ruoloB.Titolo];

                    if (vA <= vB)
                        res = true;
                    else
                        res = false;
                }
                                
                if (res.HasValue)
                    return res.Value;

            }
            

            return true;
        }

        public static bool ValidaA_MaggioriUgualiDi_B(Ruolo ruoloA, Ruolo ruoloB)
        {
            return ValidaA_MinoriUgualiDi_B(ruoloB, ruoloA);
        }

        /// Utilizzato da MOD_DIPENDENTI (quando cerco un posto per l'assistente, non è valido il ruolo Capo)
        /*public static bool ScartaB_MinoriDi_A(Ruolo ruoloA, Ruolo ruoloB)
        {
            var filiali = GetFiliali();

            foreach (var filiale in filiali)
            {
                bool? res = null;

                //se mi torna null vuol dire che non sono della stessa filiale, e vado avanti con gli altri controlli
                if (filiale.ContainsKey(ruoloA.Titolo) && filiale.ContainsKey(ruoloB.Titolo))
                {
                    int vA = filiale[ruoloA.Titolo];
                    int vB = filiale[ruoloB.Titolo];

                    if (vB < vA)
                        res = false;
                    else
                        res = true;
                }

                if (res.HasValue)
                    return res.Value;

            }


            return true;
        }*/

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

        
    }
}
