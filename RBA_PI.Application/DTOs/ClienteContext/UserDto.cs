namespace RBA_PI.Application.DTOs.ClienteContext
{
    public class UserDto
    {
        public int IdUsuario { get; set; }
        public decimal? CodCliente { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
