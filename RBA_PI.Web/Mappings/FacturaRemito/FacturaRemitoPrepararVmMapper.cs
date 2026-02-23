using RBA_PI.Application.DTOs.FacturasRemitos;
using RBA_PI.Web.Mappings.Base;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.FacturaRemito
{
    internal class FacturaRemitoPrepararVmMapper
    {
        internal static FacturaRemitoPrepararViewModel ToViewModel(FacturaRemitoPrepararDto dto)
        {
            return new FacturaRemitoPrepararViewModel
            {
                Header = ComprobanteHeaderMapper.ToViewModel(dto),
                Impuestos = ComprobanteImpuestosMapper.ToViewModel(dto),
                OtrosTributos = ComprobanteOtrosTributosMapper.ToViewModel(dto),
                Totales = ComprobanteTotalesMapper.ToViewModel(dto),
                Observaciones = dto.Observaciones
            };
        }
    }
}
