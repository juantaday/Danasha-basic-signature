using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainSQLite.Models
{
    public class Conection
    {
        [Key]
        public int Id { get; set; }

        public string NameDatabase { get; set; }

        public string IpConection { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string FilePath { get; set; }

    }
}
