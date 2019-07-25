using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Models.Loot
{
    public class LootListItem
    {
        public int LootId { get; set; }
        [Display(Name = "Item")]
        public string LootName { get; set; }
        [Display(Name = "Description")]
        public string LootDesc { get; set; }
        [Display(Name = "Monster")]
        public string MonsterName { get; set; }
    }
}
