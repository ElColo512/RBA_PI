
using RBA_PI.Application.Commands;
using RBA_PI.Application.Common.Results;
using RBA_PI.Application.DTOs.FacturasConceptos;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IFacturasConceptosService
    {
        Task<IReadOnlyList<FacturasConceptosExcelDto>> ObtenerParaExcelAsync(ComprobanteFiltroDto filtro);
        Task<PrepararFacturaConceptoResultDto> PrepararAsync(int idComprobante);
        Task<ComprobanteProcesarResult> ProcesarAsync(ProcesarComprobanteConceptoCommand command);
        byte[] ExportarFacturasConceptos(IEnumerable<FacturasConceptosExcelDto> datos);
    }
}
