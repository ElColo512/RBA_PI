using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IArticulosRemitoRepository
    {
        Task<List<ArticulosRemito>> GetAllAsync();
    }
}
