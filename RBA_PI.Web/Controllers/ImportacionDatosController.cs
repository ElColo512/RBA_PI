using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.DTOs.ImportacionDatos;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Controllers
{
    [Authorize]
    public class ImportacionDatosController(IComprobanteArcaService comprobanteArcaService, IExcelFileService excelFileService, IExcelParserService excelParserService, IClienteContext clienteContext) : Controller
    {
        private readonly IComprobanteArcaService _comprobanteArcaService = comprobanteArcaService;
        private readonly IExcelFileService _excelFileService = excelFileService;
        private readonly IExcelParserService _excelParserService = excelParserService;
        private readonly IClienteContext _clienteContext = clienteContext;

        public IActionResult Index(string? returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
                HttpContext.Session.SetString("Importacion_ReturnUrl", returnUrl);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Procesar(ImportacionExcelViewModel model)
        {
            string path = string.Empty;

            if (model.Archivo == null || model.Archivo.Length == 0)
            {
                TempData["Error"] = "Debe seleccionar un archivo Excel.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                UserDto user = await _clienteContext.GetUserRequiredAsync();
                path = await _excelFileService.SaveAsync(model.Archivo.OpenReadStream(), model.Archivo.FileName);
                List<ComprobanteArcaImportDto> lista = _excelParserService.Parse(path, user.CodCliente);
                int insertados = await _comprobanteArcaService.InsertarAsync(lista);
                TempData["OK"] = $"Proceso finalizado. Registros insertados: {insertados}";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = ex.Message;
            }
            finally
            {
                _excelFileService.Delete(path);
            }

            return RedirectToAction("Index");
        }
    }
}
