using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBA_PI.Application.Commands;
using RBA_PI.Application.Common.Results;
using RBA_PI.Application.DTOs;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.DTOs.FacturasConceptos;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Web.Helpers;
using RBA_PI.Web.Mappings.FacturaConcepto;
using RBA_PI.Web.ViewModels;


namespace RBA_PI.Web.Controllers
{
    [Authorize]
    public class FacturasConceptosController(IComprobantesQueryService comprobantesQueryService, IFacturasConceptosService service, IEstadosComprobantesService estadosService,
        IClienteContext clienteContext, IComprobanteArcaService comprobanteArcaService, ILookupsService lookupsService, IProveedorDefaultsService proveedorDefaultsService)
        : FacturasBaseController(comprobantesQueryService, estadosService, clienteContext, lookupsService, proveedorDefaultsService, comprobanteArcaService)
    {
        private readonly IFacturasConceptosService _facturasConceptosService = service;

        protected override FacturasIndexConfigViewModel BuildConfig() => new()
        {
            Titulo = "Exportación de Comprobantes a Tango - Facturas de Conceptos",
            MostrarBotonExcel = true,
            UrlVerificar = Url.Action("VerificarComprobante", "FacturasConceptos")!
        };

        protected override string GetReturnUrl() => Url.Action("Index", "FacturasConceptos")!;
        protected override string GetUrlVerificar(decimal id) => Url.Action("Preparar", "FacturasConceptos", new { id })!;

        [HttpPost]
        public Task<IActionResult> Pendiente([FromBody] CambiarEstadoDto dto) => CambiarEstadoBase(dto, EstadoComprobante.Pendiente, "Comprobante marcado como pendiente.");

        [HttpPost]
        public Task<IActionResult> Descartar([FromBody] CambiarEstadoDto dto) => CambiarEstadoBase(dto, EstadoComprobante.Descartado, "Comprobante descartado correctamente.");

        [HttpPost]
        public Task<IActionResult> CargaDirecta([FromBody] CambiarEstadoDto dto) => CambiarEstadoBase(dto, EstadoComprobante.CargadoEnTango, "Comprobante cargado directamente en Tango");

        public async Task<IActionResult> VerificarComprobante(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Id de comprobante inválido.", reload = false });

            try
            {
                PrepararFacturaConceptoResultDto result = await _facturasConceptosService.PrepararAsync(id);

                if (!result.PuedePreparar) return BadRequest(new { message = result.Mensaje, reload = true });

                return Ok(new { redirectUrl = GetUrlVerificar(id) });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "El comprobante no existe.", reload = true });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", type = "danger", reload = true });
            }
        }

        public async Task<IActionResult> Preparar(int id)
        {
            try
            {
                PrepararFacturaConceptoResultDto result = await _facturasConceptosService.PrepararAsync(id);
                ConfigurationDto conf = await _clienteContext.GetConfiguracionAsync();

                if (!result.PuedePreparar || result.Data is null)
                {
                    TempData["ToastWarning"] = result.Mensaje;
                    return RedirectToAction(nameof(Index));
                }

                FacturaConceptoPrepararViewModel vm = FacturaConceptoPrepararVmMapper.ToViewModel(result.Data);

                Task<IReadOnlyList<SelectItemDto>> sectoresTask = _lookupsService.ObtenerSectoresAsync();
                Task<IReadOnlyList<SelectItemDto>> conceptosTask = _lookupsService.ObtenerConceptosAsync();
                Task<IReadOnlyList<SelectItemDto>> auxiliaresTask = _lookupsService.ObtenerAuxiliaresAsync();
                await Task.WhenAll(sectoresTask, conceptosTask, auxiliaresTask);

                vm.Sectores = (await sectoresTask).ToSelectList();
                vm.Conceptos = (await conceptosTask).ToSelectList();
                vm.Auxiliares = (await auxiliaresTask).ToSelectList();
                vm.SectorId = await _proveedorDefaultsService.ObtenerUltimoSectorAsync(result.Data.Proveedor);
                vm.ConceptoId = await _proveedorDefaultsService.ObtenerUltimoConceptoAsync(result.Data.Proveedor);

                vm.OtrosTributos.PuedeImputarOtrosTributos = vm.OtrosTributos.OtrosTributos.GetValueOrDefault() > 0;

                if (vm.OtrosTributos.PuedeImputarOtrosTributos)
                {
                    Task<IReadOnlyList<SelectItemDto>> taskPrimer = _lookupsService.ObtenerImputarAAsync(TipoImpuesto.PrimerImpuesto);
                    Task<IReadOnlyList<SelectItemDto>> taskSegundo = _lookupsService.ObtenerImputarAAsync(TipoImpuesto.SegundoImpuesto);
                    await Task.WhenAll(taskPrimer, taskSegundo);

                    IReadOnlyList<SelectItemDto> itemsPrimerImpuesto = taskPrimer.Result;
                    IReadOnlyList<SelectItemDto> itemsSegundoImpuesto = taskSegundo.Result;

                    vm.OtrosTributos.ImputarAOptions = CrearSelectList(itemsPrimerImpuesto);
                    vm.OtrosTributos.ImputarAOptions2 = CrearSelectList(itemsSegundoImpuesto);
                }

                vm.UsaSector = conf.USA_SECTOR.GetValueOrDefault();
                vm.UsaAuxiliar = conf.USA_AUXILIAR.GetValueOrDefault();

                return View(vm);
            }
            catch (Exception)
            {
                TempData["ToastError"] = "Ocurrió un error inesperado.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Procesar(FacturaConceptoPrepararViewModel model)
        {
            if (!ModelState.IsValid) return Json(new { ok = false, message = "Errores de validación..." });

            try
            {
                model.NombreCompleto = _clienteContext.GetUserRequiredAsync().Result.NombreCompleto;
                ProcesarComprobanteConceptoCommand command = FacturaConceptoProcesarCommandMapper.ToCommand(model);
                ComprobanteProcesarResult result = await _facturasConceptosService.ProcesarAsync(command);

                if (result.Ok)
                {
                    await CambiarEstadoBase(new CambiarEstadoDto { IdComprobante = model.Header.IdComprobante }, EstadoComprobante.Procesado, "Comprobante procesado correctamente.", result.NcompInC);
                    TempData["ToastMessage"] = result.Mensaje;
                    return Json(new { ok = true, redirectUrl = Url.Action(nameof(Index)) });
                }

                return Json(new { ok = false, message = result.Mensaje });
            }
            catch (Exception)
            {
                return Json(new { ok = false, message = "Ocurrió un error inesperado al procesar el comprobante." });
            }
        }

        public async Task<IActionResult> ExportarExcel(DateOnly desde, DateOnly hasta, int? estadoId)
        {
            try
            {
                UserDto user = await _clienteContext.GetUserRequiredAsync();

                IReadOnlyList<FacturasConceptosExcelDto> datos = await _facturasConceptosService.ObtenerParaExcelAsync(
                    new ComprobanteFiltroDto
                    {
                        CodCliente = user.CodCliente,
                        Desde = desde,
                        Hasta = hasta,
                        EstadoId = estadoId == -1 ? null : estadoId
                    }
                );

                if (!datos.Any()) return BadRequest(new { message = "No hay datos para exportar." });

                byte[] excelBytes = _facturasConceptosService.ExportarFacturasConceptos(datos);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Facturas_de_Conceptos.xlsx");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al generar el archivo Excel.");
            }
        }

        private static IEnumerable<SelectListItem> CrearSelectList(IReadOnlyList<SelectItemDto> items) => new[] { new SelectListItem { Value = "", Text = "(Seleccionar)" } }.Concat(items.Select(x => new SelectListItem { Value = x.Value, Text = x.Text }));
    }
}
