namespace RBA_PI.Application.DTOs.ImportacionDatos
{
    public class ComprobanteArcaImportDto
    {
        public DateOnly Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int PtoVenta { get; set; }
        public int NroComprobante { get; set; }
        public string CodAutorizacion { get; set; } = string.Empty;
        public string TipoDoc { get; set; } = string.Empty;
        public string NroDocumento { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public decimal TipoCambio { get; set; }
        public string Moneda { get; set; } = string.Empty;
        public decimal Neto_0 { get; set; }
        public decimal Iva_25 { get; set; }
        public decimal Neto_25 { get; set; }
        public decimal Iva_5 { get; set; }
        public decimal Neto_5 { get; set; }
        public decimal Iva_105 { get; set; }
        public decimal Neto_105 { get; set; }
        public decimal Iva_21 { get; set; }
        public decimal Neto_21 { get; set; }
        public decimal Iva_27 { get; set; }
        public decimal Neto_27 { get; set; }
        public decimal ImpNeto { get; set; }
        public decimal ImpNoGravado { get; set; }
        public decimal ImpExento { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal Iva { get; set; }
        public decimal ImpTotal { get; set; }
        public decimal? CodCliente { get; set; }
    }
}
