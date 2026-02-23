using RBA_PI.Application.DTOs.FacturasRemitos;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Application.Mappings.Facturas
{
    internal class FacturaRemitoPrepararMapper
    {
        internal static PrepararFacturaRemitoResultDto ToResult(int idComprobante, VwComprobantesArca cabecera, ComprobantesArca importes, DateOnly fechaContable, string? proveedor)
        {
            {
                FacturaRemitoPrepararDto dto = FacturaPrepararBaseMapper.ToBaseData<FacturaRemitoPrepararDto>(idComprobante, cabecera, importes, fechaContable, proveedor);

                return new PrepararFacturaRemitoResultDto
                {
                    PuedePreparar = true,
                    Data = dto
                };
            }
        }
    }
}
