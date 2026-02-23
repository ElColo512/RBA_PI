using RBA_PI.Application.DTOs;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.Base
{
    internal static class ComprobanteTotalesMapper
    {
        internal static ComprobanteTotalesViewModel ToViewModel(ComprobantePrepararDto dto)
        {
            return new ComprobanteTotalesViewModel
            {
                ImporteGravado = dto.ImporteGravado,
                ImporteNoGravado = dto.ImporteNoGravado,
                ImporteExento = dto.ImporteExento,
                ImporteTotal = dto.ImporteTotal
            };
        }
    }
}
