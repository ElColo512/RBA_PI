using RBA_PI.Application.DTOs.FacturasConceptos;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Application.Mappings.Facturas
{
    public static class FacturaConceptoFiltroMapper
    {
        public static ComprobanteFiltro ToDomain(ComprobanteFiltroDto dto)
        {
            ComprobanteFiltro filtro = new();
            FiltroRangoFechaMapper.MapBase(dto, filtro);
            filtro.EstadoId = dto.EstadoId;
            return filtro;
        }
    }
}
