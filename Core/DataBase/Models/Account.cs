using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataBase.Models
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        [Required(ErrorMessage = "account id is required")]
        [Display(Name = "Account ID")]
        [Column("account_id")]
        public int AccountId { get; set; }


        [Required(ErrorMessage = "UserName is required")]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        [Column("username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        [Column("password")]
        public string Password { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
    }
}
