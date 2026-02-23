using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class FechaCierreRepository(IConnectionStringFactory csFactory) : IFechaCierreRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<DateOnly?> GetFechaCierreAsync()
        {
            string cs = await _csFactory.CreateAsync();
            const string sql = "SELECT CIERRE_CON FROM CPA10";

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value) return null;

            return DateOnly.FromDateTime((DateTime)result);
        }
    }
}
