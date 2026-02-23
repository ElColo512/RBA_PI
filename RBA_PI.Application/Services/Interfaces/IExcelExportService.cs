using RBA_PI.Application.Helpers;

namespace RBA_PI.Application.Services.Interfaces
{
    public interface IExcelExportService
    {
        byte[] Export<T>(IEnumerable<T> data, string sheetName, IReadOnlyList<ExcelColumn<T>> columns);
    }
}
