using RBA_PI.Application.Commands;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Application.Interfaces.Repositories
{
    public interface IComprobanteProcesadorRepository
    {
        Task<ResultadoProcesarComprobante> ProcesarConceptoAsync(ProcesarComprobanteConceptoCommand command);
        Task<ResultadoProcesarComprobante> ProcesarRemitoAsync(ProcesarComprobanteRemitoCommand command);
    }
}
