using RBA_PI.Application.DTOs.AnalisisDatos;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Application.Mappings
{
    internal static class AnalisisDatosFiltroMapper
    {
        internal static AnalisisDatosFiltro ToDomain(AnalisisDatosFiltroDto dto)
        {
            AnalisisDatosFiltro filtro = new();
            FiltroRangoFechaMapper.MapBase(dto, filtro);
            return filtro;
        }
    }
}
