namespace RBA_PI.Domain.Interfaces.Repositories
{
    public interface IFechaCierreRepository
    {
        Task<DateOnly?> GetFechaCierreAsync();
    }
}
