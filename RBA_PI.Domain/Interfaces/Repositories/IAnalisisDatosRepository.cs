using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IAnalisisDatosRepository
    {
        Task<List<VwComprobantesArca>> GetFacturasAsync(AnalisisDatosFiltro filtro, Configuracion cfg);
    }
}
