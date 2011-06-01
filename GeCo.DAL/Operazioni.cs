using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.DAL
{
    public static class Operazioni
    {
        public static void SalvaDipendente(Dipendente Dipendente)
        {
            using (PavimentalDb context = new PavimentalDb())
            {
                Dipendente dip = new Dipendente();
                dip.Cognome = Dipendente.Cognome;
                dip.Nome = Dipendente.Nome;
                dip.DataNascita = Dipendente.DataNascita;
                
                
                dip.Conoscenze = new List<ConoscenzaCompetenza>();

                foreach (var c in Dipendente.Conoscenze)
                {
                    ConoscenzaCompetenza conoscenza = new ConoscenzaCompetenza();

                    if (c.LivelloConoscenza.Id != 0)
                    {
                        int id = c.LivelloConoscenza.Id;
                        conoscenza.LivelloConoscenza = context.LivelliConoscenza.Single(lcq => lcq.Id == id);
                    }

                    if (c.Competenza.Id != 0)
                    {
                        int id = c.Competenza.Id;
                        conoscenza.Competenza = context.Competenze.Single(cq => cq.Id == id);
                    }
                    dip.Conoscenze.Add(conoscenza);
                }
                

                //context.Anagrafica.Attach(dip);
                //context.Entry<Anagrafica>(dip).State = System.Data.EntityState.Modified;

                /*context.Anagrafica.Attach(dip);
                if (dip.Id == 0)
                    context.Entry<Anagrafica>(dip).State = System.Data.EntityState.Added;
                else
                    context.Entry<Anagrafica>(dip).State = System.Data.EntityState.Modified;*/

                //context.Anagrafica.Add(dip);
                //context.Entry<Anagrafica>(dip).State = System.Data.EntityState.Modified;

                context.Dipendenti.Attach(dip);
                context.Entry<Dipendente>(dip).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
