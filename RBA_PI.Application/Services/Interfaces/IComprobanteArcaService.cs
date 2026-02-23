
using RBA_PI.Application.DTOs.ImportacionDatos;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IComprobanteArcaService : IComprobanteEstadoService
    {
        Task<int> InsertarAsync(List<ComprobanteArcaImportDto> lista);
    }
}
