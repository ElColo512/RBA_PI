using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ConceptoRepository(IConnectionStringFactory csFactory) : IConceptoRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<List<Concepto>> GetAllAsync()
        {
            List<Concepto> lista = [];
            string cs = await _csFactory.CreateAsync();

            var sql = """SELECT COD_CONCEP, DESC_CONCE FROM CPA45 WHERE INHABILITA = 0 ORDER BY DESC_CONCE""";

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                lista.Add(new Concepto
                {
                    Codigo = rdr.GetString(0),
                    Descripcion = rdr.GetString(1)
                });
            }

            return lista;
        }
    }
}
