
using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IComprobanteArcaRepository
    {
        Task<bool> ExistsAsync(string codAutorizacion, decimal? codCliente);
        Task AddRangeAsync(List<ComprobantesArca> entities);
        Task<ComprobantesArca?> ObtenerParaPrepararAsync(decimal id);
        Task<ComprobanteParaCambioEstado?> ObtenerDatosParaVerificacionAsync(decimal id);
        Task CambiarEstadoAsync(decimal idComprobante, int nuevoEstado, decimal? interno);
    }
}
