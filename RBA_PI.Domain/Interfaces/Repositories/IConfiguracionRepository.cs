using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IConfiguracionRepository
    {
        Task<Configuracion> ObtenerFlagsAsync(decimal? codCliente);
    }
}
