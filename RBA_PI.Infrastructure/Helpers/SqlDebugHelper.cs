using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RBA_PI.Infrastructure.Helpers
{
    internal static class SqlDebugHelper
    {
        public static string GenerarSqlConDeclares(SqlCommand cmd, string sqlOriginal)
        {
            StringBuilder sb = new();

            sb.AppendLine("/* ========= DECLARACIÓN DE PARÁMETROS ========= */");
            sb.AppendLine();

            foreach (SqlParameter p in cmd.Parameters)
            {
                string tipoSql = ObtenerTipoSql(p);
                string valor = FormatearValorSql(p.Value);

                sb.AppendLine($"DECLARE {p.ParameterName} {tipoSql};");
                sb.AppendLine($"SET {p.ParameterName} = {valor};");
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("/* ========= SCRIPT ORIGINAL ========= */");
            sb.AppendLine();
            sb.AppendLine(sqlOriginal);

            return sb.ToString();
        }

        private static string ObtenerTipoSql(SqlParameter param)
        {
            return param.SqlDbType switch
            {
                SqlDbType.VarChar => $"VARCHAR({param.Size})",
                SqlDbType.Decimal => "DECIMAL(18,2)",
                SqlDbType.Int => "INT",
                SqlDbType.BigInt => "BIGINT",
                SqlDbType.DateTime => "DATETIME",
                _ => param.SqlDbType.ToString().ToUpper()
            };
        }

        private static string FormatearValorSql(object? value)
        {
            if (value == null || value == DBNull.Value) return "NULL";

            return value switch
            {
                string s => $"'{s.Replace("'", "''")}'",
                decimal d => d.ToString(System.Globalization.CultureInfo.InvariantCulture),
                double d => d.ToString(System.Globalization.CultureInfo.InvariantCulture),
                float f => f.ToString(System.Globalization.CultureInfo.InvariantCulture),
                bool b => b ? "1" : "0",
                _ => value.ToString() ?? "NULL"
            };
        }
    }
}
