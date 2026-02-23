using Microsoft.EntityFrameworkCore;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Mappings;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class EstadosComprobantesRepository(AppDbContext context) : IEstadosComprobantesRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<EstadosComprobante>> GetAllAsync()
        {
            List<ESTADOS_COMPROBANTE> estadosDb = await _context.ESTADOS_COMPROBANTEs.AsNoTracking().OrderBy(e => e.COD_ESTADO_COMPROBANTE).ToListAsync();

            return [.. estadosDb.Select(EstadosComprobanteMapper.ToDomain)];
        }
    }
}
