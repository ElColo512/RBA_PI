using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Identity.Entities;
using System.Security.Claims;

namespace RBA_PI.Infrastructure.Services
{
    public class ClienteContext(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IClienteRepository clienteRepository,
        IConfiguracionRepository configuracionRepository) : IClienteContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IConfiguracionRepository _configuracionRepository = configuracionRepository;
        private UserDto? _user;
        private ClienteDto? _cliente;
        private ConfigurationDto? _configuracion;

        public async Task<UserDto?> GetUserAsync()
        {
            if (_user != null) return _user;

            ClaimsPrincipal? principal = _httpContextAccessor.HttpContext?.User;

            if (principal?.Identity?.IsAuthenticated != true) return null;

            ApplicationUser? user = await _userManager.GetUserAsync(principal);

            if (user == null) return null;

            _user = new UserDto
            {
                IdUsuario = user.IdUsuario,
                NombreCompleto = user.NombreCompleto!,
                CodCliente = user.CodCliente
            };

            return _user;
        }

        public async Task<ClienteDto?> GetClienteAsync()
        {
            if (_cliente != null) return _cliente;

            UserDto? user = await GetUserAsync();

            if (user?.CodCliente == null) return null;

            Cliente? cliente = await _clienteRepository.ObtenerPorClienteAsync(user.CodCliente.Value);

            if (cliente == null) return null;

            _cliente = new ClienteDto
            {
                RazonSocial = cliente.RazonSocial
            };

            return _cliente;
        }

        public async Task<ConfigurationDto> GetConfiguracionAsync()
        {
            if (_configuracion != null) return _configuracion;

            UserDto user = await GetUserRequiredAsync();

            if (user.CodCliente == null) throw new InvalidOperationException("Usuario sin cliente asociado");

            Configuracion config = await _configuracionRepository.ObtenerFlagsAsync(user.CodCliente) ?? throw new InvalidOperationException("Configuración no encontrada");

            _configuracion = new ConfigurationDto
            {
                CcServidor = config.CcServidor,
                CcBaseDatos = config.CcBaseDatos,
                CcUsuario = config.CcUsuario,
                CcPassword = config.CcPassword,
                USA_AUXILIAR = config.UsaAuxiliar,
                USA_SECTOR = config.UsaSector
            };

            return _configuracion;
        }

        public async Task<UserDto> GetUserRequiredAsync()
        {
            UserDto? user = await GetUserAsync();
            return user ?? throw new UnauthorizedAccessException("Usuario no autenticado");
        }

        public async Task<ClienteDto> GetClienteRequiredAsync()
        {
            ClienteDto? cliente = await GetClienteAsync();
            return cliente ?? throw new InvalidOperationException("Cliente no disponible");
        }
    }
}
