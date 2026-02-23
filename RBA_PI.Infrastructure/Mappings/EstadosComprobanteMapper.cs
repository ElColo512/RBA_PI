using RBA_PI.Domain.Entities;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Mappings
{
    internal class EstadosComprobanteMapper
    {
        internal static EstadosComprobante ToDomain(ESTADOS_COMPROBANTE db)
        {
            return new EstadosComprobante
            {
                CodEstadoComprobante = db.COD_ESTADO_COMPROBANTE,
                DescEstadoComprobante = db.DESC_ESTADO_COMPROBANTE ?? string.Empty
            };
        }
    }
}
