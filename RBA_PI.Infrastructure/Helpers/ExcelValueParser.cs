using ClosedXML.Excel;
using System.Globalization;

namespace RBA_PI.Infrastructure.Helpers
{
    internal class ExcelValueParser
    {
        internal static decimal GetDecimal(IXLCell cell)
        {
            if (cell.IsEmpty())
                return 0m;

            // Caso 1: Excel ya lo guarda como número
            if (cell.DataType == XLDataType.Number)
                return cell.GetDouble() != 0 ? Convert.ToDecimal(cell.GetDouble()) : 0m;

            // Caso 2: Texto (ej: "$1.234,56")
            string raw = cell.GetString().Replace("$", "").Replace("USD", "").Trim();
            raw = raw.Replace(".", "").Replace(" ", "");
            raw = raw.Replace(",", ".");

            if (decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                return value;

            throw new FormatException($"No se pudo convertir a decimal: '{cell.GetString()}'");
        }

        internal static DateOnly GetDateOnly(IXLCell cell)
        {
            if (cell.IsEmpty())
                throw new Exception("La celda de fecha está vacía.");

            // Caso 1: Excel la reconoce como DateTime
            if (cell.DataType == XLDataType.DateTime)
                return DateOnly.FromDateTime(cell.GetDateTime());

            // Caso 2: Excel la guarda como número (serial)
            if (cell.DataType == XLDataType.Number)
                return DateOnly.FromDateTime(DateTime.FromOADate(cell.GetDouble()));

            // Caso 3: viene como texto
            string raw = cell.GetString().Trim();

            if (DateTime.TryParse(raw, out var dt))
                return DateOnly.FromDateTime(dt);

            throw new FormatException($"Fecha inválida: '{raw}'");
        }
    }
}
