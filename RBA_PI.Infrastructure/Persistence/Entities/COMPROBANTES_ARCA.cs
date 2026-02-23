namespace RBA_PI.Infrastructure.Persistence.Entities;

public partial class COMPROBANTES_ARCA
{
    public decimal ID_COMPROBANTE_AFIP { get; set; }

    public DateOnly FECHA { get; set; }

    public string TIPO { get; set; } = null!;

    public decimal PTO_VENTA { get; set; }

    public decimal NRO_COMPROBANTE { get; set; }

    public string? COD_AUTORIZACION { get; set; }

    public string? TIPO_DOC { get; set; }

    public string? NRO_DOCUMENTO { get; set; }

    public string? RAZON_SOCIAL { get; set; }

    public decimal? TIPO_CAMBIO { get; set; }

    public string? MONEDA { get; set; }

    public decimal? IMP_NETO { get; set; }

    public decimal? IMP_NO_GRAVADO { get; set; }

    public decimal? IMP_EXENTO { get; set; }

    public decimal? OTROS_TRIBUTOS { get; set; }

    public decimal? IVA { get; set; }

    public decimal? IMP_TOTAL { get; set; }

    public decimal? COD_CLIENTE { get; set; }

    public decimal COD_ESTADO_COMPROBANTE { get; set; }

    public decimal? NETO_GRAVADO_0 { get; set; }

    public decimal? IVA_25 { get; set; }

    public decimal? NETO_25 { get; set; }

    public decimal? IVA_5 { get; set; }

    public decimal? NETO_5 { get; set; }

    public decimal? IVA_105 { get; set; }

    public decimal? NETO_105 { get; set; }

    public decimal? IVA_21 { get; set; }

    public decimal? NETO_21 { get; set; }

    public decimal? IVA_27 { get; set; }

    public decimal? NETO_27 { get; set; }

    public decimal? NCOMP_IN_C { get; set; }
}
