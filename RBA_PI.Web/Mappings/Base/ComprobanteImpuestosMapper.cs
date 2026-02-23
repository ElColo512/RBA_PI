using RBA_PI.Application.DTOs;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.Base
{
    internal static class ComprobanteImpuestosMapper
    {
        internal static ComprobanteImpuestosViewModel ToViewModel(ComprobantePrepararDto dto)
        {
            return new ComprobanteImpuestosViewModel
            {
                Items =
                [
                    new() { Tipo = TipoIva.Iva21,  Neto = dto.Neto21,   Iva = dto.Iva21 },
                    new() { Tipo = TipoIva.Iva27,  Neto = dto.Neto27,   Iva = dto.Iva27 },
                    new() { Tipo = TipoIva.Iva105, Neto = dto.Neto10_5, Iva = dto.Iva10_5 },
                    new() { Tipo = TipoIva.Iva25,  Neto = dto.Neto2_5,  Iva = dto.Iva2_5 },
                    new() { Tipo = TipoIva.Iva5,   Neto = dto.Neto5,    Iva = dto.Iva5 }
                ]
            };
        }
    }
}
