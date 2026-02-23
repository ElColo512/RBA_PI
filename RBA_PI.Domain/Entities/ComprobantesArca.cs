namespace RBA_PI.Domain.Entities
{
    public class ComprobantesArca
    {
        public decimal IdComprobanteAfip { get; set; }
        public DateOnly Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal PtoVenta { get; set; }
        public decimal NroComprobante { get; set; }
        public string CodAutorizacion { get; set; } = string.Empty;
        public string TipoDoc { get; set; } = string.Empty;
        public string NroDocumento { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public decimal? TipoCambio { get; set; }
        public string Moneda { get; set; } = string.Empty;
        public decimal? ImpNeto { get; set; }
        public decimal? ImpNoGravado { get; set; }
        public decimal? ImpExento { get; set; }
        public decimal? OtrosTributos { get; set; }
        public decimal? Iva { get; set; }
        public decimal? ImpTotal { get; set; }
        public decimal? CodCliente { get; set; }
        public decimal CodEstadoComprobante { get; set; }
        public decimal? NetoGravado0 { get; set; }
        public decimal? Iva25 { get; set; }
        public decimal? Neto25 { get; set; }
        public decimal? Iva5 { get; set; }
        public decimal? Neto5 { get; set; }
        public decimal? Iva105 { get; set; }
        public decimal? Neto105 { get; set; }
        public decimal? Iva21 { get; set; }
        public decimal? Neto21 { get; set; }
        public decimal? Iva27 { get; set; }
        public decimal? Neto27 { get; set; }
        public decimal? NcompInC { get; set; }
    }
}
