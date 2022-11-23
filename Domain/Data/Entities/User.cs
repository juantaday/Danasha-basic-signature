using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Entities
{
    public class User :IdentityUser<string>
    {
    }
}
