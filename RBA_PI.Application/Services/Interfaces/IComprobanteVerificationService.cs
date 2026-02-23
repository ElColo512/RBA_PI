using RBA_PI.Application.DTOs.VerificarComprobante;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IComprobanteVerificationService
    {
        Task<VerificacionComprobanteResultDto> VerificarAsync(string cuit, string nroFactura, string tipoComprobante);
    }
}
