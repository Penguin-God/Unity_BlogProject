using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CreatureEntities;
using RuleEntities;
using UnityEngine;

public interface IPositionGetter
{
    Vector3 Position { get; }
}

namespace CreatureManagementUseCases
{
    public class UnitManager
    {
        public UnitManager() // TODO : Rule 받아와서 Spawn시 조건 검사하기
        {

        }

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
        public Monster Spawn(IPositionGetter positionGetter = null)
        {
            var monster = new Monster(1000, positionGetter);
            _monsters.Add(monster);
            monster.OnDead += RemoveMonster;
            return monster;
        }

        void RemoveMonster(Monster monster) => _monsters.Remove(monster);

        public Monster FindProximateMonster(IPositionGetter positionGetter)
        {
            if (_monsters.Count == 0) return null;
            (float minDistance, Monster monster) minDistanceMonster = (Mathf.Infinity, null);
            foreach (var monster in _monsters)
            {
                float distanceToEnemy = Vector3.Distance(positionGetter.Position, monster.PositionGetter.Position);
                if (distanceToEnemy < minDistanceMonster.minDistance)
                    minDistanceMonster = (distanceToEnemy, monster);
            }
            return minDistanceMonster.monster;
        }
    }
}