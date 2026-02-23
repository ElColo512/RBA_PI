using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBA_PI.Application.DTOs;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.DTOs.Estados;
using RBA_PI.Application.DTOs.FacturasConceptos;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Web.Helpers;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Controllers
{
    public abstract class FacturasBaseController(IComprobantesQueryService comprobantesQueryService, IEstadosComprobantesService estadosService,
        IClienteContext clienteContext, ILookupsService lookupsService, IProveedorDefaultsService proveedorDefaultsService,
        IComprobanteEstadoService comprobanteEstadoService) : Controller
    {
        protected readonly IComprobantesQueryService _queryService = comprobantesQueryService;
        protected readonly IEstadosComprobantesService _estadosService = estadosService;
        protected readonly IClienteContext _clienteContext = clienteContext;
        protected readonly ILookupsService _lookupsService = lookupsService;
        protected readonly IProveedorDefaultsService _proveedorDefaultsService = proveedorDefaultsService;
        protected readonly IComprobanteEstadoService _comprobanteEstadoService = comprobanteEstadoService;
        protected static readonly string FiltrosSessionKey = "ComprobantesFiltros";
        protected static readonly string FiltrosGlobalesKey = "FiltrosGlobales";

        //Configuración específica que define cada hijo
        protected abstract FacturasIndexConfigViewModel BuildConfig();
        protected abstract string GetUrlVerificar(decimal id);
        protected virtual string GetReturnUrl() => Url.Action("Index", ControllerContext.ActionDescriptor.ControllerName)!;

        public async Task<IActionResult> Index()
        {
            FiltrosGlobalesDto? globales = PageStateSession.Load<FiltrosGlobalesDto>(HttpContext.Session, FiltrosGlobalesKey)?.Filters;
            ComprobanteFiltroDto? propios = PageStateSession.Load<ComprobanteFiltroDto>(HttpContext.Session, FiltrosSessionKey)?.Filters;

            DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
            List<EstadoComprobanteDto> estadosDto = await _estadosService.ObtenerSelectAsync();

            List<SelectListItem> estadosSelect = [.. estadosDto
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Descripcion
                })];

            estadosSelect.Insert(0, new SelectListItem
            {
                Value = "-1",
                Text = "Todos"
            });

            FacturasIndexViewModel model = new()
            {
                Filtros = new FacturasConceptosViewModel
                {
                    Desde = globales?.Desde ?? hoy.AddMonths(-1),
                    Hasta = globales?.Hasta ?? hoy,
                    EstadoId = propios?.EstadoId ?? -1,
                    Estados = estadosSelect
                },
                Config = BuildConfig(),
                ReturnUrl = GetReturnUrl()
            };

            return View("~/Views/Shared/Comprobantes/FacturasIndex.cshtml", model);
        }

        public async Task<IActionResult> Buscar([FromQuery] ComprobanteFiltroDto filtros)
        {
            DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
            UserDto user = await _clienteContext.GetUserRequiredAsync();

            IReadOnlyList<FacturaComprobanteDto> comprobantes = await _queryService.ObtenerAsync(new ComprobanteFiltroDto
            {
                CodCliente = user.CodCliente,
                Desde = filtros.Desde ?? hoy.AddMonths(-1),
                Hasta = filtros.Hasta ?? hoy,
                EstadoId = filtros.EstadoId
            });

            return Json(new
            {
                data = comprobantes.Select(c => new
                {
                    id = c.Id,
                    fecha = c.Fecha.ToString("dd/MM/yyyy"),
                    razonSocial = c.RazonSocial,
                    cuit = c.Cuit,
                    comprobante = c.Comprobante,
                    importeTotal = c.ImporteFormateado,
                    estado = c.Estado,
                    urlVerificar = GetUrlVerificar(c.Id)
                })
            });
        }

        [HttpPost]
        public IActionResult GuardarFiltros([FromBody] GuardarFiltrosFacturasConceptosDto dto)
        {
            if (dto == null) return BadRequest();

            PageStateSession.Save(HttpContext.Session,
                FiltrosGlobalesKey,
                new FiltrosGlobalesDto
                {
                    Desde = dto.Desde,
                    Hasta = dto.Hasta
                });

            PageStateSession.Save(HttpContext.Session,
                FiltrosSessionKey,
                new ComprobanteFiltroDto
                {
                    EstadoId = dto.EstadoId
                });

            return Ok();
        }

        protected async Task<IActionResult> CambiarEstadoBase(CambiarEstadoDto dto, EstadoComprobante nuevoEstado, string mensajeExito, decimal? interno = null)
        {
            if (dto == null || dto.IdComprobante <= 0) return BadRequest(new { message = "Datos inválidos." });

            try
            {
                await _comprobanteEstadoService.CambiarEstadoAsync(dto.IdComprobante, nuevoEstado);
                return Ok(new { message = mensajeExito });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = $"Error al actualizar el estado a {nuevoEstado}." });
            }
        }
    }
}
