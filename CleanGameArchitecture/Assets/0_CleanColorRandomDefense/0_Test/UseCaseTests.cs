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
            var manager = new UnitManager();
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

        public void TestMonsterSpawnAndDead()
        {
            var manager = new MonsterManager();
            var monster = manager.Spawn();
            Debug.Assert(manager.Monsters[0] == monster);

            monster.OnDamage(monster.CurrentHp);
            Debug.Assert(manager.Monsters.Count == 0);
            Debug.Log("Pass Monster Spawn And Dead!!");
        }

        public void TestFindMonster()
        {
            var manager = new MonsterManager();
            Unit unit = new Unit(new UnitFlags(0, 0), new TestPositionGetter(Vector3.zero));

            for (int i = 0; i < 20; i++)
                manager.Spawn(new TestPositionGetter(Vector3.one * i));
            var findMonster = manager.FindProximateMonster(unit.PositionGetter);
            Debug.Assert(findMonster.PositionGetter.Position == Vector3.zero);
            findMonster.OnDamage(1000000);
            var findSecondMonster = manager.FindProximateMonster(unit.PositionGetter);
            Debug.Assert(findSecondMonster.PositionGetter.Position == Vector3.one);
            Debug.Log("몬스터 찾기 통과!!");
        }
    }

    class TestPositionGetter : IPositionGetter
    {
        Vector3 _pos;
        public TestPositionGetter(Vector3 pos) => _pos = pos;

        public Vector3 Position => _pos;
    }
}
