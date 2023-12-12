using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataBase.Models
{
    [Table("characters")]
    public class Character
    {
        [Key]
        [Required(ErrorMessage = "character id is required")]
        [Column("character_id")]
        public int CharacterId { get; set; }

        [Required(ErrorMessage = "account id is required")]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "position is required")]
        [Column("position")]
        public int Position { get; set; }

        [Required(ErrorMessage = "name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "class id is required")]
        [Column("class_id")]
        public int ClassId { get; set; }

        [Column("base_level")]
        public int BaseLevel { get; set; }

        [Column("job_level")]
        public int JobLevel { get; set; }

        [Column("base_exp")]
        public int BaseExp { get; set; }

        [Column("job_exp")]
        public int JobExp { get; set; }

        [Column("zeny")]
        public float Zeny { get; set; }

        [Column("is_online")]
        public bool IsOnline { get; set; }
    }
}
