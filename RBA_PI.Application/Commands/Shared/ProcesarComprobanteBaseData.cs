namespace RBA_PI.Application.Commands.Shared
{
    public record ProcesarComprobanteBaseData(string NroFactura, string Proveedor, string Cuit, string Cae, DateOnly FechaEmision, DateOnly FechaContable, int? SectorId, string? Observaciones,
        int? AuxiliarId, int? ImputarAId, int? ImputarAId2, decimal? Neto21, decimal? Iva21, decimal? Neto27, decimal? Iva27, decimal? Neto10_5, decimal? Iva10_5, decimal? Neto5, decimal? Iva5,
        decimal? Neto2_5, decimal? Iva2_5, decimal? OtrosTributos, decimal? ImportePercepcion, decimal? ImportePercepcion2, decimal? NoGrabadoOtros, decimal? ImporteGravado, decimal? ImporteNoGravado,
        decimal? ImporteExento, decimal? ImporteTotal, string UsuarioNombreCompleto);
}
