using MonsterLoots.Data;
using MonsterLoots.Models.Event;
using MonsterLoots.Models.History;
using MonsterLoots.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Services
{
    public class EventService
    {
        private readonly Guid _userId;

        public EventService(Guid userId)
        {
            _userId = userId;
        }

        public EventModel RandomLoot(EventModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var lootList = ctx.Loot.Where(e => e.OwnerId == _userId && e.MonsterId == model.MonsterId).ToList();
                var random = new Random();
                var lengthList = lootList.Count;
                var genNum = random.Next(0, lengthList);
                int getList = lootList[genNum].LootId;

                return new EventModel()
                {
                    MonsterId = model.MonsterId,
                    MonsterName = model.MonsterName,
                    LootId = getList,
                    LootName = lootList[genNum].LootName
                };
            }
        }

        public bool DeleteOldestEntry()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .History
                        .Single(e => e.OwnerId == _userId);
                ctx.History.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<HistoryListItem> GetHistory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .History
                    .Where(entity => entity.OwnerId == _userId)
                    .Select(
                        entity =>
                            new HistoryListItem
                            {
                                HistoryId = entity.HistoryId,
                                OwnerId = entity.OwnerId,
                                MonsterName = entity.MonsterName,
                                LootName = entity.LootName
                            }
                    );
                return query.OrderByDescending(e => e.HistoryId).ToList();
            }
        }
        public EventModel GetMonsterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Monsters
                        .Single(e => e.MonsterId == id && e.OwnerId == _userId);
                return
                    new EventModel
                    {
                        MonsterId = entity.MonsterId,
                        MonsterName = entity.MonsterName
                    };
            }
        }

        public bool AddHistory(EventModel model)
        {
            var entity =
                new History()
                {
                    OwnerId = _userId,
                    MonsterId = model.MonsterId,
                    MonsterName = model.MonsterName,
                    LootName = model.LootName

                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.History.Add(entity);
                return ctx.SaveChanges() == 1; 
            }
        }
    }
}
