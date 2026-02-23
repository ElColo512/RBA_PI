using RBA_PI.Application.DTOs.ClienteContext;

namespace RBA_PI.Web.ViewModels
{
    public class HeaderClienteViewModel
    {
        public UserDto? Usuario { get; set; }
        public ClienteDto? Cliente { get; set; }
        public bool IsAuthenticated => Usuario != null;
    }
}
