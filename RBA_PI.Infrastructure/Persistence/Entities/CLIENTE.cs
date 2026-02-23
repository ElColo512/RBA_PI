namespace RBA_PI.Infrastructure.Persistence.Entities;

public partial class CLIENTE
{
    public decimal COD_CLIENTE { get; set; }

    public string? RAZON_SOCIAL { get; set; }

    public string? NOTAS { get; set; }

    public string? DOMICILIO { get; set; }

    public string? TELEFONO { get; set; }

    public string? LOCALIDAD { get; set; }

    public DateOnly? FECHA_INHA { get; set; }

    public string? CUIT { get; set; }
}
