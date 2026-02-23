using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;
using System.Data;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ProveedorRepository(IConnectionStringFactory csFactory) : IProveedorRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public Task<string?> GetCodProveedorPorCuitAsync(string cuit)
        {
            string sql = """SELECT COD_PROVEE FROM CPA01 WHERE N_CUIT = @cuit AND FECHA_INHA = '18000101' """;
            return ExecuteScalarAsync<string?>(sql, p => p.Add("@cuit", SqlDbType.VarChar, 13).Value = cuit);
        }

        public Task<string?> GetUltimoConceptoAsync(string codProveedor)
        {
            string sql = """
                SELECT TOP 1 cpa47.COD_CONCEP FROM cpa04 INNER JOIN cpa47 ON cpa04.tcomp_in_c = cpa47.tcomp_in_c AND cpa04.ncomp_in_c = cpa47.NCOMP_IN_C 
                WHERE cpa04.cod_provee = @codProveedor AND cpa04.t_comp = 'FAC' ORDER BY cpa04.FECHA_CONT DESC 
                """;

            return ExecuteScalarAsync<string?>(sql, p => p.Add("@codProveedor", SqlDbType.VarChar, 6).Value = codProveedor);
        }

        public Task<int?> GetUltimoSectorAsync(string codProveedor)
        {
            var sql = """
            SELECT TOP 1 cpa51.ID_CPA51 FROM cpa04 LEFT JOIN cpa51 ON cpa04.COD_SECTOR = cpa51.COD_SECTOR WHERE cpa04.cod_provee = @codProveedor AND cpa04.t_comp = 'FAC'
            ORDER BY cpa04.FECHA_CONT DESC
            """;

            return ExecuteScalarAsync<int?>(sql, p => p.Add("@codProveedor", SqlDbType.VarChar, 6).Value = codProveedor);
        }

        public Task<string?> GetUltimoDepositoAsync(string codProveedor)
        {
            string sql = """SELECT TOP 1 cod_deposi FROM cpa46 WHERE ncomp_in_c IN (select max(ncomp_in_c) AS NCOMP_IN_C FROM cpa04 WHERE cod_provee = @codProveedor AND TCOMP_IN_C ='FS')""";

            return ExecuteScalarAsync<string?>(sql, p => p.Add("@codProveedor", SqlDbType.VarChar, 6).Value = codProveedor);
        }

        public async Task<T?> ExecuteScalarAsync<T>(string sql, Action<SqlParameterCollection> parameters)
        {
            object? result = await ExecuteScalarAsync(sql, parameters);
            return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
        }

        protected async Task<object?> ExecuteScalarAsync(string sql, Action<SqlParameterCollection> parameters)
        {
            string cs = await _csFactory.CreateAsync();
            using SqlConnection conn = new(cs);
            using SqlCommand cmd = new(sql, conn);

            parameters(cmd.Parameters);

            await conn.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }
    }
}
