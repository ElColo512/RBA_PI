using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Infrastructure.Services;
using RBA_PI.Web.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RBA_PI.Web.Controllers
{
    [Authorize]
    public class HomeController(IClienteContext clienteContext) : Controller
    {
        private readonly IClienteContext _clienteContext = clienteContext;

        public async Task<IActionResult> Index()
        {
            UserDto user = await _clienteContext.GetUserRequiredAsync();
            ViewBag.NombreCompleto = user.NombreCompleto;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
