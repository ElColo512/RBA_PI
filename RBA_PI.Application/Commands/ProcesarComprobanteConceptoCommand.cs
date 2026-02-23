using RBA_PI.Application.Commands.Shared;

namespace RBA_PI.Application.Commands
{
    public record ProcesarComprobanteConceptoCommand(ProcesarComprobanteBaseData Base, string CodConcepto, string TipoFactura);
}
