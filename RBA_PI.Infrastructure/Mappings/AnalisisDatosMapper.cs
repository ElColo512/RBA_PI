using RBA_PI.Domain.Entities;
using RBA_PI.Infrastructure.Persistence.ReadModels;

namespace RBA_PI.Infrastructure.Mappings
{
    internal class AnalisisDatosMapper
    {
        internal static VwComprobantesArca ToDomain(AnalisisFacturaRow row) => new()
        {
            RazonSocial = row.RAZON_SOCIAL,
            FechaAMostrar = row.FECHA_A_MOSTRAR,
            Cuit = row.CUIT,
            Comprobante = row.COMPROBANTE,
            Moneda = row.MONEDA,
            ImpTotal = row.IMP_TOTAL,
            Estado = row.ESTADO,
            IdComprobanteAfip = row.ID_COMPROBANTE_AFIP,
            NcompInC = row.INTERNO
        };
    }
}
