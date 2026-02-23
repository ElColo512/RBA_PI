using RBA_PI.Application.DTOs.FacturasConceptos;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IComprobantesQueryService
    {
        Task<IReadOnlyList<FacturaComprobanteDto>> ObtenerAsync(ComprobanteFiltroDto filtroDto);
    }
}
