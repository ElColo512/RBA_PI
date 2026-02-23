using RBA_PI.Application.DTOs.FacturasConceptos;
using RBA_PI.Web.Mappings.Base;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.FacturaConcepto
{
    public static class FacturaConceptoPrepararVmMapper
    {
        public static FacturaConceptoPrepararViewModel ToViewModel(FacturaConceptoPrepararDto dto)
        {
            return new FacturaConceptoPrepararViewModel
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
