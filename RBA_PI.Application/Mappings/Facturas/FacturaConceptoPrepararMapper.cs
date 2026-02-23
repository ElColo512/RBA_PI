using RBA_PI.Application.DTOs.FacturasConceptos;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Application.Mappings.Facturas
{
    internal class FacturaConceptoPrepararMapper
    {
        internal static PrepararFacturaConceptoResultDto ToResult(int idComprobante, VwComprobantesArca cabecera, ComprobantesArca importes, DateOnly fechaContable, string? proveedor)
        {
            FacturaConceptoPrepararDto dto = FacturaPrepararBaseMapper.ToBaseData<FacturaConceptoPrepararDto>(idComprobante, cabecera, importes, fechaContable, proveedor);

            return new PrepararFacturaConceptoResultDto
            {
                PuedePreparar = true,
                Data = dto
            };
        }
    }
}
