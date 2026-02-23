using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ArticulosRemitoRepository(IConnectionStringFactory csFactory) : IArticulosRemitoRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<List<ArticulosRemito>> GetAllAsync()
        {
            List<ArticulosRemito> lista = [];
            string cs = await _csFactory.CreateAsync();

            string sql = """
                SELECT ID_STA11, COD_ARTICU, DESCRIPCIO, DESC_ADIC, RTRIM(DESCRIPCIO COLLATE Latin1_General_BIN) + ISNULL(' ' + RTRIM(DESC_ADIC), '') + ' (' + COD_ARTICU + ')' AS LISTA
                FROM STA11 WHERE perfil IN ('A', 'C')
                """;

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                lista.Add(new ArticulosRemito
                {
                    IdSta11 = rdr.GetInt32(0),
                    Codigo = rdr.GetString(1),
                    Descripcion = rdr.GetString(2)
                });
            }

            return lista;
        }
    }
}
