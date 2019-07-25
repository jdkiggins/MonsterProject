using MonsterLoots.Data;
using MonsterLoots.Models.Monsters;
using MonsterLoots.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterLoots.Services
{
    public class MonstersService
    {
        private readonly Guid _userId;
        public MonstersService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMonster(MonstersCreate model)
        {
            var entity =
                new Monsters()
                {
                    OwnerId = _userId,
                    MonsterName = model.MonsterName,
                    MonsterDesc = model.MonsterDesc,
                    MonsterLevel = short.Parse(model.MonsterLevel)
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Monsters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MonstersListItem> GetMonsters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Monsters
                    .Where(entity => entity.OwnerId == _userId)
                    .Select(
                        entity =>
                            new MonstersListItem
                            {
                                MonsterId = entity.MonsterId,
                                MonsterName = entity.MonsterName,
                                MonsterDesc = entity.MonsterDesc,
                                MonsterLevel = entity.MonsterLevel
                            }
                    );
                return query.OrderBy(prod => prod.MonsterName).ToArray();

            }
        }
        public MonstersDetails GetMonsterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Monsters
                        .Single(e => e.MonsterId == id && e.OwnerId == _userId);
                return
                    new MonstersDetails
                    {
                        MonsterId = entity.MonsterId,
                        MonsterName = entity.MonsterName,
                        MonsterDesc = entity.MonsterDesc,
                        MonsterLevel = entity.MonsterLevel
                    };
            }
        }
        public bool UpdateMonster(MonstersEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Monsters
                        .Single(e => e.MonsterId == model.MonsterId && e.OwnerId == _userId);

                entity.MonsterId = model.MonsterId;
                entity.MonsterName = model.MonsterName;
                entity.MonsterDesc = model.MonsterDesc;
                entity.MonsterLevel = model.MonsterLevel;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMonster(int MonsterId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Monsters
                        .Single(e => e.MonsterId == MonsterId && e.OwnerId == _userId);

                ctx.Monsters.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
