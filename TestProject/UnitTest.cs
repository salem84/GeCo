using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeCo;
using GeCo.DAL;
using System.Configuration;
using GeCo.Model;

namespace GeCo.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void InsertAnagrafica()
        {
            var value = ConfigurationManager.ConnectionStrings["PavimentalDb"];
            PavimentalContext context = new PavimentalContext();

            context.Dipendenti.Add(new Dipendente()
            {
                Nome = "ciccio",
                Cognome = "panza"
            });

            context.SaveChanges();
        }

        [TestMethod]
        public void GetValue()
        {
            PavimentalContext context = new PavimentalContext();
            var tutti = context.Competenze.ToList();
            foreach (var p in tutti)
            {
                string titolo = p.Titolo;
            }
        }


        [TestMethod]
        public void InsertCompetenza()
        {
            
            PavimentalContext context = new PavimentalContext("PavimentalDb");

            context.Competenze.Add(new Competenza()
            {
                Titolo="Italiano",
                Descrizione="descrizione1",
                Peso=1
            });
            context.Competenze.Add(new Competenza()
            {
                Titolo = "Inglese",
                Descrizione = "descrizione2",
                Peso=1
            });
            context.Competenze.Add(new Competenza()
            {
                Titolo = "Francese",
                Descrizione = "descrizione3",
                Peso=1
            });

            context.SaveChanges();
        }

        [TestMethod]
        public void InsertAltro()
        {
            PavimentalContext context = new PavimentalContext();
            //context.Aree.Add(new Area() { Nome = "Area1" });
            context.LivelliConoscenza.Add(new LivelloConoscenza()
            {
                Titolo="Basso",
                Valore=1
            });
            context.LivelliConoscenza.Add(new LivelloConoscenza()
            {
                Titolo = "Alto",
                Valore = 2
            });

            context.SaveChanges();
        }

        [TestMethod]
        public void InsertCapo()
        {
            PavimentalContext context = new PavimentalContext();

            ConoscenzaCompetenza conoscenza1 = new ConoscenzaCompetenza();
            conoscenza1.Competenza = context.Competenze.Single(c=> c.Titolo=="Italiano");
            conoscenza1.LivelloConoscenza = context.LivelliConoscenza.Single(lc => lc.Titolo=="Alto");

            ConoscenzaCompetenza conoscenza2 = new ConoscenzaCompetenza();
            conoscenza2.Competenza = context.Competenze.Single(c=> c.Titolo=="Inglese");
            conoscenza2.LivelloConoscenza = context.LivelliConoscenza.Single(lc => lc.Titolo=="Alto");

            List<ConoscenzaCompetenza> conoscenze = new List<ConoscenzaCompetenza>();
            conoscenze.Add(conoscenza1);
            conoscenze.Add(conoscenza2);
            context.Ruoli.Add(new Ruolo()
            {
                //Area = new Area() { Id = 1 },
                Titolo = "Capo",
                //Conoscenze = conoscenze
            });

        }

        [TestMethod]
        public void Reset()
        {
            PavimentalContext context = new PavimentalContext();
            context.Database.Delete();
            context.Database.Create();

        }

        [TestMethod]
        public void UpdateDipendente()
        {
            Dipendente dip = new Dipendente();
            dip.Id = 1;
            dip.Nome = "pidasdadad";
            dip.DataNascita = DateTime.Now;



            using (PavimentalContext context = new PavimentalContext())
            {
                context.Dipendenti.Attach(dip);
                context.Entry<Dipendente>(dip).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Prova2()
        {

        }
    }
}
