using RBA_PI.Application.DTOs;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Application.Mappings.Facturas
{
    internal class FacturaPrepararBaseMapper
    {
        internal static T ToBaseData<T>(int idComprobante, VwComprobantesArca cabecera, ComprobantesArca importes, DateOnly fechaContable, string? proveedor)
            where T : ComprobantePrepararDto, new()
        {
            return new T
            {
                IdComprobante = idComprobante,
                Cae = importes.CodAutorizacion,
                Proveedor = proveedor ?? string.Empty,
                Cuit = cabecera.Cuit,
                RazonSocial = cabecera.RazonSocial,
                Comprobante = cabecera.Comprobante,
                Fecha = cabecera.Fecha,
                FechaContable = fechaContable,
                Neto21 = importes.Neto21,
                Iva21 = importes.Iva21,
                Neto27 = importes.Neto27,
                Iva27 = importes.Iva27,
                Neto10_5 = importes.Neto105,
                Iva10_5 = importes.Iva105,
                Neto2_5 = importes.Neto25,
                Iva2_5 = importes.Iva25,
                Neto5 = importes.Neto5,
                Iva5 = importes.Iva5,
                ImporteGravado = importes.ImpNeto,
                ImporteNoGravado = importes.ImpNoGravado,
                ImporteExento = importes.ImpExento,
                OtrosTributos = importes.OtrosTributos,
                ImporteTotal = importes.ImpTotal
            };
        }
    }
}