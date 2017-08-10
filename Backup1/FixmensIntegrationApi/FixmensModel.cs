namespace FixmensIntegrationApi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FixmensModel : DbContext
    {
        public FixmensModel()
            : base("name=FixmensModel")
        {
        }

        public virtual DbSet<PRUEBA> PRUEBAs { get; set; }
        public virtual DbSet<REPARACIONE> REPARACIONES { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRUEBA>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.HORAINGRESO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.HORATERMINADO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.PROMETIDO_HORA)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.CHIP_NS)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.ORDEN_CLI)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.CLI_NOMBRE_EVENTUAL)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.TEL_EVENTUAL)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.CLIENTE_GARANTIA)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.PIN)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.CAMPOLIBRE1)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.CAMPOLIBRE2)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.GARANTIA_FACTURA)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.REMITO_ENTREGADO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.REMITO_INGRESO)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.BARCODE)
                .IsUnicode(false);

            modelBuilder.Entity<REPARACIONE>()
                .Property(e => e.ARTICULO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<database_firewall_rules>()
                .Property(e => e.start_ip_address)
                .IsUnicode(false);

            modelBuilder.Entity<database_firewall_rules>()
                .Property(e => e.end_ip_address)
                .IsUnicode(false);
        }
    }
}
