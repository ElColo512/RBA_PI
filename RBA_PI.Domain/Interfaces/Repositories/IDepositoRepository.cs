using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IDepositoRepository
    {
        Task<List<Deposito>> GetAllAsync();
    }
}
