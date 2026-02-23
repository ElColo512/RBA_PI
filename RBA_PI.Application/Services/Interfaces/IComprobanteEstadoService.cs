using RBA_PI.Domain.Common.Enums;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IComprobanteEstadoService
    {
        Task CambiarEstadoAsync(int idComprobante, EstadoComprobante nuevoEstado);
    }
}
