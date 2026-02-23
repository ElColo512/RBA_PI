// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Identity.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace RBA_PI.Web.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IClienteRepository clienteRepository) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "El campo {0} es requerido.")]
            public string Cuit { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "El campo {0} es requerido.")]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // validar CUIT contra cliente
                var cliente = await _clienteRepository.ObtenerPorClienteAsync(user.CodCliente!.Value);

                if (cliente.Cuit != Input.Cuit)
                {
                    ModelState.AddModelError(string.Empty, "Los datos no coinciden.");
                    return Page();
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                   Input.Email,
                     "Restablecimiento de contraseña",
                     $@"<p>Hemos recibido una solicitud para restablecer su contraseña.</p>
                         <p>
                            Para continuar, haga clic en el siguiente enlace:
                            <br />
                            <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Restablecer Contraseña</a>        
                         </p>
                         <p>
                            Si usted no solicitó este cambio, puede ignorar este correo.
                         </p>");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
