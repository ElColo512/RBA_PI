using Microsoft.EntityFrameworkCore;
using RBA_PI.Domain.Entities;
using RBA_PI.Domain.Interfaces.Repositories;
using RBA_PI.Infrastructure.Mappings;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Persistence.Entities;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class ComprobanteArcaRepository(AppDbContext context) : BaseRepository<COMPROBANTES_ARCA>(context), IComprobanteArcaRepository
    {
        public async Task AddRangeAsync(List<ComprobantesArca> entities)
        {
            List<COMPROBANTES_ARCA> persistenceEntities = [.. entities.Select(ComprobantesArcaMapper.ToPersistence)];

            _context.COMPROBANTES_ARCAs.AddRange(persistenceEntities);
            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync(decimal idComprobante, int nuevoEstado, decimal? interno)
        {
            Dictionary<string, object?> fields = new()
            {
                ["COD_ESTADO_COMPROBANTE"] = nuevoEstado
            };

            if (interno.HasValue)
                fields["NCOMP_IN_C"] = interno;

            await UpdateAsync(x => x.ID_COMPROBANTE_AFIP == idComprobante, fields);
        }

        public async Task<bool> ExistsAsync(string codAutorizacion, decimal? codCliente) => await _context.COMPROBANTES_ARCAs.AnyAsync(c => c.COD_AUTORIZACION == codAutorizacion && c.COD_CLIENTE == codCliente);

        public async Task<ComprobantesArca?> ObtenerParaPrepararAsync(decimal id)
        {
            COMPROBANTES_ARCA? entity = await _context.COMPROBANTES_ARCAs.FirstOrDefaultAsync(x => x.ID_COMPROBANTE_AFIP == id);
            return entity == null ? null : ComprobantesArcaMapper.ToDomain(entity);
        }

        public async Task<ComprobanteParaCambioEstado?> ObtenerDatosParaVerificacionAsync(decimal id)
        {
            VW_COMPROBANTES_ARCA? entity = await _context.VW_COMPROBANTES_ARCAs.FirstOrDefaultAsync(x => x.ID_COMPROBANTE_AFIP == id);
            return entity == null ? null : ComprobantesArcaMapper.ToVerification(entity);
        }
    }
}
