using RBA_PI.Application.DTOs.AnalisisDatos;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.Mappings;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Domain.Queries;

namespace RBA_PI.Application.Services.Implementations
{
    public class AnalisisDatosService(IAnalisisDatosRepository analisisDatosRepository, IClienteContext clienteContext) : IAnalisisDatosService
    {
        private readonly IAnalisisDatosRepository _repository = analisisDatosRepository;
        private readonly IClienteContext _clienteContext = clienteContext;

        public async Task<List<AnalisisFacturaDto>> ObtenerAsync(AnalisisDatosFiltroDto filtroDto)
        {
            AnalisisDatosFiltro filtro = AnalisisDatosFiltroMapper.ToDomain(filtroDto);
            ConfigurationDto cfgDto = await _clienteContext.GetConfiguracionAsync();
            Configuracion cfg = ConfiguracionMapper.ToDomain(cfgDto);
            List<VwComprobantesArca> facturas = await _repository.GetFacturasAsync(filtro, cfg);

            return [.. facturas.Select(static f => new AnalisisFacturaDto
            {
                Id = f.IdComprobanteAfip,
                Fecha = f.FechaAMostrar,
                RazonSocial = f.RazonSocial,
                Cuit = f.Cuit,
                Comprobante = f.Comprobante,
                Moneda = f.Moneda,
                ImporteTotal = f.ImpTotal,
                Estado = f.Estado,
                Interno = f.NcompInC
            })];
        }
    }
}
