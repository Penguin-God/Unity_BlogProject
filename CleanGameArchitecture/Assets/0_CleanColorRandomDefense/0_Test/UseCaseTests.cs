using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureEntities;
using RuleEntities;

namespace UseCaseTests
{
    public class CreatureManagerTester : Testing
    {
        public void TestUnitManagement()
        {
            var manager = new UnitManager(new UnitCountRule(15));
            var spawnFlag = new UnitFlags(0, 0);
            var unit = manager.Spawn(spawnFlag);
            Assert(manager.Units[0] == unit);
            Assert(manager.TryFindUnit(spawnFlag, out Unit findUnit));
            Assert(findUnit == unit);

            unit.Dead();
            Assert(manager.Units.Count == 0);
            Assert(manager.TryFindUnit(spawnFlag, out Unit nullUnit) == false);
            Assert(nullUnit == null);

            for (int i = 0; i < 14; i++)
                manager.Spawn(spawnFlag);
            Assert(manager.TrySpawn(spawnFlag, out unit));
            Assert(manager.TrySpawn(spawnFlag, out unit) == false);
            Log("유닛 소환 통과!!");
        }

        public void TestMonsterManagement()
        {
            var manager = new MonsterManager();
            var monster = manager.Spawn(1000);
            Assert(manager.Monsters[0] == monster);

            monster.OnDamage(monster.CurrentHp);
            Assert(manager.Monsters.Count == 0);
            Log("Pass Monster Spawn And Dead!!");
        }

        public void TestBattleLoss()
        {
            bool isLoss = false;
            var manager = new MonsterManager();
            var rule = new BattleRule(50);
            manager.OnMonsterCountChanged += (count) => { if (rule.CheckLoss(count)) isLoss = true; };
            MonsterSpawn(manager, 30);
            Assert(isLoss == false);

            MonsterSpawn(manager, 20);
            Assert(isLoss);
            Log("게임 패배 통과!!");
        }

        void MonsterSpawn(MonsterManager manager, int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
                manager.Spawn(1000);
        }

        public void TestFindMonster()
        {
            var manager = new MonsterManager();
            Unit unit = new Unit(new UnitFlags(0, 0), new TestPositionGetter(Vector3.zero));

            for (int i = 0; i < 20; i++)
                manager.Spawn(1000, new TestPositionGetter(Vector3.one * i));
            var findFirstMonster = manager.FindProximateMonster(unit.PositionGetter);
            Assert(findFirstMonster.PositionGetter.Position == Vector3.zero);
            findFirstMonster.OnDamage(1000000);

            var findSecondMonster = manager.FindProximateMonster(unit.PositionGetter);
            Assert(findSecondMonster.PositionGetter.Position == Vector3.one);
            Log("몬스터 찾기 통과!!");
        }
    }

    class TestPositionGetter : IPositionGetter
    {
        Vector3 _pos;
        public TestPositionGetter(Vector3 pos) => _pos = pos;

        public Vector3 Position => _pos;
    }
}
