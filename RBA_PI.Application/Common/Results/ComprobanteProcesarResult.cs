namespace RBA_PI.Application.Common.Results
{
    public class ComprobanteProcesarResult
    {
        public bool Ok { get; init; }
        public string? Mensaje { get; init; }
        public decimal? NcompInC { get; init; }
        public static ComprobanteProcesarResult Success(decimal? ncompInC, string mensaje) => new() { Ok = true, NcompInC = ncompInC, Mensaje = mensaje };
        public static ComprobanteProcesarResult Fail(string mensaje) => new() { Ok = false, Mensaje = mensaje };
    }
}
