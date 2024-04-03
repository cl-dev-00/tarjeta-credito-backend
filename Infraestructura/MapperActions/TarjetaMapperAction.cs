using AutoMapper;
using Dominio.Dtos;
using Dominio.Entities;
using Dominio.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.MapperActions
{
    public class TarjetaMapperAction : IMappingAction<Tarjeta, TarjetaDetalleResponseDto>
    {
        private ICalculoTarjetaRepository _calculoTarjetaRepository;


        public TarjetaMapperAction(ICalculoTarjetaRepository calculoTarjetaRepository)
        {
            _calculoTarjetaRepository = calculoTarjetaRepository;
        }

        public void Process(Tarjeta source, TarjetaDetalleResponseDto destination, ResolutionContext context)
        {
            destination.InteresBonificable = _calculoTarjetaRepository.ObtenerInteresesBonificable(source.SaldoActual);
            destination.CuotaMinima = _calculoTarjetaRepository.ObtenerCuotaMinimaAPagar(source.SaldoActual);
            destination.MontoTotalContadoConInteres = _calculoTarjetaRepository.ObtenerMontoTotalContadoConIntereses(source.SaldoActual);
        }
    }
}
