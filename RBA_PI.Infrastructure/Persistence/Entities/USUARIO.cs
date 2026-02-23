namespace RBA_PI.Infrastructure.Persistence.Entities;

public partial class USUARIO
{
    public decimal ID_USUARIO { get; set; }

    public decimal COD_CLIENTE { get; set; }

    public string NOMBRE_COMPLETO { get; set; } = null!;

    public string MAIL { get; set; } = null!;

    public string PASSWORD { get; set; } = null!;

    public bool? ADMINISTRADOR { get; set; }
}
