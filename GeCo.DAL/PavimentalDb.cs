using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GeCo.DAL
{
    public class PavimentalDb : DbContext
    {
        public DbSet<Dipendente> Dipendenti { get; set; }
        public DbSet<FiguraProfessionale> FigureProfessionali { get; set; }
        public DbSet<Competenza> Competenze { get; set; }
        public DbSet<Area> Aree { get; set; }
        public DbSet<LivelloConoscenza> LivelliConoscenza { get; set; }
        public DbSet<TipologiaCompetenza> TipologieCompetenze { get; set; }
        public DbSet<ConoscenzaCompetenza> ConoscenzaCompetenze { get; set; }
        public DbSet<Parametro> Parametri { get; set; }
        

        public PavimentalDb()
            : base()
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public PavimentalDb(string nameConn)
            : base(nameConn)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Anagrafica>()
                .Map(m => m.ToTable("Anagrafica"));

            modelBuilder.Entity<Area>()
                .Map(m => m.ToTable("Area"));

            modelBuilder.Entity<Competenza>()
                .Map(m => m.ToTable("Competenze"));

            modelBuilder.Entity<ConoscenzaCompetenza>()
                .Map(m => m.ToTable("ConoscenzeCompetenza"));
                //.HasRequired(m => m.Dotato).WithRequiredPrincipal().WillCascadeOnDelete();
            

            modelBuilder.Entity<FiguraProfessionale>()
                .Map(m =>m.ToTable("FigureProfessionali"));
            

            modelBuilder.Entity<LivelloConoscenza>()
                .Map(m => m.ToTable("LivelliConoscenza"));


            modelBuilder.Entity<TipologiaCompetenza>()
                .Map(m => m.ToTable("TipologieCompetenza"));*/
            

            modelBuilder.Entity<Parametro>()
                .HasKey(m => m.Nome)
                .Map(m => m.ToTable("Parametri"));
        }
    }
}
