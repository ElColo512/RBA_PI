using RBA_PI.Application.DTOs.VerificarComprobante;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Domain.ReadModels;

namespace RBA_PI.Application.Services.Implementations
{
    public class ComprobanteVerificationService(IComprobanteVerificacionRepository comprobanteVerificacionRepository) : IComprobanteVerificationService
    {
        private readonly IComprobanteVerificacionRepository _comprobanteVerificacionRepository = comprobanteVerificacionRepository;

        public async Task<VerificacionComprobanteResultDto> VerificarAsync(string cuit, string nroFactura, string tipoComprobante)
        {

            ComprobanteVerificacionData? data = await _comprobanteVerificacionRepository.VerificarFacturaAsync(cuit, nroFactura, tipoComprobante);

            if (data is null)
            {
                return new()
                {
                    Resultado = ResultadoVerificacionComprobante.SinInformacion,
                    Mensaje = "No se encontró información para la factura."
                };
            }

            return data.Resultado switch
            {
                "OK" => new VerificacionComprobanteResultDto
                {
                    Resultado = ResultadoVerificacionComprobante.PuedeCargarse,
                    Mensaje = "Factura lista para cargar."
                },

                "Error_proveedor" => new VerificacionComprobanteResultDto
                {
                    Resultado = ResultadoVerificacionComprobante.ProveedorInexistente,
                    Mensaje = "El proveedor no existe en Tango. Debe registrarlo antes de procesar la factura."
                },

                "Factura_Existe" => new VerificacionComprobanteResultDto
                {
                    Resultado = ResultadoVerificacionComprobante.FacturaYaRegistrada,
                    NumeroInterno = data.NCompIn,
                    Mensaje = $"La factura ya se encuentra registrada en Tango bajo el nro interno {data.NCompIn}."
                },

                _ => new VerificacionComprobanteResultDto
                {
                    Resultado = ResultadoVerificacionComprobante.Error,
                    Mensaje = "Ocurrió un error desconocido al verificar la factura."
                }
            };
        }
    }
}
