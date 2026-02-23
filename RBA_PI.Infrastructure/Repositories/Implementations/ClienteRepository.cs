using Microsoft.EntityFrameworkCore;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Mappings;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ClienteRepository(AppDbContext context) : IClienteRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Cliente?> ObtenerPorClienteAsync(decimal codCliente)
        {
            CLIENTE? clienteDb = await _context.CLIENTEs.AsNoTracking().SingleOrDefaultAsync(c => c.COD_CLIENTE == codCliente);

            return clienteDb == null ? null : ClienteMapper.ToDomain(clienteDb);
        }
    }
}
