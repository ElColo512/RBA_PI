namespace RBA_PI.Application.DTOs.FacturasConceptos
{
    public class FacturaComprobanteDto
    {
        public decimal Id { get; set; }
        public DateOnly Fecha { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string Comprobante { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public decimal? ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string ImporteFormateado => $"{Moneda?.Trim()} {ImporteTotal:N2}";
    }
}
