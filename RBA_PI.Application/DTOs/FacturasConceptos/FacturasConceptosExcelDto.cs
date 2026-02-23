namespace RBA_PI.Application.DTOs.FacturasConceptos
{
    public class FacturasConceptosExcelDto
    {
        public decimal Id { get; set; }
        public DateOnly Fecha { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string Comprobante { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public decimal? ImporteNeto { get; set; }
        public decimal? ImporteTotal { get; set; }
        public decimal? ImpExento { get; set; }
        public decimal? OtrosTributos { get; set; }
        public decimal? Iva { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal? Interno { get; set; }
    }
}
