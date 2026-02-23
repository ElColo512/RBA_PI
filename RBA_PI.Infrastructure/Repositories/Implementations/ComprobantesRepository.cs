using Microsoft.EntityFrameworkCore;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Domain.Queries;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ComprobantesRepository(AppDbContext context) : BaseRepository<VW_COMPROBANTES_ARCA>(context), IComprobantesRepository
    {
        public async Task<List<VwComprobantesArca>> GetAllAsync(ComprobanteFiltro filtro)
        {
            IQueryable<VW_COMPROBANTES_ARCA> query = _context.VW_COMPROBANTES_ARCAs.AsNoTracking().Where(f => f.FECHA >= filtro.Desde &&
                 f.FECHA <= filtro.Hasta && f.COD_CLIENTE == filtro.CodCliente);

            if (filtro.EstadoId.HasValue && filtro.EstadoId != -1)
                query = query.Where(f => f.COD_ESTADO_COMPROBANTE == filtro.EstadoId);

            return await query.OrderBy(f => f.RAZON_SOCIAL)
                .Select(f => new VwComprobantesArca
                {
                    Cuit = f.CUIT ?? string.Empty,
                    RazonSocial = f.RAZON_SOCIAL ?? string.Empty,
                    Fecha = f.FECHA,
                    FechaAMostrar = f.FECHA_A_MOSTRAR ?? string.Empty,
                    Comprobante = f.COMPROBANTE ?? string.Empty,
                    NroComprobante = f.n_COMPROBANTE ?? string.Empty,
                    Moneda = f.MONEDA ?? string.Empty,
                    CodEstadoComprobante = f.COD_ESTADO_COMPROBANTE,
                    Estado = f.Estado ?? string.Empty,
                    CodCliente = f.COD_CLIENTE,
                    IdComprobanteAfip = f.ID_COMPROBANTE_AFIP,
                    Iva = f.IVA,
                    IvaTotal = f.IVA_TOTAL,
                    NcompInC = f.NCOMP_IN_C,
                    ImpNeto = f.IMP_NETO,
                    ImpNoGravado = f.IMP_NO_GRAVADO,
                    ImpExento = f.IMP_EXENTO,
                    OtrosTributos = f.OTROS_TRIBUTOS,
                    ImpTotal = f.IMP_TOTAL
                }).ToListAsync();
        }

        public async Task<VwComprobantesArca?> GetHeaderAsync(decimal idComprobante)
        {
            return await _context.VW_COMPROBANTES_ARCAs
                .AsNoTracking()
                .Where(x => x.ID_COMPROBANTE_AFIP == idComprobante)
                .Select(x => new VwComprobantesArca
                {
                    Cuit = x.CUIT ?? string.Empty,
                    RazonSocial = x.RAZON_SOCIAL ?? string.Empty,
                    Comprobante = x.COMPROBANTE ?? string.Empty,
                    FechaAMostrar = x.FECHA_A_MOSTRAR ?? string.Empty,
                    Fecha = x.FECHA
                })
                .FirstOrDefaultAsync();
        }
    }
}
