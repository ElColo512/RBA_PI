using RBA_PI.Application.DTOs;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.Base
{
    internal static class ComprobanteOtrosTributosMapper
    {
        internal static ComprobantesOtrosTributosViewModel ToViewModel(ComprobantePrepararDto dto)
        {
            return new ComprobantesOtrosTributosViewModel
            {
                OtrosTributos = dto.OtrosTributos
            };
        }
    }
}
