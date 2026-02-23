using ClosedXML.Excel;
using RBA_PI.Application.Helpers;
using RBA_PI.Application.Services.Interfaces;

namespace RBA_PI.Infrastructure.Services.Excel
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] Export<T>(IEnumerable<T> data, string sheetName, IReadOnlyList<ExcelColumn<T>> columns)
        {
            using XLWorkbook wb = new();
            IXLWorksheet ws = wb.Worksheets.Add(sheetName);

            for (int i = 0; i < columns.Count; i++)
            {
                ws.Cell(1, i + 1).Value = columns[i].Header;
            }

            ws.Range(1, 1, 1, columns.Count).Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightGray)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < columns.Count; col++)
                {
                    IXLCell cell = ws.Cell(row, col + 1);
                    object? value = columns[col].Value(item);
                    SetCellValue(cell, value);

                    if (!string.IsNullOrWhiteSpace(columns[col].NumberFormat))
                    {
                        cell.Style.NumberFormat.Format = columns[col].NumberFormat!;
                    }
                }
                row++;
            }

            ws.Columns().AdjustToContents();

            using MemoryStream ms = new();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        private static void SetCellValue(IXLCell cell, object? value)
        {
            if (value == null)
                return;

            switch (value)
            {
                case DateOnly d:
                    cell.SetValue(d.ToDateTime(TimeOnly.MinValue));
                    break;

                case DateTime dt:
                    cell.SetValue(dt);
                    break;

                case decimal dec:
                    cell.SetValue(dec);
                    break;

                case int i:
                    cell.SetValue(i);
                    break;

                case long l:
                    cell.SetValue(l);
                    break;

                case double db:
                    cell.SetValue(db);
                    break;

                case bool b:
                    cell.SetValue(b);
                    break;

                default:
                    cell.SetValue(value.ToString());
                    break;
            }
        }
    }
}
