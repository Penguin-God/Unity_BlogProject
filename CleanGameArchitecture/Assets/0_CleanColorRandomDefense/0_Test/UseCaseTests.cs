using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureEntities;

namespace UseCaseTests
{
    public class CreatureManagerTester
    {
        public void TestUnitManagement()
        {
            var manager = new UnitManager(new RuleEntities.UnitCountRule(15));
            var spawnFlag = new UnitFlags(0, 0);
            var unit = manager.Spawn(spawnFlag);
            Debug.Assert(manager.Units[0] == unit);
            Debug.Assert(manager.TryFindUnit(spawnFlag, out Unit findUnit));
            Debug.Assert(findUnit == unit);

            unit.Dead();
            Debug.Assert(manager.Units.Count == 0);
            Debug.Assert(manager.TryFindUnit(spawnFlag, out Unit nullUnit) == false);
            Debug.Assert(nullUnit == null);

            for (int i = 0; i < 14; i++)
                manager.Spawn(spawnFlag);
            Debug.Assert(manager.TrySpawn(spawnFlag, out unit));
            Debug.Assert(manager.TrySpawn(spawnFlag, out unit) == false);
            Debug.Log("유닛 소환 통과!!");
        }

        public void TestMonsterManagement()
        {
            var manager = new MonsterManager();
            var monster = manager.Spawn(1000);
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
                manager.Spawn(1000, new TestPositionGetter(Vector3.one * i));
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
