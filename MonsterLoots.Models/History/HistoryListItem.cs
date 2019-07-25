using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Models.History
{
    public class HistoryListItem
    {
        public int HistoryId { get; set; }
        public Guid OwnerId { get; set; }
        [Display(Name = "Monster: ")]
        public string MonsterName { get; set; }
        public string LootName { get; set; }
    }
}
