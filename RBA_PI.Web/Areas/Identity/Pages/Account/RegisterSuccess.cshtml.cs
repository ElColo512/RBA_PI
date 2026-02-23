using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RBA_PI.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterSuccess : PageModel
    {
        public void OnGet()
        {
        }
    }
}
