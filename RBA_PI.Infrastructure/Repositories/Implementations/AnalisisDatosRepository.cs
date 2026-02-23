using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Domain.Queries;
using RBA_PI.Infrastructure.Mappings;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Persistence.ReadModels;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class AnalisisDatosRepository(AppDbContext context) : IAnalisisDatosRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<List<VwComprobantesArca>> GetFacturasAsync(AnalisisDatosFiltro filtro, Configuracion cfg)
        {
            string sql = $@"SELECT A.ID_COMPROBANTE_AFIP , a.RAZON_SOCIAL, a.FECHA_A_MOSTRAR, a.CUIT, a.comprobante, a.Moneda, 
                         CAST(a.IMP_TOTAL AS decimal(18,2)) AS IMP_TOTAL, a.Estado, CAST(cpa04.ncomp_in_c AS decimal(18,0)) AS INTERNO                       
                         FROM VW_COMPROBANTES_ARCA a 
                         LEFT JOIN (select t_comp, n_comp, cpa04.cod_provee, ncomp_in_c, cpa01.n_cuit FROM [{cfg.CcServidor}].[{cfg.CcBaseDatos}].dbo.CPA04 
                         INNER JOIN [{cfg.CcServidor}].[{cfg.CcBaseDatos}].dbo.VW_RBA_CPA01 cpa01 ON cpa04.COD_PROVEE = cpa01.COD_PROVEE
                         WHERE t_comp ='FAC') cpa04 ON cpa04.n_comp COLLATE Modern_Spanish_CI_AS = a.N_COMPROBANTE
                         AND cpa04.n_cuit COLLATE Modern_Spanish_CI_AS = a.CUIT 
                         WHERE a.COD_CLIENTE = @cliente AND a.FECHA BETWEEN @desde AND @hasta
                         AND (cpa04.t_comp = 'FAC' OR cpa04.t_comp IS NULL) AND a.COMPROBANTE like 'FAC%'";
            //string sql = $@"SELECT a.RAZON_SOCIAL, a.FECHA_A_MOSTRAR, a.CUIT, a.comprobante, a.Moneda, CAST(a.IMP_TOTAL AS decimal(18,2)) AS IMP_TOTAL, 
            //             a.Estado, a.ID_COMPROBANTE_AFIP, CAST(cpa04.ncomp_in_c AS decimal(18,0)) AS INTERNO                       
            //             FROM VW_COMPROBANTES_ARCA a 
            //             LEFT JOIN [{cfg.CcServidor}].[{cfg.CcBaseDatos}].dbo.CPA04 cpa04 ON cpa04.n_comp COLLATE Modern_Spanish_CI_AS = a.N_COMPROBANTE
            //             LEFT JOIN [{cfg.CcServidor}].[{cfg.CcBaseDatos}].dbo.VW_RBA_CPA01 cpa01 ON cpa04.COD_PROVEE = cpa01.COD_PROVEE
            //             AND cpa01.n_cuit COLLATE Modern_Spanish_CI_AS = a.CUIT 
            //             WHERE a.COD_CLIENTE = @cliente AND a.FECHA BETWEEN @desde AND @hasta AND (cpa04.t_comp = 'FAC' OR cpa04.t_comp IS NULL)";

            List<AnalisisFacturaRow> rows = await _context.AnalisisFacturaRows.FromSqlRaw(sql, new SqlParameter("@cliente", filtro.CodCliente),
                new SqlParameter("@desde", filtro.Desde), new SqlParameter("@hasta", filtro.Hasta)).AsNoTracking().ToListAsync();

            return [.. rows.Select(AnalisisDatosMapper.ToDomain)];
        }
    }
}
