using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Application.Mappings
{
    public static class ConfiguracionMapper
    {
        public static Configuracion ToDomain(ConfigurationDto dto)
        => new()
        {
            CcServidor = dto.CcServidor,
            CcBaseDatos = dto.CcBaseDatos
        };
    }
}
