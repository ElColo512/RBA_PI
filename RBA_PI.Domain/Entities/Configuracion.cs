namespace RBA_PI.Domain.Entities
{
    public class Configuracion
    {
        public string CcServidor { get; set; } = string.Empty;
        public string CcBaseDatos { get; set; } = string.Empty;
        public string CcUsuario { get; set; } = string.Empty;
        public string CcPassword { get; set; } = string.Empty;
        public bool? UsaAuxiliar { get; set; }
        public bool? UsaSector { get; set; }
    }
}
