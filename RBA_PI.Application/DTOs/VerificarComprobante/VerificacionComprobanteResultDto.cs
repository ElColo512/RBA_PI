using RBA_PI.Domain.Common.Enums;

namespace RBA_PI.Application.DTOs.VerificarComprobante
{
    public class VerificacionComprobanteResultDto
    {
        public ResultadoVerificacionComprobante Resultado { get; init; }
        public string Mensaje { get; init; } = string.Empty;
        public decimal? NumeroInterno { get; init; }
    }
}
