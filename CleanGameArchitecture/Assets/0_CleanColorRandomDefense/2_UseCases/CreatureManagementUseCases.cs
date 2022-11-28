using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CreatureEntities;

namespace CreatureManagementUseCases
{
    public class UnitManager
    {
        List<Unit> _units = new List<Unit>();
        public IReadOnlyList<Unit> Units => _units;
        public Unit Spawn(UnitFlags flag)
        {
            var unit = new Knight(flag);
            _units.Add(unit);
            unit.OnDead += RemoveUnit;
            return unit;
        }

        void RemoveUnit(Unit unit) => _units.Remove(unit);

        public bool TryGetUnit(UnitFlags flag, out Unit unit)
        {
            var units = _units.Where(x => x.Flag == flag).ToArray();
            if(units.Length == 0)
            {
                unit = null;
                return false;
            }
            else
            {
                unit = units[0];
                return true;
            }
        }
    }

    public class MonsterManager
    {
        List<Monster> _monsters = new List<Monster>();
        public IReadOnlyList<Monster> Monsters => _monsters;
        public Monster Spawn()
        {
            var monster = new NormalMonster(1000);
            _monsters.Add(monster);
            monster.OnDead += RemoveMonster;
            return monster;
        }

        void RemoveMonster(Monster monster) => _monsters.Remove(monster);
    }
}