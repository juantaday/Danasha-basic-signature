using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Domain.Helpers
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
        }

        public static bool ContainsLetters(string text)
        {
            return Regex.Match(text, @"^[a-zA-Z]").Success;
        }//!\"·$%&/()=¿¡?'_:;,|@#€*+.

        public static bool ContainsCahracterEcpeciasl(string text)
        {
            return Regex.Match(text, @"[!\""·$%&/()=¿¡?'_:;,|@#€*+.]").Success;
        }

        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? value.ToString();
        }


    }
}
