namespace RBA_PI.Web.ViewModels
{
    public class FacturasIndexViewModel
    {
        public FacturasConceptosViewModel Filtros { get; init; } = new();
        public FacturasIndexConfigViewModel Config { get; init; } = new();
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
