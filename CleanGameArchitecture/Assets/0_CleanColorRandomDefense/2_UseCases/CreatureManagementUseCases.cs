﻿using System.Collections;
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
        public void Spawn()
        {

        }
    }
}