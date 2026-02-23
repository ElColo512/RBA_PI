using RBA_PI.Application.Commands;
using RBA_PI.Application.Common.Results;
using RBA_PI.Application.DTOs.FacturasRemitos;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IFacturasRemitosService
    {
        Task<PrepararFacturaRemitoResultDto> PrepararAsync(int idComprobante);
        Task<ComprobanteProcesarResult> ProcesarAsync(ProcesarComprobanteRemitoCommand command);
    }
}
