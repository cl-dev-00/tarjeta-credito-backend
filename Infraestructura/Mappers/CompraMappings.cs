using AutoMapper;
using Dominio.Constants;
using Dominio.Dtos;
using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Mappers
{
    public class CompraMappings: Profile
    {
        public CompraMappings() {
            CreateMap<CrearCompraRequestDto, Compra>()
                .ForMember(c => c.CreadoPor, ccr => ccr.MapFrom(ccr => ccr.NombreCliente))
                .ForMember(c => c.Estado, ccr => ccr.MapFrom(ccr => Estados.Activo))
                .ForMember(c => c.FechaHoraCreacion, ccr => ccr.MapFrom(c => DateTime.Now));

            CreateMap<Compra, CrearCompraResponseDto>()
                .ForMember(c => c.NombreCliente, ccr => ccr.MapFrom(ccr => ccr.CreadoPor))
                .ForMember(c => c.Fecha, ccr => ccr.MapFrom(ccr => ccr.Fecha.ToString("yyyy-MM-dd")));

            CreateMap<Compra, CompraDto>()
                .ForMember(c => c.Fecha, ccr => ccr.MapFrom(ccr => ccr.Fecha.ToString("yyyy-MM-dd")));
        }
    }
}
