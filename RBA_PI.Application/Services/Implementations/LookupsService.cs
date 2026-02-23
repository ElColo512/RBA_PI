using RBA_PI.Application.DTOs;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;

namespace RBA_PI.Application.Services.Implementations
{
    public class LookupsService(ISectorRepository sectorRepository, IConceptoRepository conceptoRepository, IAuxiliarRepository auxiliarRepository,
        IDepositoRepository depositoRepository, IImputarARepository imputarARepository, IArticulosRemitoRepository articulosRemitoRepository) : ILookupsService
    {
        private readonly ISectorRepository _sectorRepository = sectorRepository;
        private readonly IConceptoRepository _conceptoRepository = conceptoRepository;
        private readonly IAuxiliarRepository _auxiliarRepository = auxiliarRepository;
        private readonly IDepositoRepository _depositoRepository = depositoRepository;
        private readonly IImputarARepository _imputarARepository = imputarARepository;
        private readonly IArticulosRemitoRepository _articulosRemitoRepository = articulosRemitoRepository;

        public async Task<IReadOnlyList<SelectItemDto>> ObtenerArticulosRemitoAsync()
        {
            List<ArticulosRemito> articulosRemito = await _articulosRemitoRepository.GetAllAsync();

            return [.. articulosRemito
                .Select(a => new SelectItemDto
                {
                    Value = a.IdSta11.ToString(),
                    Text = $"{a.Descripcion} ({a.Codigo})"
                })];
        }

        public async Task<IReadOnlyList<SelectItemDto>> ObtenerAuxiliaresAsync()
        {
            List<Auxiliar> auxiliares = await _auxiliarRepository.GetAllAsync();

            return [.. auxiliares
                .Select(a => new SelectItemDto
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.Descripcion} ({a.Codigo})"
                })];
        }

        public async Task<IReadOnlyList<SelectItemDto>> ObtenerConceptosAsync()
        {
            List<Concepto> conceptos = await _conceptoRepository.GetAllAsync();

            return [.. conceptos
                .Select(c => new SelectItemDto
                {
                    Value = c.Codigo,
                    Text = $"{c.Descripcion} ({c.Codigo})"
                }).OrderBy(c => c.Text)];
        }

        public async Task<IReadOnlyList<SelectItemDto>> ObtenerDepositosAsync()
        {
            List<Deposito> depositos = await _depositoRepository.GetAllAsync();

            return [.. depositos
                .Select(d => new SelectItemDto
                {
                    Value = d.Id.ToString(),
                    Text = d.Descripcion.ToString()
                })];
        }

        public async Task<IReadOnlyList<SelectItemDto>> ObtenerImputarAAsync(TipoImpuesto tipo)
        {
            List<ImputarAItem> imputarItems = await _imputarARepository.GetImpuestoAsync(tipo);

            return [.. imputarItems
                .Select(s => new SelectItemDto
                {
                    Value = s.Id.ToString(),
                    Text = s.Descripcion.ToString()
                }).OrderBy(s => s.Text)];
        }

        public async Task<IReadOnlyList<SelectItemDto>> ObtenerSectoresAsync()
        {
            List<Sector> sectores = await _sectorRepository.GetAllAsync();

            return [.. sectores
             .Select(s => new SelectItemDto
             {
                 Value = s.IdCpa51.ToString(),
                 Text = $"{s.Descripcion} ({s.Codigo})"
             }).OrderBy(s => s.Text)];
        }
    }
}
