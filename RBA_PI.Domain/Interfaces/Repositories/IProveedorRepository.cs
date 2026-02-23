namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IProveedorRepository
    {
        Task<string?> GetCodProveedorPorCuitAsync(string cuit);
        Task<int?> GetUltimoSectorAsync(string codProveedor);
        Task<string?> GetUltimoConceptoAsync(string codProveedor);
        Task<string?> GetUltimoDepositoAsync(string codProveedor);
    }
}
