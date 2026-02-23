using RBA_PI.Application.Commands.Shared;

namespace RBA_PI.Application.Commands
{
    public record ProcesarComprobanteRemitoCommand(ProcesarComprobanteBaseData Base, long IdSession, int? DepositoId, IReadOnlyList<ProcesarComprobanteItemCommand> Items, string TipoFactura);
}
