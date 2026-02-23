using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class SectorRepository(IConnectionStringFactory csFactory) : ISectorRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<List<Sector>> GetAllAsync()
        {
            List<Sector> lista = [];
            string cs = await _csFactory.CreateAsync();

            string sql = """SELECT ID_CPA51, COD_SECTOR, DESC_SECTO FROM CPA51 ORDER BY DESC_SECTO""";

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                lista.Add(new Sector
                {
                    IdCpa51 = rdr.GetInt32(0),
                    Codigo = rdr.GetString(1),
                    Descripcion = rdr.GetString(2)
                });
            }

            return lista;
        }
    }
}
