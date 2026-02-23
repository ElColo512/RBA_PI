using Microsoft.EntityFrameworkCore;
using RBA_PI.Infrastructure.Persistence;
using RBA_PI.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace RBA_PI.Infrastructure.Repositories.Implementations
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context;

        public async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, IDictionary<string, object?> fields)
        {
            if (fields == null || fields.Count == 0)
                return 0;

            List<T> entities = await _context.Set<T>().Where(predicate).ToListAsync();

            if (entities.Count == 0)
                return 0;

            Dictionary<string, PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToDictionary(static p => p.Name);

            foreach (var entity in entities)
            {
                foreach (var field in fields)
                {
                    if (!properties.TryGetValue(field.Key, out var property))
                        throw new InvalidOperationException($"La propiedad '{field.Key}' no existe en '{typeof(T).Name}'");

                    if (!property.CanWrite)
                        throw new InvalidOperationException($"La propiedad '{field.Key}' es de solo lectura");

                    Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    object? convertedValue = field.Value == null ? null : Convert.ChangeType(field.Value, targetType);

                    property.SetValue(entity, convertedValue);
                }
            }
            return await _context.SaveChangesAsync();
        }
    }
}
