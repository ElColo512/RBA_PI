using RBA_PI.Application.DTOs.AnalisisDatos;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IAnalisisDatosService
    {
        Task<List<AnalisisFacturaDto>> ObtenerAsync(AnalisisDatosFiltroDto filtro);
    }
}
