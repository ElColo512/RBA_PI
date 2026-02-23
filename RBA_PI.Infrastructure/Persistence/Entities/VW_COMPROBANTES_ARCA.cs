namespace RBA_PI.Infrastructure.Persistence.Entities;

public partial class VW_COMPROBANTES_ARCA
{
    public string? CUIT { get; set; }

    public string? RAZON_SOCIAL { get; set; }

    public DateOnly FECHA { get; set; }

    public string? FECHA_A_MOSTRAR { get; set; }

    public string? COMPROBANTE { get; set; }

    public string? n_COMPROBANTE { get; set; }

    public string? MONEDA { get; set; }

    public decimal COD_ESTADO_COMPROBANTE { get; set; }

    public string? Estado { get; set; }

    public decimal? COD_CLIENTE { get; set; }

    public decimal ID_COMPROBANTE_AFIP { get; set; }

    public decimal? IVA { get; set; }

    public decimal? IVA_TOTAL { get; set; }

    public decimal? NCOMP_IN_C { get; set; }

    public decimal? IMP_NETO { get; set; }

    public decimal? IMP_NO_GRAVADO { get; set; }

    public decimal? IMP_EXENTO { get; set; }

    public decimal? OTROS_TRIBUTOS { get; set; }

    public decimal? IMP_TOTAL { get; set; }
}
