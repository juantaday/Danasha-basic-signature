using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Helpers
{
    public static  class Utilities
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

    }
}
