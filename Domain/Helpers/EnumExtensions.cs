using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Domain.Helpers
{
    public static class EnumExtensions
    {
        public static T? FromDisplayName<T>(string displayName) where T : struct, Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                var attr = field.GetCustomAttribute<DisplayAttribute>();

                if (attr?.Name?.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                    return (T)field.GetValue(null);

                // Fallback: nombre directo del enum
                if (field.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase))
                    return (T)field.GetValue(null);
            }
            return null;
        }

        // Extra útil: obtener el Display Name desde el enum
        public static string ToDisplayName<T>(this T value) where T : struct, Enum
        {
            return typeof(T)
                .GetField(value.ToString())
                ?.GetCustomAttribute<DisplayAttribute>()
                ?.Name;
        }

        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? value.ToString();
        }
    }
}