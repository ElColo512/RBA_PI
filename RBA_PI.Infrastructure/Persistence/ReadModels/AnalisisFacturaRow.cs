namespace RBA_PI.Infrastructure.Persistence.ReadModels
{
    public class AnalisisFacturaRow
    {
        public string RAZON_SOCIAL { get; set; } = string.Empty;
        public string FECHA_A_MOSTRAR { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string COMPROBANTE { get; set; } = string.Empty;
        public string MONEDA { get; set; } = string.Empty;
        public decimal IMP_TOTAL { get; set; }
        public string ESTADO { get; set; } = string.Empty;
        public decimal ID_COMPROBANTE_AFIP { get; set; }
        public decimal? INTERNO { get; set; }
    }
}
