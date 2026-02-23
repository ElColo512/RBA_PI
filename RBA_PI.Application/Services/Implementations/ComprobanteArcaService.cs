using RBA_PI.Application.DTOs.ImportacionDatos;
using RBA_PI.Application.DTOs.VerificarComprobante;
using RBA_PI.Application.Helpers;
using RBA_PI.Application.Mappings;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;

namespace RBA_PI.Application.Services.Implementations
{
    public class ComprobanteArcaService(IComprobanteArcaRepository repo, IComprobanteVerificationService comprobanteVerificationService) : IComprobanteArcaService
    {
        private readonly IComprobanteArcaRepository _repo = repo;
        private readonly IComprobanteVerificationService _ComprobanteVerificationService = comprobanteVerificationService;

        public async Task<int> InsertarAsync(List<ComprobanteArcaImportDto> lista)
        {
            List<ComprobantesArca> nuevos = [];

            foreach (var dto in lista)
            {
                if (!await _repo.ExistsAsync(dto.CodAutorizacion, dto.CodCliente))
                {
                    nuevos.Add(ComprobantesArcaMapper.ToDomain(dto));
                }
            }

            await _repo.AddRangeAsync(nuevos);
            return nuevos.Count;
        }

        public async Task CambiarEstadoAsync(int idComprobante, EstadoComprobante nuevoEstado)
        {
            ComprobanteParaCambioEstado? comprobante = await _repo.ObtenerDatosParaVerificacionAsync(idComprobante);

            string tipoComprobante = comprobante!.NroFactura is { Length: >= 3 } ? comprobante.NroFactura[..3] : string.Empty;
            string nroFacturaNormalizado = FacturaNormalizer.Normalizar(comprobante.NroFactura);

            VerificacionComprobanteResultDto verificacion = await _ComprobanteVerificationService.VerificarAsync(comprobante.Cuit, nroFacturaNormalizado, tipoComprobante);

            await _repo.CambiarEstadoAsync(idComprobante, (int)nuevoEstado, verificacion.NumeroInterno);
        }
    }
}
