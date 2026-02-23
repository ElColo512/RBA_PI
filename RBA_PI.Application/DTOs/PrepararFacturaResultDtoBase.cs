namespace RBA_PI.Application.DTOs
{
    public abstract class PrepararFacturaResultDtoBase<T>
    {
        public bool PuedePreparar { get; set; }
        public string? Mensaje { get; set; }
        public T? Data { get; set; }
    }
}
