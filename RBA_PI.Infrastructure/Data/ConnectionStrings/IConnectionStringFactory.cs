namespace RBA_PI.Infrastructure.Data.ConnectionStrings
{
    public interface IConnectionStringFactory
    {
        Task<string> CreateAsync();
    }
}
