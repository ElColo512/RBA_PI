using RBA_PI.Domain.Entities;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Mappings
{
    internal class ConfiguracionMapper
    {
        internal static Configuracion ToDomain(CONFIGURACION db)
        {
            return new Configuracion
            {
                CcServidor = db.CC_SERVIDOR ?? string.Empty,
                CcBaseDatos = db.CC_BASE_DATOS ?? string.Empty,
                CcUsuario = db.CC_USUARIO ?? string.Empty,
                CcPassword = db.CC_PASSWORD ?? string.Empty,
                UsaAuxiliar = db.USA_AUXILIAR,
                UsaSector = db.USA_SECTOR
            };
        }
    }
}
