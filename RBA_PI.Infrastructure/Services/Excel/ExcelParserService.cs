using ClosedXML.Excel;
using RBA_PI.Application.DTOs.ImportacionDatos;
using RBA_PI.Application.Services.Interfaces;
using RBA_PI.Infrastructure.Helpers;

namespace RBA_PI.Infrastructure.Services.Excel
{
    public class ExcelParserService : IExcelParserService
    {
        public List<ComprobanteArcaImportDto> Parse(string path, decimal? codCliente)
        {
            List<ComprobanteArcaImportDto> lista = [];

            using XLWorkbook wb = new(path);
            IXLWorksheet ws = wb.Worksheet(1);
            int row = 3;

            while (!ws.Cell(row, 1).IsEmpty())
            {
                string moneda = ws.Cell(row, 13).GetString().Trim();
                if (moneda != "$")
                {
                    row++;
                    continue;
                }

                lista.Add(new ComprobanteArcaImportDto
                {
                    Fecha = ExcelValueParser.GetDateOnly(ws.Cell(row, 1)),
                    Tipo = ws.Cell(row, 2).GetString(),
                    PtoVenta = ws.Cell(row, 3).GetValue<int>(),
                    NroComprobante = ws.Cell(row, 4).GetValue<int>(),
                    CodAutorizacion = ws.Cell(row, 6).GetString(),
                    TipoDoc = ws.Cell(row, 7).GetString(),
                    NroDocumento = ws.Cell(row, 8).GetString(),
                    RazonSocial = ws.Cell(row, 9).GetString(),
                    Moneda = moneda,
                    TipoCambio = ExcelValueParser.GetDecimal(ws.Cell(row, 12)),
                    Neto_0 = ExcelValueParser.GetDecimal(ws.Cell(row, 14)),
                    Iva_25 = ExcelValueParser.GetDecimal(ws.Cell(row, 15)),
                    Neto_25 = ExcelValueParser.GetDecimal(ws.Cell(row, 16)),
                    Iva_5 = ExcelValueParser.GetDecimal(ws.Cell(row, 17)),
                    Neto_5 = ExcelValueParser.GetDecimal(ws.Cell(row, 18)),
                    Iva_105 = ExcelValueParser.GetDecimal(ws.Cell(row, 19)),
                    Neto_105 = ExcelValueParser.GetDecimal(ws.Cell(row, 20)),
                    Iva_21 = ExcelValueParser.GetDecimal(ws.Cell(row, 21)),
                    Neto_21 = ExcelValueParser.GetDecimal(ws.Cell(row, 22)),
                    Iva_27 = ExcelValueParser.GetDecimal(ws.Cell(row, 23)),
                    Neto_27 = ExcelValueParser.GetDecimal(ws.Cell(row, 24)),
                    ImpNeto = ExcelValueParser.GetDecimal(ws.Cell(row, 25)),
                    ImpNoGravado = ExcelValueParser.GetDecimal(ws.Cell(row, 26)),
                    ImpExento = ExcelValueParser.GetDecimal(ws.Cell(row, 27)),
                    OtrosTributos = ExcelValueParser.GetDecimal(ws.Cell(row, 28)),
                    Iva = ExcelValueParser.GetDecimal(ws.Cell(row, 29)),
                    ImpTotal = ExcelValueParser.GetDecimal(ws.Cell(row, 30)),
                    CodCliente = codCliente
                });
                row++;
            }
            return lista;
        }
    }
}
