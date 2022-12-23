using System.Collections;
using System.Collections.Generic;
using CreatureEntities;
using RuleEntities;
using UnitUseCases;
using UnityEngine;
using System;
using System.Linq;

public interface IMaxCountRule
{
    bool IsMaxCount(int count);
}

namespace CreatureManagementUseCases
{
    public class UnitSpanwer
    {
        UnitManager _unitManager;
        IMaxCountRule _countRule;
        public UnitSpanwer(UnitManager unitManager, IMaxCountRule countRule) 
            => (_unitManager, _countRule) = (unitManager, countRule);

        public bool TrySpawn(UnitFlags flag, out Unit unit)
        {
            if (_countRule.IsMaxCount(_unitManager.Units.Count))
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
        public static Monster SpawnMonster(int hp) => new Monster(hp);
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
        Dictionary<UnitFlags, CombineCondition[]> _flagByCombineConditions;
        UnitManager _manager;
        public UnitCombiner(Dictionary<UnitFlags, CombineCondition[]> conditons, UnitManager manager)
        {
            _flagByCombineConditions = conditons;
            _manager = manager;
        }

        public bool TryCombine(UnitFlags tryCombineFlag, out Unit combineUnit)
        {
            foreach (var condition in _flagByCombineConditions[tryCombineFlag])
            {
                if(_manager.Units.Where(x => x.Flag == condition.Flag).Count() < condition.NeedCount)
                {
                    combineUnit = null;
                    return false;
                }
            }

            foreach (var condition in _flagByCombineConditions[tryCombineFlag])
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
    }
}