using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Data
{
    public class Loot
    {
        [Key]
        public int LootId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Loot: ")]
        public string LootName { get; set; }
        [Required]
        [MaxLength(400, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Description: ")]
        public string LootDesc { get; set; }

        [ForeignKey(nameof(Monsters))]
        public int MonsterId { get; set; }
        public virtual Monsters Monsters { get; set; }
    }
}
