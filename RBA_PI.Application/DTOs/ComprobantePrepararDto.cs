namespace RBA_PI.Application.DTOs
{
    public abstract class ComprobantePrepararDto
    {
        public int IdComprobante { get; init; }
        public string Proveedor { get; init; } = string.Empty;
        public string Cuit { get; init; } = string.Empty;
        public string RazonSocial { get; init; } = string.Empty;
        public string Comprobante { get; init; } = string.Empty;
        public string Cae { get; init; } = string.Empty;
        public DateOnly Fecha { get; init; }
        public DateOnly FechaContable { get; init; }
        public string Observaciones { get; set; } = string.Empty;
        public decimal? Neto21 { get; init; }
        public decimal? Iva21 { get; init; }
        public decimal? Neto27 { get; init; }
        public decimal? Iva27 { get; init; }
        public decimal? Neto10_5 { get; init; }
        public decimal? Iva10_5 { get; init; }
        public decimal? Neto5 { get; init; }
        public decimal? Iva5 { get; init; }
        public decimal? Neto2_5 { get; init; }
        public decimal? Iva2_5 { get; init; }
        public decimal? OtrosTributos { get; set; }
        public decimal? ImputarValor { get; set; }
        public decimal? ImputarNoGravado { get; set; }
        public decimal? ImporteGravado { get; set; }
        public decimal? ImporteNoGravado { get; set; }
        public decimal? ImporteExento { get; set; }
        public decimal? ImporteTotal { get; set; }
    }
}
