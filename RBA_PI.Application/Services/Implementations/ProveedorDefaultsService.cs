using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Interfaces.Repositories;

namespace RBA_PI.Application.Services.Implementations
{
    public class ProveedorDefaultsService(IProveedorRepository proveedorRepository) : IProveedorDefaultsService
    {
        private readonly IProveedorRepository _proveedorRepository = proveedorRepository;
        public async Task<string> ObtenerUltimoConceptoAsync(string codProveedor)
        {
            string? concepto = await _proveedorRepository.GetUltimoConceptoAsync(codProveedor);
            return string.IsNullOrWhiteSpace(concepto) ? "014" : concepto;
        }

        public async Task<string> ObtenerUltimoDepositoAsync(string codProveedor)
        {
            string? deposito = await _proveedorRepository.GetUltimoDepositoAsync(codProveedor);
            return string.IsNullOrWhiteSpace(deposito) ? "01" : deposito;
        }

        public async Task<int> ObtenerUltimoSectorAsync(string codProveedor)
        {
            int? sector = await _proveedorRepository.GetUltimoSectorAsync(codProveedor);
            return sector ?? 1;
        }
    }
}
