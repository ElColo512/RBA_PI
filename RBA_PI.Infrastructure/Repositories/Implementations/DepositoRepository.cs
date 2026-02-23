using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class DepositoRepository(IConnectionStringFactory csFactory) : IDepositoRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;
        public async Task<List<Deposito>> GetAllAsync()
        {
            List<Deposito> lista = [];
            string cs = await _csFactory.CreateAsync();

            string sql = """SELECT COD_SUCURS, NOMBRE_SUC + ' (' + COD_SUCurs collate Latin1_General_BIN + ')' AS Deposito FROM sta22 WHERE INHABILITA = 0""";

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                lista.Add(new Deposito
                {
                    Id = rdr.GetString(0),
                    Descripcion = rdr.GetString(1)
                });
            }

            return lista;
        }
    }
}
