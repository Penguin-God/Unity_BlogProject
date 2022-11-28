using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureEntities;

namespace UseCaseTests
{
    public class CreatureManagerTester
    {
        public void TestUnitSpawnAndDead()
        {
            var manager = new CreatureManagementUseCases.UnitManager();
            var spawnFlag = new UnitFlags(0, 0);
            var unit = manager.Spawn(spawnFlag);
            Debug.Assert(manager.Units[0] == unit);
            Debug.Assert(manager.TryGetUnit(spawnFlag, out Unit findUnit));
            Debug.Assert(findUnit == unit);

            unit.Dead();
            Debug.Assert(manager.Units.Count == 0);
            Debug.Assert(manager.TryGetUnit(spawnFlag, out Unit nullUnit) == false);
            Debug.Assert(nullUnit == null);
            Debug.Log("Pass Unit Spawn And Dead!!");
        }

        public void TestMonsterDead()
        {

        }
    }
}
