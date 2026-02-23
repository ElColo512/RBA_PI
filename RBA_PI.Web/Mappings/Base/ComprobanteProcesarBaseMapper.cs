using RBA_PI.Application.Commands.Shared;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.Base
{
    internal static class ComprobanteProcesarBaseMapper
    {
        internal static ProcesarComprobanteBaseData ToBaseData(IComprobantePrepararBaseVm vm)
        {
            List<ComprobanteImpuestoItemViewModel> imp = vm.Impuestos.Items;

            decimal Neto(TipoIva t) => imp.FirstOrDefault(x => x.Tipo == t)?.Neto ?? 0m;

            decimal Iva(TipoIva t) => imp.FirstOrDefault(x => x.Tipo == t)?.Iva ?? 0m;

            return new ProcesarComprobanteBaseData(
                NroFactura: vm.Header.Comprobante,
                Proveedor: vm.Header.Proveedor,
                Cuit: vm.Header.Cuit,
                Cae: vm.Header.Cae,
                FechaEmision: vm.Header.FechaEmision,
                FechaContable: vm.Header.FechaContable,
                SectorId: vm.SectorId,
                AuxiliarId: vm.AuxiliarId,
                ImputarAId: vm.OtrosTributos.ImputarAId,
                ImputarAId2: vm.OtrosTributos.ImputarAId2,
                Observaciones: vm.Observaciones,
                Neto21: Neto(TipoIva.Iva21),
                Iva21: Iva(TipoIva.Iva21),
                Neto27: Neto(TipoIva.Iva27),
                Iva27: Iva(TipoIva.Iva27),
                Neto10_5: Neto(TipoIva.Iva105),
                Iva10_5: Iva(TipoIva.Iva105),
                Neto5: Neto(TipoIva.Iva5),
                Iva5: Iva(TipoIva.Iva5),
                Neto2_5: Neto(TipoIva.Iva25),
                Iva2_5: Iva(TipoIva.Iva25),
                OtrosTributos: vm.OtrosTributos.OtrosTributos ?? 0,
                ImportePercepcion: vm.OtrosTributos.ImputarValor ?? 0,
                ImportePercepcion2: vm.OtrosTributos.ImputarValor2 ?? 0,
                NoGrabadoOtros: vm.OtrosTributos.ImputarNoGravado ?? 0,
                ImporteGravado: vm.Totales.ImporteGravado,
                ImporteNoGravado: vm.Totales.ImporteNoGravado,
                ImporteExento: vm.Totales.ImporteExento,
                ImporteTotal: vm.Totales.ImporteTotal,
                UsuarioNombreCompleto: vm.NombreCompleto
            );
        }
    }
}
