using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBA_PI.Web.ViewModels
{
    public interface IComprobantePrepararBaseVm
    {
        ComprobanteHeaderViewModel Header { get; set; }
        ComprobanteImpuestosViewModel Impuestos { get; set; }
        ComprobantesOtrosTributosViewModel OtrosTributos { get; set; }
        ComprobanteTotalesViewModel Totales { get; set; }
        IEnumerable<SelectListItem> Sectores { get; set; }
        IEnumerable<SelectListItem> Auxiliares { get; set; }
        IEnumerable<SelectListItem> ImputarAOptions { get; set; }
        string? Observaciones { get; set; }
        int SectorId { get; set; }
        int? AuxiliarId { get; set; }
        string NombreCompleto { get; set; }
        bool PuedeImputarOtrosTributos { get; set; }
        bool UsaSector { get; set; }
        bool UsaAuxiliar { get; set; }
    }
}
