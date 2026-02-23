using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerPorClienteAsync(decimal codCliente);
    }
}
