using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface ISectorRepository
    {
        Task<List<Sector>> GetAllAsync();
    }
}
