namespace RBA_PI.Infrastructure.Persistence.Entities;

public partial class VW_RBA_ANALISIS_ARCA_TANGO22
{
    public DateOnly fecha { get; set; }

    public string? RAZON_SOCIAL { get; set; }

    public string? FECHA_A_MOSTRAR { get; set; }

    public string? CUIT { get; set; }

    public string? comprobante { get; set; }

    public string? moneda { get; set; }

    public decimal? IMP_TOTAL { get; set; }

    public decimal COD_ESTADO_COMPROBANTE { get; set; }

    public string? Estado { get; set; }

    public decimal? COD_CLIENTE { get; set; }

    public decimal ID_COMPROBANTE_AFIP { get; set; }

    public double? interno { get; set; }

    public decimal? IVA { get; set; }

    public decimal? IVA_TOTAL { get; set; }
}
