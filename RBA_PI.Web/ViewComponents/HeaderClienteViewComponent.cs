using Microsoft.AspNetCore.Mvc;
using RBA_PI.Application.DTOs.ClienteContext;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.ViewComponents
{
    public class HeaderClienteViewComponent(IClienteContext clienteContext) : ViewComponent
    {
        private readonly IClienteContext _clienteContext = clienteContext;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserDto? user = await _clienteContext.GetUserAsync();
            ClienteDto? cliente = await _clienteContext.GetClienteAsync();

            HeaderClienteViewModel vm = new()
            {
                Usuario = user,
                Cliente = cliente
            };

            return View(vm);
        }
    }
}
