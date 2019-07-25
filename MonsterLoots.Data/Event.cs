using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Data
{
    public class Event
    {
        [Key]
        public Guid OwnerId { get; set; }

        [Required]
        [ForeignKey(nameof(Monsters))]
        public int MonsterId { get; set; }
        public virtual Monsters Monsters { get; set; }

        [ForeignKey(nameof(Loot))]
        public int LootId { get; set; }
        public virtual Loot Loot { get; set; }
    }
}
