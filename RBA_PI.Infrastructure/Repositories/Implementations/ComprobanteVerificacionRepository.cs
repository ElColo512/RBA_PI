using Microsoft.Data.SqlClient;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Domain.ReadModels;
using RBA_PI.Infrastructure.Data.ConnectionStrings;
using System.Data;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ComprobanteVerificacionRepository(IConnectionStringFactory csFactory) : IComprobanteVerificacionRepository
    {
        private readonly IConnectionStringFactory _csFactory = csFactory;
        public async Task<ComprobanteVerificacionData?> VerificarFacturaAsync(string cuitProveedor, string nroFactura, string tipoComprobante)
        {
            string cs = await _csFactory.CreateAsync();
            const string sql = """               
                       DECLARE @RESULTADO VARCHAR(1000) = 'OK';
                       DECLARE @NCOMP_IN_C   NUMERIC(18,0) = 0;
                       DECLARE @COD_PROVEE   VARCHAR(6);
                       DECLARE @TCOMP_IN_C AS VARCHAR(2)

                       SET @COD_PROVEE = (SELECT TOP 1 COD_PROVEE FROM CPA01 WHERE N_CUIT = @CUIT_PROVEEDOR);

                       IF @COD_PROVEE IS NULL
                       BEGIN
                       SET @RESULTADO = 'Error_proveedor';
                       END
                       ELSE
                       BEGIN
                       
                       IF @T_COMP='FAC' 
                        SET @TCOMP_IN_C ='%'
                       else if @T_COMP ='N/C'
                        SET @TCOMP_IN_C='CP'
                       ELSE IF @T_COMP = 'N/D'
                        SET @TCOMP_IN_C='DP'

                       IF EXISTS (SELECT 1 FROM CPA04 WHERE N_COMP = @NRO_FACTURA AND COD_PROVEE = @COD_PROVEE  AND TCOMP_IN_C  LIKE @TCOMP_IN_C )
                       BEGIN
                       SET @RESULTADO = 'Factura_Existe';
                       SET @NCOMP_IN_C = (SELECT TOP 1 NCOMP_IN_C FROM CPA04 WHERE N_COMP = @NRO_FACTURA AND COD_PROVEE = @COD_PROVEE  AND TCOMP_IN_C  LIKE @TCOMP_IN_C );
                       END
                       END

                       SELECT @RESULTADO AS Resultado, @NCOMP_IN_C AS NCompIn, @TCOMP_IN_C AS TcompInC;          
                """;

            //const string sql = """               
            //           DECLARE @RESULTADO VARCHAR(1000) = 'OK';
            //           DECLARE @NCOMP_IN_C   NUMERIC(18,0) = 0;
            //           DECLARE @COD_PROVEE   VARCHAR(6);

            //           SET @COD_PROVEE = (SELECT TOP 1 COD_PROVEE FROM CPA01 WHERE N_CUIT = @CUIT_PROVEEDOR);

            //           IF @COD_PROVEE IS NULL
            //           BEGIN
            //           SET @RESULTADO = 'Error_proveedor';
            //           END
            //           ELSE
            //           BEGIN

            //           IF EXISTS (SELECT 1 FROM CPA04 WHERE N_COMP = @NRO_FACTURA AND COD_PROVEE = @COD_PROVEE)
            //           BEGIN
            //           SET @RESULTADO = 'Factura_Existe';
            //           SET @NCOMP_IN_C = (SELECT TOP 1 NCOMP_IN_C FROM CPA04 WHERE N_COMP = @NRO_FACTURA AND COD_PROVEE = @COD_PROVEE);
            //           END
            //           END

            //           SELECT @RESULTADO AS Resultado, @NCOMP_IN_C AS NCompIn;          
            //    """;

            await using var cn = new SqlConnection(cs);
            await using var cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@NRO_FACTURA", SqlDbType.VarChar, 14).Value = nroFactura;
            cmd.Parameters.Add("@CUIT_PROVEEDOR", SqlDbType.VarChar, 13).Value = cuitProveedor;
            cmd.Parameters.Add("@T_COMP", SqlDbType.VarChar, 3).Value = tipoComprobante;

            await cn.OpenAsync();
            await using var rdr = await cmd.ExecuteReaderAsync();

            if (!await rdr.ReadAsync()) return null;

            return new ComprobanteVerificacionData
            {
                Resultado = rdr.GetString(0),
                NCompIn = rdr.GetDecimal(1)
            };
        }
    }
}
