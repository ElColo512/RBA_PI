namespace RBA_PI.Application.DTOs.FacturasConceptos
{
    public class GuardarFiltrosFacturasConceptosDto
    {
        public DateOnly Desde { get; set; }
        public DateOnly Hasta { get; set; }
        public int? EstadoId { get; set; }
    }
}
