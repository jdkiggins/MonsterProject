using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Data
{
    public class History
    {
        [Key]
        public int HistoryId { get; set; }
        public Guid OwnerId { get; set; }
        public int MonsterId { get; set; }
        [Display(Name = "Monster")]
        public string MonsterName { get; set; }
        [Display(Name = "Loot")]
        public string LootName { get; set; }

    }
}
