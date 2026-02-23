using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class AuxiliarRepository(IConnectionStringFactory csFactory) : IAuxiliarRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<List<Auxiliar>> GetAllAsync()
        {
            List<Auxiliar> lista = [];
            string cs = await _csFactory.CreateAsync();

            string sql = """SELECT ID_AUXILIAR, COD_AUXILIAR, DESC_AUXILIAR FROM AUXILIAR WHERE HABILITADO = 'S'""";

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                lista.Add(new Auxiliar
                {
                    Id = rdr.GetInt32(0),
                    Codigo = rdr.GetString(1),
                    Descripcion = rdr.GetString(2)
                });
            }

            return lista;
        }
    }
}
