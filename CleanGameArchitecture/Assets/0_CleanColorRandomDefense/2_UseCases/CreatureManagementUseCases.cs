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
        UnitCountRule _unitCountRule;
        UnitManager _unitManager;
        public UnitSpanwer(UnitCountRule unitCountRule, UnitManager unitManager)
        {
            _unitCountRule = unitCountRule;
            _unitManager = unitManager;
        }

        public bool TrySpawn(UnitFlags flag, out Unit unit)
        {
            if (_unitCountRule.CheckFullUnit(_unitManager.Units.Count))
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
        UnitCountRule _unitCountRule;
        public UnitManager(UnitCountRule unitCountRule)
        {
            _unitCountRule = unitCountRule;
        }

        public UnitManager()
        {
        }

        List<Unit> _units = new List<Unit>();
        public IReadOnlyList<Unit> Units => _units;
        public Unit Spawn(UnitFlags flag)
        {
            var unit = new Unit(flag);
            _units.Add(unit);
            unit.OnDead += RemoveUnit;
            return unit;
        }

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

    public class MonsterManager
    {
        List<Monster> _monsters = new List<Monster>();
        public IReadOnlyList<Monster> Monsters => _monsters;
        public event Action<int> OnMonsterCountChanged;
        public Monster Spawn(int hp, IPositionGetter positionGetter = null)
        {
            var monster = new Monster(hp, positionGetter);
            _monsters.Add(monster);
            OnMonsterCountChanged?.Invoke(_monsters.Count);
            monster.OnDead += RemoveMonster;
            return monster;
        }

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