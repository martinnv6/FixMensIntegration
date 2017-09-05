namespace FixmensCMD.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FixMensSQLModel : DbContext
    {
        public FixMensSQLModel()
            : base("name=FixMensModel")
        {
        }

        public virtual DbSet<REPARACIONESVIEW> REPARACIONESVIEWs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.EQUIPO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.NOMBRES)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.FALLA)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.INFORMETALLER)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.DETALLE)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.PRESPUPUESTO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.TECNICO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.CELULAR)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.ENTREGADO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.NS)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.AVISADO)
                .IsUnicode(false);
            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.ESTADOEQUIPO)
                .IsUnicode(false);
            modelBuilder.Entity<REPARACIONESVIEW>()
                .Property(e => e.COLOR)
                .IsUnicode(false);
        }
    }
}
