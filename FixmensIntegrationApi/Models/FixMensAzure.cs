namespace FixmensIntegrationApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FixMensAzure : DbContext
    {
        public FixMensAzure()
            : base("name=FixMensAzure")
        {
        }

        public virtual DbSet<REPARACIONESVIEW> REPARACIONESVIEW { get; set; }

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
        }
    }
}
