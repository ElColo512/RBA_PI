namespace RBA_PI.Application.DTOs.ClienteContext
{
    public class ConfigurationDto
    {
        public string CcServidor { get; set; } = string.Empty;
        public string CcBaseDatos { get; set; } = string.Empty;
        public string CcUsuario { get; set; } = string.Empty;
        public string CcPassword { get; set; } = string.Empty;
        public bool? USA_AUXILIAR { get; set; }
        public bool? USA_SECTOR { get; set; }
    }
}
