using RBA_PI.Application.DTOs.ImportacionDatos;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IExcelParserService
    {
        List<ComprobanteArcaImportDto> Parse(string path, decimal? codCliente);
    }
}
