using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBA_PI.Web.ViewModels
{
    public class FacturaRemitoPrepararViewModel : IComprobantePrepararBaseVm
    {
        public long IdSession { get; set; }
        public IEnumerable<SelectListItem> Depositos { get; set; } = [];
        public string? DepositoId { get; set; }
        public List<FacturaRemitoItemViewModel> Items { get; set; } = [];
        public IEnumerable<SelectListItem> Articulos { get; set; } = [];
        public ComprobanteHeaderViewModel Header { get; set; } = new();
        public ComprobanteImpuestosViewModel Impuestos { get; set; } = new();
        public ComprobantesOtrosTributosViewModel OtrosTributos { get; set; } = new();
        public ComprobanteTotalesViewModel Totales { get; set; } = new();
        public IEnumerable<SelectListItem> Sectores { get; set; } = [];
        public IEnumerable<SelectListItem> Auxiliares { get; set; } = [];
        public IEnumerable<SelectListItem> ImputarAOptions { get; set; } = [];
        public string? Observaciones { get; set; }
        public int SectorId { get; set; }
        public int? AuxiliarId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public bool PuedeImputarOtrosTributos { get; set; }
        public bool UsaSector { get; set; }
        public bool UsaAuxiliar { get; set; }
    }
}
