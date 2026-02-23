using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBA_PI.Web.ViewModels
{
    public class ComprobantesOtrosTributosViewModel
    {
        public int? ImputarAId { get; set; }
        public int? ImputarAId2 { get; set; }
        public decimal? OtrosTributos { get; set; }
        public bool PuedeImputarOtrosTributos { get; set; }
        public decimal? ImputarValor { get; set; }
        public decimal? ImputarValor2 { get; set; }
        public decimal? ImputarNoGravado { get; set; }
        public IEnumerable<SelectListItem> ImputarAOptions { get; set; } = [];
        public IEnumerable<SelectListItem> ImputarAOptions2 { get; set; } = [];
    }
}
