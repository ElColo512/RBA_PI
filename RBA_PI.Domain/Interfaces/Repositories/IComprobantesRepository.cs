using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IComprobantesRepository
    {
        Task<List<VwComprobantesArca>> GetAllAsync(ComprobanteFiltro filtro);
        Task<VwComprobantesArca?> GetHeaderAsync(decimal idComprobante);
    }
}
