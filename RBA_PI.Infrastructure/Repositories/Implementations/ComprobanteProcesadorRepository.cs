using Microsoft.Data.SqlClient;
using RBA_PI.Application.Commands;
using RBA_PI.Application.Interfaces.Repositories;
using RBA_PI.Domain.Entities;
using RBA_PI.Infrastructure.Data.ConnectionStrings;
using RBA_PI.Infrastructure.Helpers;
using RBA_PI.Infrastructure.Mappings;
using RBA_PI.Infrastructure.Persistence.DTOs;
using RBA_PI.Infrastructure.Persistence.Sql;
using System.Data;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ComprobanteProcesadorRepository(IConnectionStringFactory csFactory) : IComprobanteProcesadorRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;

        public async Task<ResultadoProcesarComprobante> ProcesarConceptoAsync(ProcesarComprobanteConceptoCommand command)
        {
            string cs = await _csFactory.CreateAsync();
            await using SqlConnection cn = new(cs);
            await cn.OpenAsync();
           

            try
            {
                ProcesarComprobanteData data = ProcesarComprobanteDataMapper.FromConcepto(command);
                ResultadoProcesarComprobante result = await EjecutarSpAsync(cn, data);
         
                return result;
            }
            catch
            {
          
                throw;
            }
        }

        public async Task<ResultadoProcesarComprobante> ProcesarRemitoAsync(ProcesarComprobanteRemitoCommand command)
        {
            string cs = await _csFactory.CreateAsync();
            await using SqlConnection cn = new(cs);
            await cn.OpenAsync();
       

            try
            {
                await InsertarRenglonesAsync(cn, command.IdSession, command.Items);

                ProcesarComprobanteData data = ProcesarComprobanteDataMapper.FromRemito(command);
                ResultadoProcesarComprobante result = await EjecutarSpAsync(cn, data);

            
                return result;
            }
            catch
            {
              
                throw;
            }
        }

        private static async Task<ResultadoProcesarComprobante> EjecutarSpAsync(SqlConnection cn, ProcesarComprobanteData data)
        {
            string sql = SqlResourceLoader.Load("RBA_PI.Infrastructure.Persistence.StoredProcedures.ProcesarComprobante.sql");

            await using var cmd = new SqlCommand(sql, cn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = 0
            };

            cmd.Parameters.Add("@NRO_FACTURA", SqlDbType.VarChar, 14).Value = data.NroFactura;
            cmd.Parameters.Add("@CUIT_PROVEEDOR", SqlDbType.VarChar, 11).Value = data.CuitProveedor;
            cmd.Parameters.Add("@COD_CONCEPTO", SqlDbType.VarChar, 3).Value = data.CodConcepto;
            cmd.Parameters.Add("@LEYENDA", SqlDbType.VarChar, 30).Value = data.Observaciones ?? string.Empty;
            cmd.Parameters.Add("@NOMBRE_COMPLETO", SqlDbType.VarChar, 120).Value = data.NombreCompleto;
            cmd.Parameters.Add("@TIPO_FACTURA", SqlDbType.VarChar, 1).Value = data.TipoFactura;
            cmd.Parameters.Add("@CAE", SqlDbType.VarChar, 15).Value = data.Cae;
            cmd.Parameters.Add("@F_CONTABLE", SqlDbType.VarChar, 8).Value = data.FechaContable;
            cmd.Parameters.Add("@F_EMISION", SqlDbType.VarChar, 8).Value = data.FechaEmision;
            cmd.Parameters.Add("@F_VTO_CAE", SqlDbType.VarChar, 8).Value = data.FechaVencimientoCae;
            cmd.Parameters.Add("@ID_SECTOR", SqlDbType.Decimal).Value = (object?)data.SectorId ?? DBNull.Value;
            cmd.Parameters.Add("@ID_AUXILIAR", SqlDbType.Decimal).Value = (object?)data.AuxiliarId ?? DBNull.Value;
            cmd.Parameters.Add("@ID_PERCEPCION", SqlDbType.Decimal).Value = (object?)data.ImputarAId ?? DBNull.Value;
            cmd.Parameters.Add("@ID_PERCEPCION2", SqlDbType.Decimal).Value = (object?)data.ImputarAId2 ?? DBNull.Value;
            cmd.Parameters.Add("@ID_SESION", SqlDbType.Decimal).Value = data.IdSesion;
            AddDecimal(cmd, "@IMPORTE_GRAVADO", data.ImporteGravado);
            AddDecimal(cmd, "@IMPORTE_NO_GRAVADO", data.ImporteNoGravado);
            AddDecimal(cmd, "@IMPORTE_IVA_21", data.ImporteIva21);
            AddDecimal(cmd, "@IMPORTE_IVA_105", data.ImporteIva105);
            AddDecimal(cmd, "@IMPORTE_IVA_27", data.ImporteIva27);
            AddDecimal(cmd, "@IMPORTE_IVA_5", data.ImporteIva5);
            AddDecimal(cmd, "@IMPORTE_IVA_25", data.ImporteIva25);
            AddDecimal(cmd, "@IMPORTE_TOTAL", data.ImporteTotal);
            AddDecimal(cmd, "@IMPORTE_PERCEPCION", data.ImportePrecepcion);
            AddDecimal(cmd, "@IMPORTE_PERCEPCION2", data.ImportePrecepcion2);
            AddDecimal(cmd, "@NO_GRAVADO_OTROS", data.NoGravadoOtros);

#if DEBUG
            string sqlDebug = SqlDebugHelper.GenerarSqlConDeclares(cmd, sql);
            File.WriteAllText("debug_procesar_comprobante.sql", sqlDebug);
            Console.WriteLine(Directory.GetCurrentDirectory());
#endif
            await using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.Read())
            {
                return new ResultadoProcesarComprobante
                {
                    Resultado = "ERROR: El SP no devolvió resultado.",
                    NcompInC = null
                };
            }

            string resultado = reader.GetString(reader.GetOrdinal("RESULTADO"));
            decimal? ncompInC = reader.IsDBNull(reader.GetOrdinal("NCOMP_IN_C")) ? null : reader.GetDecimal(reader.GetOrdinal("NCOMP_IN_C"));

            return new ResultadoProcesarComprobante
            {
                Resultado = resultado,
                NcompInC = ncompInC
            };
        }

        private static async Task InsertarRenglonesAsync(SqlConnection cn, long idSession, IReadOnlyList<ProcesarComprobanteItemCommand> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                ProcesarComprobanteItemCommand item = items[i];

                SqlCommand cmd = new("""INSERT INTO RBA_TEMP_RENGLONES (ID_SESION, N_RENGLON, ID_STA11, CANTIDAD, PRECIO) VALUES (@IdSesion, @NRenglon, @IdSta11, @Cantidad, @Precio)""", cn);

                cmd.Parameters.Add("@IdSesion", SqlDbType.Decimal).Value = idSession;
                cmd.Parameters.Add("@NRenglon", SqlDbType.Int).Value = i + 1;
                cmd.Parameters.Add("@IdSta11", SqlDbType.Int).Value = item.ArticuloId;
                cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = item.Cantidad;
                cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = item.Precio;

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static void AddDecimal(SqlCommand cmd, string name, decimal? value)
        {
            SqlParameter p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = 18;
            p.Scale = 2;
            p.Value = value;
        }
    }
}
