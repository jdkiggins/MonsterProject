using MonsterLoots.Data;
using MonsterLoots.Models.Loot;
using MonsterLoots.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Services
{
    public class LootService
    {
        private readonly Guid _userId;
        public LootService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateLoot(LootCreate model)
        {
            var entity =
                new Loot()
                {
                    OwnerId = _userId,
                    LootName = model.LootName,
                    LootDesc = model.LootDesc,
                    MonsterId = model.MonsterId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Loot.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<LootListItem> GetLoot()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Loot
                    .Where(entity => entity.OwnerId == _userId)
                    .Select(
                        entity =>
                            new LootListItem
                            {
                                LootId = entity.LootId,
                                LootName = entity.LootName,
                                LootDesc = entity.LootDesc,
                                MonsterName = entity.Monsters.MonsterName

                            }
                    );
                return query.OrderBy(prod => prod.MonsterName).ToList();
            }
        }
        public LootDetails GetLootById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Loot
                        .Single(e => e.LootId == id && e.OwnerId == _userId);
                return
                    new LootDetails
                    {
                        LootId = entity.LootId,
                        LootName = entity.LootName,
                        LootDesc = entity.LootDesc,
                        MonsterId = entity.MonsterId,
                        MonsterName = entity.Monsters.MonsterName
                    };
            }
        }
        public bool UpdateLoot(LootEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Loot
                        .Single(e => e.LootId == model.LootId && e.OwnerId == _userId);

                entity.LootId = model.LootId;
                entity.LootName = model.LootName;
                entity.LootDesc = model.LootDesc;
                entity.MonsterId = model.MonsterId;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteLoot(int LootId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Loot
                        .Single(e => e.LootId == LootId && e.OwnerId == _userId);

                ctx.Loot.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
