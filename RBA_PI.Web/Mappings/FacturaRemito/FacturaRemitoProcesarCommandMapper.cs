using RBA_PI.Application.Commands;
using RBA_PI.Application.Commands.Shared;
using RBA_PI.Web.Mappings.Base;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.FacturaRemito
{
    internal class FacturaRemitoProcesarCommandMapper
    {
        internal static ProcesarComprobanteRemitoCommand ToCommand(FacturaRemitoPrepararViewModel vm)
        {
            ProcesarComprobanteBaseData baseData = ComprobanteProcesarBaseMapper.ToBaseData(vm);

            List<ProcesarComprobanteItemCommand> items = [.. vm.Items.Select((x, index) => new ProcesarComprobanteItemCommand(
                ArticuloId: x.ArticuloId,
                Cantidad: x.Cantidad,
                Precio: x.Precio
                ))];

            return new ProcesarComprobanteRemitoCommand(Base: baseData, IdSession: vm.IdSession, DepositoId: int.TryParse(vm.DepositoId, out var depositoId)
                ? depositoId : 0, TipoFactura: "R", Items: items);
        }
    }
}
