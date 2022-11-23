using Domain.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{

    [Table ("MySetting")]
    public class MySetting
    {
        [Key]
        public int MySettingID { get; set; }

        public int MyCommerceId { get; set; }

        [Column(TypeName = "varbinary(max)")]
        [Required]
        public byte[] ImageLogo { get; set; }

        [Required]
        [StringLength (100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string SMTP { get; set; }

        [Required]
        [StringLength(10)]
        public string Port { get; set; }

        [StringLength(30)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string CellPhone { get; set; }

        public virtual MyCommerce MyCommerce { get; set; }


    }

}
