using Microsoft.AspNetCore.Mvc.Rendering;
using RBA_PI.Application.DTOs;

namespace RBA_PI.Web.Helpers
{
    internal static class SelectItemHelper
    {
        internal static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<SelectItemDto> items)
        {
            return items.Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Text
            });
        }
    }
}
