namespace RBA_PI.Domain.Queries
{
    public abstract class FiltroRangoFecha
    {
        public decimal CodCliente { get; set; }
        public DateOnly? Desde { get; set; }
        public DateOnly? Hasta { get; set; }
    }
}
