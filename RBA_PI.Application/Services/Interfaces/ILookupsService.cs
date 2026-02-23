using RBA_PI.Application.DTOs;
using RBA_PI.Domain.Common.Enums;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface ILookupsService
    {
        Task<IReadOnlyList<SelectItemDto>> ObtenerSectoresAsync();
        Task<IReadOnlyList<SelectItemDto>> ObtenerConceptosAsync();
        Task<IReadOnlyList<SelectItemDto>> ObtenerAuxiliaresAsync();
        Task<IReadOnlyList<SelectItemDto>> ObtenerDepositosAsync();
        Task<IReadOnlyList<SelectItemDto>> ObtenerImputarAAsync(TipoImpuesto tipo);
        Task<IReadOnlyList<SelectItemDto>> ObtenerArticulosRemitoAsync();
    }
}
