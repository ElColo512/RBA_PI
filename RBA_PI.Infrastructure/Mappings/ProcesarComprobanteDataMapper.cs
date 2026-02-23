using RBA_PI.Application.Commands;
using RBA_PI.Application.Commands.Shared;
using RBA_PI.Application.Helpers;
using RBA_PI.Infrastructure.Persistence.DTOs;
using System.Text.RegularExpressions;

namespace RBA_PI.Infrastructure.Mappings
{
    internal class ProcesarComprobanteDataMapper
    {
        internal static ProcesarComprobanteData FromConcepto(ProcesarComprobanteConceptoCommand command)
        {
            return FromBase(command.Base, "C") with
            {
                CodConcepto = string.IsNullOrEmpty(command.CodConcepto) ? "-1" : command.CodConcepto
            };
        }

        internal static ProcesarComprobanteData FromRemito(ProcesarComprobanteRemitoCommand command)
        {
            return FromBase(command.Base, "R") with
            {
                IdSesion = command.IdSession,
                DepositoId = command.DepositoId ?? -1,
                CodConcepto = "-1"
            };

        }
        private static ProcesarComprobanteData FromBase(ProcesarComprobanteBaseData baseData, string tipoFactura)
        {
            return new ProcesarComprobanteData
            {
                NroFactura = FacturaNormalizer.Normalizar(baseData.NroFactura),
                CuitProveedor = Regex.Replace(baseData.Cuit ?? string.Empty, @"\D", ""),
                NombreCompleto = baseData.UsuarioNombreCompleto,
                TipoFactura = tipoFactura,
                Cae = baseData.Cae,
                FechaEmision = baseData.FechaEmision.ToDateTime(TimeOnly.MinValue).ToString("yyyyMMdd"),
                FechaContable = baseData.FechaContable.ToDateTime(TimeOnly.MinValue).ToString("yyyyMMdd"),
                FechaVencimientoCae = baseData.FechaContable.AddDays(10).ToDateTime(TimeOnly.MinValue).ToString("yyyyMMdd"),
                SectorId = baseData.SectorId ?? -1,
                AuxiliarId = baseData.AuxiliarId ?? -1,
                ImputarAId = baseData.ImputarAId ?? -1,
                ImputarAId2 = baseData.ImputarAId2 ?? -1,
                Observaciones = baseData.Observaciones ?? string.Empty,
                ImporteGravado = baseData.ImporteGravado,
                ImporteNoGravado = baseData.ImporteNoGravado,
                ImporteIva21 = baseData.Iva21,
                ImporteIva105 = baseData.Iva10_5,
                ImporteIva27 = baseData.Iva27,
                ImporteIva5 = baseData.Iva5,
                ImporteIva25 = baseData.Iva2_5,
                ImporteTotal = baseData.ImporteTotal,
                ImportePrecepcion = baseData.ImportePercepcion ?? 0,
                ImportePrecepcion2 = baseData.ImportePercepcion2 ?? 0,
                NoGravadoOtros = baseData.NoGrabadoOtros ?? 0
            };
        }
    }
}

