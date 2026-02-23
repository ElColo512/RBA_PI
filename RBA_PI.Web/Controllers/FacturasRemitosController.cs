using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBA_PI.Application.Commands;
using RBA_PI.Application.Common.Results;
using RBA_PI.Application.DTOs;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.DTOs.FacturasRemitos;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Web.Helpers;
using RBA_PI.Web.Mappings.FacturaRemito;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Controllers
{
    [Authorize]
    public class FacturasRemitosController(IComprobantesQueryService comprobantesQueryService, IFacturasRemitosService service, IEstadosComprobantesService estadosService,
        IClienteContext clienteContext, IComprobanteArcaService comprobanteArcaService, ILookupsService lookupsService, IProveedorDefaultsService proveedorDefaultsService)
        : FacturasBaseController(comprobantesQueryService, estadosService, clienteContext, lookupsService, proveedorDefaultsService, comprobanteArcaService)
    {
        private readonly IFacturasRemitosService _facturasRemitosService = service;

        protected override FacturasIndexConfigViewModel BuildConfig() => new()
        {
            Titulo = "Exportación de Comprobantes a Tango - FACTURAS REMITOS sin Ordenes de Compras Vinculadas",
            MostrarBotonExcel = false,
            UrlVerificar = Url.Action("VerificarComprobanteRemito", "FacturasRemitos")!
        };

        protected override string GetReturnUrl() => Url.Action("Index", "FacturasRemitos")!;
        protected override string GetUrlVerificar(decimal id) => Url.Action("Preparar", "FacturasRemitos", new { id })!;

        [HttpPost]
        public Task<IActionResult> Pendiente([FromBody] CambiarEstadoDto dto) => CambiarEstadoBase(dto, EstadoComprobante.Pendiente, "Comprobante marcado como pendiente.");

        [HttpPost]
        public Task<IActionResult> Descartar([FromBody] CambiarEstadoDto dto) => CambiarEstadoBase(dto, EstadoComprobante.Descartado, "Comprobante descartado correctamente.");

        [HttpPost]
        public Task<IActionResult> CargaDirecta([FromBody] CargaDirectaDto dto) => CambiarEstadoBase(dto, EstadoComprobante.CargadoEnTango, $"Comprobante cargado correctamente en Tango (Int. {dto.Interno}).", dto.Interno);

        public async Task<IActionResult> VerificarComprobanteRemito(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Id de comprobante inválido.", reload = false });

            try
            {
                PrepararFacturaRemitoResultDto result = await _facturasRemitosService.PrepararAsync(id);

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
                PrepararFacturaRemitoResultDto result = await _facturasRemitosService.PrepararAsync(id);
                ConfigurationDto conf = await _clienteContext.GetConfiguracionAsync();

                if (!result.PuedePreparar || result.Data is null)
                {
                    TempData["ToastWarning"] = result.Mensaje;
                    return RedirectToAction(nameof(Index));
                }

                FacturaRemitoPrepararViewModel vm = FacturaRemitoPrepararVmMapper.ToViewModel(result.Data);

                Task<IReadOnlyList<SelectItemDto>> sectoresTask = _lookupsService.ObtenerSectoresAsync();
                Task<IReadOnlyList<SelectItemDto>> depositosTask = _lookupsService.ObtenerDepositosAsync();
                Task<IReadOnlyList<SelectItemDto>> auxiliaresTask = _lookupsService.ObtenerAuxiliaresAsync();
                Task<IReadOnlyList<SelectItemDto>> articulosRemitoTask = _lookupsService.ObtenerArticulosRemitoAsync();
                await Task.WhenAll(sectoresTask, depositosTask, auxiliaresTask, articulosRemitoTask);

                vm.Sectores = (await sectoresTask).ToSelectList();
                vm.Depositos = (await depositosTask).ToSelectList();
                vm.Auxiliares = (await auxiliaresTask).ToSelectList();
                vm.Articulos = (await articulosRemitoTask).ToSelectList();
                vm.SectorId = await _proveedorDefaultsService.ObtenerUltimoSectorAsync(result.Data.Proveedor);
                vm.DepositoId = await _proveedorDefaultsService.ObtenerUltimoDepositoAsync(result.Data.Proveedor);

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
        public async Task<IActionResult> Procesar(FacturaRemitoPrepararViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { ok = false, message = "Errores de validación..." });

            try
            {
                model.NombreCompleto = _clienteContext.GetUserRequiredAsync().Result.NombreCompleto;
                UserDto user = await _clienteContext.GetUserRequiredAsync();
                long idUsuario = Convert.ToInt64(user.IdUsuario);
                model.IdSession = long.Parse($"{idUsuario}{DateTime.Now:yyyyMMddHHmmss}");

                ProcesarComprobanteRemitoCommand command = FacturaRemitoProcesarCommandMapper.ToCommand(model);
                ComprobanteProcesarResult result = await _facturasRemitosService.ProcesarAsync(command);

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

        private static IEnumerable<SelectListItem> CrearSelectList(IReadOnlyList<SelectItemDto> items) => new[] { new SelectListItem { Value = "", Text = "(Seleccionar)" } }.Concat(items.Select(x => new SelectListItem { Value = x.Value, Text = x.Text }));
    }
}
