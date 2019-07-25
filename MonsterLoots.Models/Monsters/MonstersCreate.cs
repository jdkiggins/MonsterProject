using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Models.Monsters
{
    public class MonstersCreate
    {
        [Required]
        [Display(Name = "Monster Name:")]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(30, ErrorMessage = "There are too many characters in this field.")]
        public string MonsterName { get; set; }
        [Required]
        [MaxLength(400, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Description:")]
        public string MonsterDesc { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "Error: Maxium level is 9999")]
        [Display(Name = "Level:")]
        public string MonsterLevel { get; set; }
    }
}
