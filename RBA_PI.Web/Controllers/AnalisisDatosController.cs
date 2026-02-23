using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBA_PI.Application.DTOs;
using RBA_PI.Application.DTOs.AnalisisDatos;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Web.Helpers;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Controllers
{
    [Authorize]
    public class AnalisisDatosController(IAnalisisDatosService analisisDatosService, IComprobanteArcaService comprobanteArcaService, IClienteContext clienteContext) : Controller
    {
        private readonly IAnalisisDatosService _analisisDatosService = analisisDatosService;
        private readonly IComprobanteArcaService _comprobanteArcaService = comprobanteArcaService;
        private readonly IClienteContext _clienteContext = clienteContext;

        public IActionResult Index()
        {
            DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
            FiltrosGlobalesDto? globales = PageStateSession.Load<FiltrosGlobalesDto>(HttpContext.Session, "FiltrosGlobales")?.Filters;

            AnalisisDatosViewModel model = new()
            {
                Desde = globales?.Desde ?? hoy.AddMonths(-1),
                Hasta = globales?.Hasta ?? hoy,
            };
            return View(model);
        }

        public async Task<IActionResult> Buscar(DateOnly? desde, DateOnly? hasta)
        {
            try
            {
                DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
                UserDto user = await _clienteContext.GetUserRequiredAsync();

                List<AnalisisFacturaDto> facturas = await _analisisDatosService.ObtenerAsync(new AnalisisDatosFiltroDto
                {
                    CodCliente = user.CodCliente,
                    Desde = desde ?? hoy.AddMonths(-1),
                    Hasta = hasta ?? hoy
                });

                var result = facturas.Select(f => new
                {
                    id = f.Id,
                    fecha = f.Fecha,
                    razonSocial = f.RazonSocial,
                    cuit = f.Cuit,
                    comprobante = f.Comprobante,
                    importeTotal = f.ImporteFormateado,
                    estado = f.Estado,
                    interno = f.Interno
                });

                return Json(new { data = result });
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    success = false,
                    message = "Error al buscar los comprobantes."
                });
            }
        }

        [HttpPost]
        public IActionResult GuardarFiltros([FromBody] GuardarAnalisisDatosConceptosDto dto)
        {
            if (dto == null) return BadRequest();

            PageStateSession.Save(HttpContext.Session, "FiltrosGlobales",
                new FiltrosGlobalesDto
                {
                    Desde = dto.Desde,
                    Hasta = dto.Hasta
                }
            );
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CargaDirecta([FromBody] CargaDirectaDto dto)
        {
            if (dto == null || dto.IdComprobante <= 0)
            {
                return BadRequest(new
                {
                    message = "Datos inválidos para la carga directa."
                });
            }

            try
            {
                await _comprobanteArcaService.CambiarEstadoAsync(dto.IdComprobante, EstadoComprobante.CargadoEnTango);

                return Ok(new
                {
                    message = $"Comprobante cargado correctamente en Tango (Int. {dto.Interno})."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al realizar la carga directa."
                });
            }
        }
    }
}
