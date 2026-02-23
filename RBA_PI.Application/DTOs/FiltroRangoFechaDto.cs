namespace RBA_PI.Application.DTOs
{
    public abstract class FiltroRangoFechaDto
    {
        public decimal? CodCliente { get; set; }
        public DateOnly? Desde { get; set; }
        public DateOnly? Hasta { get; set; }
    }
}
