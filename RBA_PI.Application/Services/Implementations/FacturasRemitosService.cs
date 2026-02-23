using RBA_PI.Application.Commands;
using RBA_PI.Application.Commands.Shared;
using RBA_PI.Application.Common.Results;
using RBA_PI.Application.DTOs.FacturasRemitos;
using RBA_PI.Application.DTOs.VerificarComprobante;
using RBA_PI.Application.Helpers;
using RBA_PI.Application.Interfaces.Repositories;
using RBA_PI.Application.Mappings.Facturas;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;

namespace RBA_PI.Application.Services.Implementations
{
    public class FacturasRemitosService(
        IFechaCierreRepository fechaCierreRepository, IProveedorRepository proveedorRepository, IComprobanteArcaRepository comprobanteArcaRepository,
        IComprobanteVerificationService comprobanteVerificationService, IComprobanteProcesadorRepository comprobanteRepository, IComprobantesRepository comprobantesRepository) : IFacturasRemitosService
    {
        private readonly IFechaCierreRepository _fechaCierreRepository = fechaCierreRepository;
        private readonly IComprobanteArcaRepository _comprobanteArcaRepository = comprobanteArcaRepository;
        private readonly IProveedorRepository _proveedorRepository = proveedorRepository;
        private readonly IComprobanteVerificationService _ComprobanteVerificationService = comprobanteVerificationService;
        private readonly IComprobanteProcesadorRepository _comprobanteRepository = comprobanteRepository;
        private readonly IComprobantesRepository _comprobantesRepository = comprobantesRepository;

        public async Task<PrepararFacturaRemitoResultDto> PrepararAsync(int idComprobante)
        {
            VwComprobantesArca? cabecera = await _comprobantesRepository.GetHeaderAsync(idComprobante);
            if (cabecera == null)
            {
                return new PrepararFacturaRemitoResultDto
                {
                    PuedePreparar = false,
                    Mensaje = "Factura no encontrada."
                };
            }
            string tipoComprobante = cabecera.Comprobante is { Length: >= 3 } ? cabecera.Comprobante[..3] : string.Empty;
            string nroFacturaNormalizado = FacturaNormalizer.Normalizar(cabecera.Comprobante);

            VerificacionComprobanteResultDto verificacion = await _ComprobanteVerificationService.VerificarAsync(cabecera.Cuit, nroFacturaNormalizado, tipoComprobante);

            if (verificacion.Resultado != ResultadoVerificacionComprobante.PuedeCargarse)
            {
                if (verificacion.Resultado == ResultadoVerificacionComprobante.FacturaYaRegistrada && verificacion.NumeroInterno > 0)
                {
                    await _comprobanteArcaRepository.CambiarEstadoAsync(idComprobante, (int)EstadoComprobante.CargadoEnTango, verificacion.NumeroInterno);
                }

                return new PrepararFacturaRemitoResultDto
                {
                    PuedePreparar = false,
                    Mensaje = verificacion.Mensaje
                };
            }

            ComprobantesArca? importes = await _comprobanteArcaRepository.ObtenerParaPrepararAsync(idComprobante);
            if (importes == null)
            {
                return new PrepararFacturaRemitoResultDto
                {
                    PuedePreparar = false,
                    Mensaje = "No se encontraron importes del comprobante."
                };
            }

            DateOnly? fechaCierreOpt = await _fechaCierreRepository.GetFechaCierreAsync();
            if (!fechaCierreOpt.HasValue)
            {
                return new PrepararFacturaRemitoResultDto
                {
                    PuedePreparar = false,
                    Mensaje = "Fecha de cierre no configurada."
                };
            }
            DateOnly fechaCierre = fechaCierreOpt.Value;
            DateOnly fechaContable = cabecera.Fecha <= fechaCierre ? fechaCierre.AddDays(1) : cabecera.Fecha;

            string? proveedor = await _proveedorRepository.GetCodProveedorPorCuitAsync(cabecera.Cuit);

            return FacturaRemitoPrepararMapper.ToResult(idComprobante, cabecera, importes, fechaContable, proveedor);
        }

        public async Task<ComprobanteProcesarResult> ProcesarAsync(ProcesarComprobanteRemitoCommand command)
        {
            ProcesarComprobanteBaseData baseData = command.Base;

            if (baseData.OtrosTributos.GetValueOrDefault() > 0)
            {
                decimal suma = baseData.ImportePercepcion.GetValueOrDefault() + baseData.ImportePercepcion2.GetValueOrDefault() + baseData.NoGrabadoOtros.GetValueOrDefault();

                if (suma != baseData.OtrosTributos)
                    return ComprobanteProcesarResult.Fail("La suma de imputaciones no coincide con Otros Tributos.");

                if (baseData.ImputarAId.HasValue && baseData.ImputarAId2.HasValue && baseData.ImputarAId == baseData.ImputarAId2)
                    return ComprobanteProcesarResult.Fail("No se puede seleccionar el mismo impuesto en ambas imputaciones.");
            }

            ResultadoProcesarComprobante resultado = await _comprobanteRepository.ProcesarRemitoAsync(command);

            if (!resultado.Ok)
                return ComprobanteProcesarResult.Fail(resultado.Resultado!);

            return ComprobanteProcesarResult.Success(resultado.NcompInC, $"Procesado en Tango exitosamente con el Número Interno: {resultado.NcompInC}");
        }
    }
}

