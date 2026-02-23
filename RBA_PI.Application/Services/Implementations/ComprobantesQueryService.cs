using RBA_PI.Application.DTOs.FacturasConceptos;
using RBA_PI.Application.Mappings.Facturas;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Application.Services.Implementations
{
    public class ComprobantesQueryService(IComprobantesRepository comprobantesRepository) : IComprobantesQueryService
    {
        private readonly IComprobantesRepository _comprobantesRepository = comprobantesRepository;

        public async Task<IReadOnlyList<FacturaComprobanteDto>> ObtenerAsync(ComprobanteFiltroDto filtroDto)
        {
            ComprobanteFiltro filtro = FacturaConceptoFiltroMapper.ToDomain(filtroDto);
            List<VwComprobantesArca> comprobantes = await _comprobantesRepository.GetAllAsync(filtro);

            return [.. comprobantes.Select(c => new FacturaComprobanteDto
            {
                Id = c.IdComprobanteAfip,
                Fecha = c.Fecha,
                RazonSocial = c.RazonSocial,
                Cuit = c.Cuit,
                Comprobante = c.Comprobante,
                Moneda = c.Moneda,
                ImporteTotal = c.ImpTotal,
                Estado = c.Estado ?? string.Empty
            })];
        }
    }
}
