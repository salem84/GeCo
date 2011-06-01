using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeCo.DAL;
using System.Collections;
using System.Data.Entity;
using GeCo.DAL.Dati;
using GeCo.BLL.AlgoritmoRicerca;

namespace TestProject
{
    [TestClass]
    public class AlgoritmoTest
    {
        [TestMethod]
        public void Ricerca()
        {
            try
            {

                using (PavimentalDb context = new PavimentalDb())
                {
                    //context.Database.Delete();
                    //context.Database.Create();
                    //InitializeDB.InitalizeAll();
                    var figure = context.FigureProfessionali.Include(f => f.Conoscenze.Select(c => c.Competenza))
                                    .Include(f => f.Conoscenze.Select(c => c.LivelloConoscenza))
                                    .AsEnumerable();
                    Dipendente dip = CreaDipendenteTest(context);

                    List<object> risultati = new List<object>();

                    foreach (var figura in figure)
                    {
                        bool idoneo = false;
                        double percentuale = 0;
                        int punteggioOsservato = 0;// dip.IndiceTotale;
                        int punteggioAtteso = 0;// figura.IndiceTotale;
                        double punteggioAttesoMinimo = punteggioAtteso * 70.0 / 100;
                        if (punteggioOsservato > punteggioAttesoMinimo)
                        {
                            idoneo = true;
                            percentuale = (double)punteggioOsservato / punteggioAtteso * 100.0;
                        }


                        risultati.Add(new { NomeFigura = figura.Titolo, PAM = punteggioAttesoMinimo, Idoneo = idoneo, Percentuale = percentuale });
                    }

                }
            }
            catch (Exception fault)
            {
                string ex = fault.Message;
            }
        }

        private Dipendente CreaDipendenteTest(PavimentalDb context)
        {

            ConoscenzaCompetenza conoscenza1 = new ConoscenzaCompetenza();
            conoscenza1.Competenza = context.Competenze.Single(c => c.Titolo == "Normative Tecniche");
            conoscenza1.LivelloConoscenza = context.LivelliConoscenza.Single(lc => lc.Titolo == "Medio");

            ConoscenzaCompetenza conoscenza2 = new ConoscenzaCompetenza();
            conoscenza2.Competenza = context.Competenze.Single(c => c.Titolo == "Contrattualistica Fornitori");
            conoscenza2.LivelloConoscenza = context.LivelliConoscenza.Single(lc => lc.Titolo == "Medio");

            List<ConoscenzaCompetenza> conoscenze = new List<ConoscenzaCompetenza>();
            conoscenze.Add(conoscenza1);
            conoscenze.Add(conoscenza2);

            Dipendente dip = new Dipendente();
            dip.Conoscenze = conoscenze;

            return dip;

        }

        
        
    }
}
