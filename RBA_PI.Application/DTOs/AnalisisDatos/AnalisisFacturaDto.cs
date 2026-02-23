namespace RBA_PI.Application.DTOs.AnalisisDatos
{
    public class AnalisisFacturaDto
    {
        public decimal Id { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string Comprobante { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public decimal? ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal? Interno { get; set; }
        public string ImporteFormateado => $"{Moneda?.Trim()} {ImporteTotal:N2}";
    }
}
