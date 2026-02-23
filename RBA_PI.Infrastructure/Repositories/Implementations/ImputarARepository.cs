using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Common.Enums;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Data.ConnectionStrings;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ImputarARepository(IConnectionStringFactory csFactory) : IImputarARepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<List<ImputarAItem>> GetImpuestoAsync(TipoImpuesto tipo)
        {
            List<ImputarAItem> lista = [];
            string cs = await _csFactory.CreateAsync();

            string whereClause = tipo switch
            {
                TipoImpuesto.PrimerImpuesto => "(TIPO_ALI = 'IV' AND PERCEPCION = 1) OR (COD_IVA >= 51)",
                TipoImpuesto.SegundoImpuesto => "COD_IVA >= 51",
                _ => throw new ArgumentOutOfRangeException(nameof(tipo))
            };

            string sql = $"""SELECT ID_CPA14, DESC_IVA + ' (' + CAST(COD_IVA AS VARCHAR(10)) + ')' AS IVA FROM CPA14 WHERE {whereClause} ORDER BY COD_IVA""";

            using var cn = new SqlConnection(cs);
            using var cmd = new SqlCommand(sql, cn);

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                lista.Add(new ImputarAItem
                {
                    Id = rdr.GetInt32(0),
                    Descripcion = rdr.GetString(1)
                });
            }

            return lista;
        }     
    }
}
