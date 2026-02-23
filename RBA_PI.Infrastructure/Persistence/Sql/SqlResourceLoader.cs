using System.Reflection;

namespace RBA_PI.Infrastructure.Persistence.Sql
{
    internal class SqlResourceLoader
    {
        internal static string Load(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException($"No se encontró el recurso SQL: {resourceName}");
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
