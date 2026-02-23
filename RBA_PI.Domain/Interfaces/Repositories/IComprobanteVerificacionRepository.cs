using RBA_PI.Domain.ReadModels;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IComprobanteVerificacionRepository
    {
        Task<ComprobanteVerificacionData?> VerificarFacturaAsync(string cuitProveedor, string nroFactura, string tipoComprobante);
    }
}
