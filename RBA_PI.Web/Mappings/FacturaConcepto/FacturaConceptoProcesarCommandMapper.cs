using RBA_PI.Application.Commands;
using RBA_PI.Application.Commands.Shared;
using RBA_PI.Web.Mappings.Base;
using RBA_PI.Web.ViewModels;

namespace RBA_PI.Web.Mappings.FacturaConcepto
{
    internal static class FacturaConceptoProcesarCommandMapper
    {
        internal static ProcesarComprobanteConceptoCommand ToCommand(FacturaConceptoPrepararViewModel vm)
        {
            ProcesarComprobanteBaseData baseData = ComprobanteProcesarBaseMapper.ToBaseData(vm);
            return new ProcesarComprobanteConceptoCommand(Base: baseData, CodConcepto: vm.ConceptoId, TipoFactura: "C");
        }
    }
}
