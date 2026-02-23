namespace RBA_PI.Domain.Entities
{
    public class ResultadoProcesarComprobante
    {
        public bool Ok => !Resultado.StartsWith("ERROR");
        public string Resultado { get; init; } = string.Empty;
        public decimal? NcompInC { get; init; }
    }
}
