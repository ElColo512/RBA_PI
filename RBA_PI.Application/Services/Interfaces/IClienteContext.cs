using RBA_PI.Application.DTOs.ClienteContext;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IClienteContext
    {
        Task<UserDto?> GetUserAsync();
        Task<UserDto> GetUserRequiredAsync();
        Task<ClienteDto?> GetClienteAsync();
        Task<ClienteDto> GetClienteRequiredAsync();
        Task<ConfigurationDto> GetConfiguracionAsync();
    }
}
