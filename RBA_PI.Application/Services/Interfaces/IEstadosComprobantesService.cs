using RBA_PI.Application.DTOs.Estados;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IEstadosComprobantesService
    {
        Task<List<EstadoComprobanteDto>> ObtenerSelectAsync();
    }
}
