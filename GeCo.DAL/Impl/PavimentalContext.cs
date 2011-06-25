using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using GeCo.Model;

namespace GeCo.DAL
{
    public class PavimentalContext : DbContext
    {
        public DbSet<Dipendente> Dipendenti { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<Competenza> Competenze { get; set; }
        //public DbSet<Area> Aree { get; set; }
        public DbSet<LivelloConoscenza> LivelliConoscenza { get; set; }
        public DbSet<TipologiaCompetenza> TipologieCompetenze { get; set; }
        public DbSet<ConoscenzaCompetenza> ConoscenzaCompetenze { get; set; }
        public DbSet<Parametro> Parametri { get; set; }
        

        //I Proxy devono essere abilitati in questo modo vengono caricati automaticamente le entità correlate
        public PavimentalContext()
            : base()
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        //Ricontrollare, non funziona bene
        public PavimentalContext(string nameConn)
            : base(nameConn)
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Anagrafica>()
                .Map(m => m.ToTable("Anagrafica"));*/

            /*modelBuilder.Entity<Area>()
                .Map(m => m.ToTable("Area"));*/
            modelBuilder.ComplexType<Area>();

            modelBuilder.Entity<Competenza>()
                .Map(m => m.ToTable("Competenze"));

            modelBuilder.Entity<ConoscenzaCompetenza>()
                .Map(m => m.ToTable("ConoscenzeCompetenza"));
                //.HasRequired(m => m.Dotato).WithRequiredPrincipal().WillCascadeOnDelete();
            

            /*modelBuilder.Entity<Ruolo>()
                .Map(m =>m.ToTable("Ruoli"));*/
            

            modelBuilder.Entity<LivelloConoscenza>()
                .Map(m => m.ToTable("LivelliConoscenza"));


            modelBuilder.Entity<TipologiaCompetenza>()
                .Map(m => m.ToTable("TipologieCompetenza"));
            

            modelBuilder.Entity<Parametro>()
                .HasKey(m => m.Nome)
                .Map(m => m.ToTable("Parametri"));
        }
    }
}
