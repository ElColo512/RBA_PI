namespace RBA_PI.Infrastructure.Persistence.DTOs
{
    internal record ProcesarComprobanteData
    {
        public string NroFactura { get; init; } = string.Empty;
        public string CuitProveedor { get; init; } = string.Empty;
        public string FechaContable { get; init; } = string.Empty;
        public string FechaEmision { get; init; } = string.Empty;
        public string Cae { get; init; } = string.Empty;
        public string FechaVencimientoCae { get; init; } = string.Empty;
        public long IdSesion { get; init; }
        public string Observaciones { get; init; } = string.Empty;
        public string NombreCompleto { get; init; } = string.Empty;
        public string TipoFactura { get; init; } = string.Empty;
        public decimal? ImporteGravado { get; init; }
        public decimal? ImporteNoGravado { get; init; }
        public decimal? ImporteIva21 { get; init; }
        public decimal? ImporteIva105 { get; init; }
        public decimal? ImporteIva27 { get; init; }
        public decimal? ImporteIva5 { get; init; }
        public decimal? ImporteIva25 { get; init; }
        public decimal? ImporteTotal { get; init; }
        public string CodConcepto { get; init; } = string.Empty;
        public string Leyenda { get; set; } = string.Empty;
        public int? SectorId { get; init; }
        public int DepositoId { get; init; }
        public int? AuxiliarId { get; init; }
        public int? ImputarAId { get; init; }
        public decimal ImportePrecepcion { get; init; }
        public int? ImputarAId2 { get; init; }
        public decimal ImportePrecepcion2 { get; init; }
        public decimal NoGravadoOtros { get; init; }
    }
}
