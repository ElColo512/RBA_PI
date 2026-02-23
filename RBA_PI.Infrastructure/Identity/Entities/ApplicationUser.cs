using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBA_PI.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public decimal? CodCliente { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
