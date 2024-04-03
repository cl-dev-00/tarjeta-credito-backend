using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.EntityTypeConfiguration
{
    public class ConfiguracionDetalleTypeConfiguration : IEntityTypeConfiguration<ConfiguracionDetalle>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionDetalle> builder)
        {
            builder.ToTable("CONFIGURACION_DETALLE");

            builder.HasKey(e => new { e.IdConfiguracionDetalle, e.IdConfiguracion });

            builder.Property(e => e.IdConfiguracionDetalle)
                .HasColumnName("idConfiguracionDetalle")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.IdConfiguracion)
                .HasColumnName("idConfiguracion")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.NombreConfiguracionDetalle)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Valor1)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Valor2)
                .HasColumnType("VARCHAR")
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.CreadoPor)
                .HasColumnName("creadoPor")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.FechaHoraCreacion)
                .HasColumnName("fechaHoraCreacion")
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(e => e.ModificadoPor)
                .HasColumnName("modificadoPor")
                .HasColumnType("VARCHAR")
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(e => e.FechaHoraModificacion)
                .HasColumnName("fechaHoraModificacion")
                .HasColumnType("DATETIME")
                .IsRequired(false);

            builder.HasOne(e => e.Configuracion)
                .WithMany(c => c.ConfiguracionDetalles)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
