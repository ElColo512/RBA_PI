namespace RBA_PI.Application.Helpers
{
    public class FacturaNormalizer
    {
        public static string Normalizar(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string[] tipos = ["FAC", "N/C"];

            string result = input.Trim();

            foreach (var tipo in tipos)
            {
                result = result.Replace(tipo, "", StringComparison.OrdinalIgnoreCase);
            }

            return result.Replace(" ", "").Replace("-", "").ToUpperInvariant();
        }
    }
}
