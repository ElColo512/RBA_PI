using RBA_PI.Domain.Entities;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Mappings
{
    internal class ClienteMapper
    {
        internal static Cliente ToDomain(CLIENTE db)
        {
            return new Cliente
            {
                RazonSocial = db.RAZON_SOCIAL ?? string.Empty,
                Cuit = db.CUIT ?? string.Empty
            };
        }
    }
}
