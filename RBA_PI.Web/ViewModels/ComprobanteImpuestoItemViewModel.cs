using RBA_PI.Application.Common.Extensions;
using RBA_PI.Domain.Common.Enums;

namespace RBA_PI.Web.ViewModels
{
    public class ComprobanteImpuestoItemViewModel
    {
        public TipoIva Tipo { get; set; }
        public decimal? Neto { get; set; }
        public decimal? Iva { get; set; }
        public string LabelNeto => $"Neto {Tipo.Porcentaje()}";
        public string LabelIva => $"IVA {Tipo.Porcentaje()}";
    }
}
