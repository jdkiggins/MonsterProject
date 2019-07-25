using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Data
{
    public class Monsters
    {
        [Key]
        public int MonsterId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "There are too many characters in this field.")]
        public string MonsterName { get; set; }
        [Required]
        [MaxLength(400, ErrorMessage = "There are too many characters in this field.")]
        public string MonsterDesc { get; set; }
        [Required]
        [Range(1, 200, ErrorMessage = "Please choose a number between 1 and 200")]
        public short MonsterLevel { get; set; }
    }
}
