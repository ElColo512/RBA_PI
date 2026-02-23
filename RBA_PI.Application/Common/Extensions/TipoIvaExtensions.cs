using RBA_PI.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RBA_PI.Application.Common.Extensions
{
    public static class TipoIvaExtensions
    {
        public static string Porcentaje(this TipoIva tipo)
        {
            MemberInfo? member = tipo.GetType().GetMember(tipo.ToString()).FirstOrDefault();
            DisplayAttribute? attr = member?.GetCustomAttribute<DisplayAttribute>();
            return attr?.Description ?? string.Empty;
        }
    }
}
