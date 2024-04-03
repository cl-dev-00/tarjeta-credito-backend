using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.EntityConfiguration
{
    public class ClienteTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTE");

            builder.HasKey(e => e.IdCliente);

            builder.Property(e => e.IdCliente)
                .HasColumnName("idCliente")
                .HasColumnType("INT")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoCliente)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.NombreCliente)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ApellidoCliente)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(200);

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
