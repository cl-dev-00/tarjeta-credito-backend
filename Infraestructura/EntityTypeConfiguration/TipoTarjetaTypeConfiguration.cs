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
    public class TipoTarjetaTypeConfiguration : IEntityTypeConfiguration<TipoTarjeta>
    {
        public void Configure(EntityTypeBuilder<TipoTarjeta> builder)
        {
            builder.ToTable("TIPO_TARJETA");

            builder.HasKey(e => e.IdTipoTarjeta);

            builder.Property(e => e.IdTipoTarjeta)
                .HasColumnName("idTipoTarjeta")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.NombreTipoTarjeta)
                .HasColumnName("nombreTipoTarjeta")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.DescripcionTipoTarjeta)
                .HasColumnName("descripcionTipoTarjeta")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.LimiteCredito)
                .HasColumnName("limiteCredito")
                .HasColumnType("DECIMAL")
                .IsRequired();

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
        }
    }
}
