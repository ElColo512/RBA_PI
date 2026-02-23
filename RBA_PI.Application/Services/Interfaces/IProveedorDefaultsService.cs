namespace RBA_PI.Application.Services.Interfaces
{
    public interface IProveedorDefaultsService
    {
        Task<int> ObtenerUltimoSectorAsync(string codProveedor);
        Task<string> ObtenerUltimoConceptoAsync(string codProveedor);
        Task<string> ObtenerUltimoDepositoAsync(string codProveedor);
    }
}
