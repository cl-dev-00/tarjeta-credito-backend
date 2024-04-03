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
    public class TarjetaTypeConfiguration : IEntityTypeConfiguration<Tarjeta>
    {
        public void Configure(EntityTypeBuilder<Tarjeta> builder)
        {
            builder.ToTable("TARJETA");

            builder.HasKey(e => new { e.IdTarjeta, e.IdCliente });

            builder.Property(e => e.IdTarjeta)
                .HasColumnName("idTarjeta")
                .HasColumnType("INT")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdCliente)
                .HasColumnName("idCliente")
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(e => e.IdTipoTarjeta)
                .HasColumnName("idTipoTarjeta")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.NumeroTarjeta)
                .HasColumnName("numeroTarjeta")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(e => e.SaldoActual)
                .HasColumnName("saldoActual")
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder.Property(e => e.SaldoDisponible)
                .HasColumnName("saldoDisponible")
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

            builder.HasOne(e => e.Cliente)
                .WithMany(c => c.Tarjetas)
                .HasForeignKey(e => e.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.TipoTarjeta)
                .WithMany(c => c.Tarjetas)
                .HasForeignKey(e => e.IdTipoTarjeta)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
