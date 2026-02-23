using Microsoft.Data.SqlClient;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.Services.Interfaces;

namespace RBA_PI.Infrastructure.Data.ConnectionStrings
{
    public class ConnectionStringFactory(IClienteContext clienteContext) : IConnectionStringFactory
    {
        private readonly IClienteContext _clienteContext = clienteContext;

        public async Task<string> CreateAsync()
        {
            ConfigurationDto config = await _clienteContext.GetConfiguracionAsync() ?? throw new InvalidOperationException("Configuración no encontrada.");

            if (string.IsNullOrWhiteSpace(config.CcServidor) || string.IsNullOrWhiteSpace(config.CcBaseDatos))
                throw new InvalidOperationException("Configuración de conexión incompleta.");

            SqlConnectionStringBuilder builder = new()
            {
                DataSource = config.CcServidor,
                InitialCatalog = config.CcBaseDatos,
                IntegratedSecurity = string.IsNullOrWhiteSpace(config.CcUsuario),
                TrustServerCertificate = true
            };

            if (!builder.IntegratedSecurity)
            {
                builder.UserID = config.CcUsuario;
                builder.Password = config.CcPassword;
            }

            return builder.ConnectionString;
        }
    }
}
