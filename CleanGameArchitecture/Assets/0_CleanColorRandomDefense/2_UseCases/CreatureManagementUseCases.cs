using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CreatureEntities;
using RuleEntities;
using UnityEngine;
using System;

public interface IPositionGetter
{
    Vector3 Position { get; }
}

namespace CreatureManagementUseCases
{
    public class UnitSpanwer
    {
        UnitManager _unitManager;
        UnitSpawnRule _unitSpawnRule;
        public UnitSpanwer(UnitManager unitManager, UnitSpawnRule unitSpawnRule) 
            => (_unitManager, _unitSpawnRule) = (unitManager, unitSpawnRule);

        public bool TrySpawn(UnitFlags flag, out Unit unit)
        {
            if (_unitSpawnRule.IsMaxCount(_unitManager.Units.Count))
            {
                unit = null;
                return false;
            }

            unit = new Unit(flag);
            return true;
        }
    }

    public class MonsterSpawner
    {
        public Monster SpawnMonster(int hp, IPositionGetter positionGetter = null) => new Monster(hp, positionGetter);
    }

    public class UnitManager
    {
        List<Unit> _units = new List<Unit>();
        public IReadOnlyList<Unit> Units => _units;

        public void AddUnit(Unit unit)
        {
            _units.Add(unit);
            unit.OnDead += RemoveUnit;
        }

        void RemoveUnit(Unit unit) => _units.Remove(unit);

        public bool TryFindUnit(UnitFlags flag, out Unit unit)
        {
            unit = _units.FirstOrDefault(x => x.Flag == flag);
            return unit != null;
        }
    }

    public struct CombineCondition
    {
        public UnitFlags Flag { get; private set; }
        public int NeedCount { get; private set; }

        public CombineCondition(UnitFlags flag, int needCount)
        {
            Flag = flag;
            NeedCount = needCount;
        }
    }

    public class UnitCombiner
    {
        Dictionary<UnitFlags, CombineCondition[]> _flagBycCombineConditions;
        UnitManager _manager;
        public UnitCombiner(Dictionary<UnitFlags, CombineCondition[]> conditons, UnitManager manager)
        {
            _flagBycCombineConditions = conditons;
            _manager = manager;
        }

        public bool TryCombine(UnitFlags tryCombineFlag, out Unit combineUnit)
        {
            foreach (var condition in _flagBycCombineConditions[tryCombineFlag])
            {
                if(_manager.Units.Where(x => x.Flag == condition.Flag).Count() < condition.NeedCount)
                {
                    combineUnit = null;
                    return false;
                }
            }

            foreach (var condition in _flagBycCombineConditions[tryCombineFlag])
            {
                for (int i = 0; i < condition.NeedCount; i++)
                {
                    _manager.TryFindUnit(condition.Flag, out Unit unit);
                    unit.Dead();
                }
            }

            combineUnit = new Unit(tryCombineFlag);
            _manager.AddUnit(combineUnit);
            return true;
        }
    }

    public class MonsterManager
    {
        List<Monster> _monsters = new List<Monster>();
        public IReadOnlyList<Monster> Monsters => _monsters;
        public event Action<int> OnMonsterCountChanged;

        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            OnMonsterCountChanged?.Invoke(_monsters.Count);
            monster.OnDead += RemoveMonster;
        }

        void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster); 
            OnMonsterCountChanged?.Invoke(_monsters.Count);
        }

        public Monster FindProximateMonster(IPositionGetter positionGetter) => FindProximateMonster(positionGetter.Position);

        public Monster FindProximateMonster(Vector3 requesterPos)
        {
            if (_monsters.Count == 0) return null;
            (float minDistance, Monster monster) minDistanceMonster = (Mathf.Infinity, null);
            foreach (var monster in _monsters)
            {
                float distanceToEnemy = Vector3.Distance(requesterPos, monster.PositionGetter.Position);
                if (distanceToEnemy < minDistanceMonster.minDistance)
                    minDistanceMonster = (distanceToEnemy, monster);
            }
            return minDistanceMonster.monster;
        }
    }
}