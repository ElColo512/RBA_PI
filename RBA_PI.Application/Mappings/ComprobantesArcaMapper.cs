using RBA_PI.Application.DTOs.ImportacionDatos;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Application.Mappings
{
    public class ComprobantesArcaMapper
    {
        public static ComprobantesArca ToDomain(ComprobanteArcaImportDto dto)
        {
            decimal diferencia = dto.ImpTotal - (dto.ImpNeto + dto.Iva + dto.ImpNoGravado + dto.OtrosTributos);

            decimal otrosTributos = 0;
            decimal total = 0;

            if (diferencia > 0)
            {
                otrosTributos = diferencia;
            }
            else
            {
                total = diferencia;
            }

            return new ComprobantesArca
            {
                Fecha = dto.Fecha,
                Tipo = dto.Tipo,
                PtoVenta = dto.PtoVenta,
                NroComprobante = dto.NroComprobante,
                CodAutorizacion = dto.CodAutorizacion,
                TipoDoc = dto.TipoDoc,
                NroDocumento = dto.NroDocumento,
                RazonSocial = dto.RazonSocial,
                TipoCambio = dto.TipoCambio,
                Moneda = dto.Moneda,
                NetoGravado0 = dto.Neto_0,
                Iva25 = dto.Iva_25,
                Neto25 = dto.Neto_25,
                Iva5 = dto.Iva_5,
                Neto5 = dto.Neto_5,
                Iva105 = dto.Iva_105,
                Neto105 = dto.Neto_105,
                Iva21 = dto.Iva_21,
                Neto21 = dto.Neto_21,
                Iva27 = dto.Iva_27,
                Neto27 = dto.Neto_27,
                ImpNeto = dto.ImpNeto,
                ImpNoGravado = dto.ImpNoGravado,
                ImpExento = dto.ImpExento,
                OtrosTributos = dto.OtrosTributos + otrosTributos,
                Iva = dto.Iva,
                ImpTotal = dto.ImpTotal - total,
                CodCliente = dto.CodCliente,
                CodEstadoComprobante = (decimal)EstadoComprobante.Pendiente
            };
        }
    }
}
