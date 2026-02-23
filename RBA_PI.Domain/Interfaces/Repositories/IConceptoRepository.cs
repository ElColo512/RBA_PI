using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IConceptoRepository
    {
        Task<List<Concepto>> GetAllAsync();
    }
}
