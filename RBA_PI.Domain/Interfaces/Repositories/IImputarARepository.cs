using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Entities;

namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IImputarARepository
    {
        Task<List<ImputarAItem>> GetImpuestoAsync(TipoImpuesto tipo);
    }
}
