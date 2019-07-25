using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Models.Monsters
{
    public class MonstersEdit
    {
        public int MonsterId { get; set; }
        [Display(Name = "Monster Name: ")]
        public string MonsterName { get; set; }
        [Display(Name = "Description: ")]
        public string MonsterDesc { get; set; }
        [Display(Name = "Level: ")]
        public short MonsterLevel { get; set; }
    }
}
