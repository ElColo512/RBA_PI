using RBA_PI.Domain.Entities;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Mappings
{
    internal static class ComprobantesArcaMapper
    {
        internal static ComprobanteParaCambioEstado ToVerification(VW_COMPROBANTES_ARCA db)
        {
            return new ComprobanteParaCambioEstado
            {
                Cuit = db.CUIT ?? string.Empty,
                NroFactura = db.COMPROBANTE ?? string.Empty
            };
        }

        internal static ComprobantesArca ToDomain(COMPROBANTES_ARCA db)
        {
            return new ComprobantesArca
            {
                IdComprobanteAfip = db.ID_COMPROBANTE_AFIP,
                Fecha = db.FECHA,
                Tipo = db.TIPO,
                PtoVenta = db.PTO_VENTA,
                NroComprobante = db.NRO_COMPROBANTE,
                CodAutorizacion = db.COD_AUTORIZACION ?? string.Empty,
                TipoDoc = db.TIPO_DOC ?? string.Empty,
                NroDocumento = db.NRO_DOCUMENTO ?? string.Empty,
                RazonSocial = db.RAZON_SOCIAL ?? string.Empty,
                TipoCambio = db.TIPO_CAMBIO,
                Moneda = db.MONEDA ?? string.Empty,
                ImpNeto = db.IMP_NETO,
                ImpNoGravado = db.IMP_NO_GRAVADO,
                ImpExento = db.IMP_EXENTO,
                OtrosTributos = db.OTROS_TRIBUTOS,
                Iva = db.IVA,
                ImpTotal = db.IMP_TOTAL,
                CodCliente = db.COD_CLIENTE,
                CodEstadoComprobante = db.COD_ESTADO_COMPROBANTE,
                NetoGravado0 = db.NETO_GRAVADO_0,
                Iva25 = db.IVA_25,
                Neto25 = db.NETO_25,
                Iva5 = db.IVA_5,
                Neto5 = db.NETO_5,
                Iva105 = db.IVA_105,
                Neto105 = db.NETO_105,
                Iva21 = db.IVA_21,
                Neto21 = db.NETO_21,
                Iva27 = db.IVA_27,
                Neto27 = db.NETO_27,
                NcompInC = db.NCOMP_IN_C
            };
        }

        internal static COMPROBANTES_ARCA ToPersistence(ComprobantesArca domain)
        {
            return new COMPROBANTES_ARCA
            {
                FECHA = domain.Fecha,
                TIPO = domain.Tipo,
                PTO_VENTA = domain.PtoVenta,
                NRO_COMPROBANTE = domain.NroComprobante,
                COD_AUTORIZACION = domain.CodAutorizacion,
                TIPO_DOC = domain.TipoDoc,
                NRO_DOCUMENTO = domain.NroDocumento,
                RAZON_SOCIAL = domain.RazonSocial,
                TIPO_CAMBIO = domain.TipoCambio,
                MONEDA = domain.Moneda,
                NETO_GRAVADO_0 = domain.NetoGravado0,
                IVA_25 = domain.Iva25,
                NETO_25 = domain.Neto25,
                IVA_5 = domain.Iva5,
                NETO_5 = domain.Neto5,
                IVA_105 = domain.Iva105,
                NETO_105 = domain.Neto105,
                IVA_21 = domain.Iva21,
                NETO_21 = domain.Neto21,
                IVA_27 = domain.Iva27,
                NETO_27 = domain.Neto27,
                IMP_NETO = domain.ImpNeto,
                IMP_NO_GRAVADO = domain.ImpNoGravado,
                IMP_EXENTO = domain.ImpExento,
                OTROS_TRIBUTOS = domain.OtrosTributos,
                IVA = domain.Iva,
                IMP_TOTAL = domain.ImpTotal,
                COD_CLIENTE = domain.CodCliente
            };
        }
    }
}
