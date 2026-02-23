using RBA_PI.Application.DTOs;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.Base
{
    internal class ComprobanteHeaderMapper
    {
        internal static ComprobanteHeaderViewModel ToViewModel(ComprobantePrepararDto dto)
        {
            return new ComprobanteHeaderViewModel
            {
                IdComprobante = dto.IdComprobante,
                Proveedor = dto.Proveedor,
                Cuit = dto.Cuit,
                Cae = dto.Cae,
                RazonSocial = dto.RazonSocial,
                Comprobante = dto.Comprobante,
                FechaEmision = dto.Fecha,
                FechaContable = dto.FechaContable
            };
        }
    }
}
