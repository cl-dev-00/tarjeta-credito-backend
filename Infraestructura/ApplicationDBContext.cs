using Dominio.Entities;
using Infraestructura.EntityConfiguration;
using Infraestructura.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<TipoTarjeta> TipoTarjeta { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Configuracion> Configuracion { get; set; }
        public virtual DbSet<ConfiguracionDetalle> ConfiguracionDetalle { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoTarjetaTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TarjetaTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CompraTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PagoTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConfiguracionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConfiguracionDetalleTypeConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
