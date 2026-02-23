using Microsoft.EntityFrameworkCore;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Mappings;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ConfiguracionRepository(AppDbContext context) : IConfiguracionRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Configuracion> ObtenerFlagsAsync(decimal? codCliente)
        {
            CONFIGURACION configDb = await _context.CONFIGURACIONs.AsNoTracking().SingleAsync(c => c.COD_CLIENTE == codCliente);

            return ConfiguracionMapper.ToDomain(configDb);
        }
    }
}
