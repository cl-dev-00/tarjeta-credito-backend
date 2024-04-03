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
    public class PagoTypeConfiguration : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.ToTable("PAGO");

            builder.HasKey(e => new { e.IdPago, e.IdTarjeta, e.IdCliente });

            builder.Property(e => e.IdPago)
                .HasColumnName("idPago")
                .HasColumnType("INT")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdTarjeta)
                .HasColumnName("idTarjeta")
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(e => e.IdCliente)
                .HasColumnName("idCliente")
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(e => e.Monto)
                .HasColumnName("monto")
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder.Property(e => e.Fecha)
                .HasColumnName("fecha")
                .HasColumnType("DATE")
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

            builder.HasOne(e => e.Tarjeta)
                .WithMany(t => t.Pagos)
                .HasForeignKey(e => new { e.IdCliente, e.IdTarjeta })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
