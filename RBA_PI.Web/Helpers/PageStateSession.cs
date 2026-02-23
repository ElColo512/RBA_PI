using RBA_PI.Application.DTOs;
using System.Text.Json;

namespace RBA_PI.Web.Helpers
{
    internal static class PageStateSession
    {
        internal static void Save<T>(ISession session, string key, T filters)
        {
            PageState<T> state = new()
            {
                Filters = filters,
            };

            session.SetString(key, JsonSerializer.Serialize(state));
        }

        internal static PageState<T>? Load<T>(ISession session, string key)
        {
            string? json = session.GetString(key);
            return json == null ? null : JsonSerializer.Deserialize<PageState<T>>(json);
        }
    }
}
