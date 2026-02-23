namespace RBA_PI.Web.ViewModels
{
    public class ComprobanteHeaderViewModel
    {
        public int IdComprobante { get; set; }
        public string Cuit { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public string Cae { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string Comprobante { get; set; } = string.Empty;
        public DateOnly FechaEmision { get; set; }
        public DateOnly FechaContable { get; set; }
    }
}
