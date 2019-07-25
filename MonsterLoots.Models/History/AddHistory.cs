using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Models.History
{
    public class AddHistory
    {
        public int MonsterId { get; set; }
        public int LootId { get; set; }
        public string LootName { get; set; }
    }
}
