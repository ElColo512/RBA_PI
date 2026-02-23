namespace RBA_PI.Infrastructure.Persistence.Entities;

public partial class CONFIGURACION
{
    public decimal COD_CLIENTE { get; set; }

    public string? COD_IVA_21 { get; set; }

    public string? COD_IVA_105 { get; set; }

    public string? COD_IVA_27 { get; set; }

    public string? COND_COMPRA { get; set; }

    public decimal? ID_ASIENTO_MODELO_CP { get; set; }

    public string? CC_SERVIDOR { get; set; }

    public string? CC_BASE_DATOS { get; set; }

    public string? CC_USUARIO { get; set; }

    public string? CC_PASSWORD { get; set; }

    public bool? USA_AUXILIAR { get; set; }

    public bool? USA_SECTOR { get; set; }
}
