using RBA_PI.Application.DTOs.Estados;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;

namespace RBA_PI.Application.Services.Implementations
{
    public class EstadosComprobantesService(IEstadosComprobantesRepository repository) : IEstadosComprobantesService
    {
        private readonly IEstadosComprobantesRepository _repository = repository;

        public async Task<List<EstadoComprobanteDto>> ObtenerSelectAsync()
        {
            List<EstadosComprobante> estados = await _repository.GetAllAsync();

            List<EstadoComprobanteDto> resultado = [.. estados
                .Select(e => new EstadoComprobanteDto
                {
                    Id = e.CodEstadoComprobante,
                    Descripcion = e.DescEstadoComprobante ?? string.Empty
                })];

            return resultado;
        }
    }
}
