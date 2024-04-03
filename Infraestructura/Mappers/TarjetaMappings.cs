using AutoMapper;
using Dominio.Dtos;
using Dominio.Entities;
using Infraestructura.MapperActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Mappers
{
    public class TarjetaMappings : Profile
    {

        public TarjetaMappings()
        {

            CreateMap<Tarjeta, TarjetaResponseDto>()
                .ForMember(trd => trd.NombreTitular, t => t.MapFrom(t => $"{t.Cliente.NombreCliente} {t.Cliente.ApellidoCliente}"))
                .ForMember(trd => trd.TipoTarjeta, t => t.MapFrom(t => t.TipoTarjeta.NombreTipoTarjeta));

            CreateMap<Tarjeta, TarjetaDetalleResponseDto>()
                .ForMember(tdrd => tdrd.NombreTitular, t => t.MapFrom(t => $"{t.Cliente.NombreCliente} {t.Cliente.ApellidoCliente}"))
                .ForMember(tdrd => tdrd.LimiteCredito, t => t.MapFrom(t => t.TipoTarjeta.LimiteCredito))
                .ForMember(tdrd => tdrd.MontoTotalAPagar, t => t.MapFrom(t => t.SaldoActual))
                .AfterMap<TarjetaMapperAction>();
        }
    }
}
