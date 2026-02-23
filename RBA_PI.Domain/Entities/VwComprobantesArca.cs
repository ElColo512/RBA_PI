namespace RBA_PI.Domain.Entities
{
    public class VwComprobantesArca
    {
        public string Cuit { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public DateOnly Fecha { get; set; }
        public string FechaAMostrar { get; set; } = string.Empty;
        public string Comprobante { get; set; } = string.Empty;
        public string NroComprobante { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public decimal CodEstadoComprobante { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal? CodCliente { get; set; }
        public decimal IdComprobanteAfip { get; set; }
        public decimal? Iva { get; set; }
        public decimal? IvaTotal { get; set; }
        public decimal? NcompInC { get; set; }
        public decimal? ImpNeto { get; set; }
        public decimal? ImpNoGravado { get; set; }
        public decimal? ImpExento { get; set; }
        public decimal? OtrosTributos { get; set; }
        public decimal? ImpTotal { get; set; }
    }
}
