using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBA_PI.Web.ViewModels
{
    public class FacturasConceptosViewModel
    {
        public DateOnly Desde { get; set; }
        public DateOnly Hasta { get; set; }
        public int? EstadoId { get; set; }
        public IEnumerable<SelectListItem> Estados { get; set; } = [];
    }
}
