using RBA_PI.Application.DTOs;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Application.Mappings
{
    public abstract class FiltroRangoFechaMapper
    {
        public static void MapBase(FiltroRangoFechaDto dto, FiltroRangoFecha destino)
        {
            destino.CodCliente = dto.CodCliente!.Value;
            destino.Desde = dto.Desde;
            destino.Hasta = dto.Hasta;
        }
    }
}
