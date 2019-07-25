using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Models.Event
{
    public class EventModel
    {
        public int MonsterId { get; set; }
        [Display(Name = "Monster")]
        public string MonsterName { get; set; }
        public int LootId { get; set; }
        [Display(Name = "Loot")]
        public string LootName { get; set; }
    }
}
