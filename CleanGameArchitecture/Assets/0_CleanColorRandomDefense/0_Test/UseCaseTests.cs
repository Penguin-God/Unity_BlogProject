using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureEntities;
using RuleEntities;
using CreatureUseCase;
using static UnityEngine.Debug;

namespace UseCaseTests
{
    public class CreatureManagerTester
    {
        public void TestUnitManagement()
        {
            Log("유닛 매니저 테스트!!");
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
        }

        public void TestMonsterManagement()
        {
            Log("몬스터 매니저 테스트!!");
            var manager = new MonsterManager();
            var monster = manager.Spawn(1000);
            Assert(manager.Monsters[0] == monster);

            monster.OnDamage(monster.CurrentHp);
            Assert(manager.Monsters.Count == 0);
        }

        public void TestBattleLoss()
        {
            Log("게임 패배 테스트!!");
            bool isLoss = false;
            var manager = new MonsterManager();
            var rule = new BattleRule(50);
            manager.OnMonsterCountChanged += (count) => { if (rule.CheckLoss(count)) isLoss = true; };
            MonsterSpawn(manager, 30);
            Assert(isLoss == false);

            MonsterSpawn(manager, 20);
            Assert(isLoss);
        }

        void MonsterSpawn(MonsterManager manager, int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
                manager.Spawn(1000);
        }

        public void TestFindMonster()
        {
            Log("몬스터 찾기 테스트!!");
            var manager = new MonsterManager();
            Unit unit = new Unit(new UnitFlags(0, 0), new TestPositionGetter(Vector3.zero));

            for (int i = 0; i < 20; i++)
                manager.Spawn(1000, new TestPositionGetter(Vector3.one * i));
            var findFirstMonster = manager.FindProximateMonster(unit.PositionGetter);
            Assert(findFirstMonster.PositionGetter.Position == Vector3.zero);
            findFirstMonster.OnDamage(1000000);

            var findSecondMonster = manager.FindProximateMonster(unit.PositionGetter);
            Assert(findSecondMonster.PositionGetter.Position == Vector3.one);
        }
    }

    class CreatureUseCaseTester
    {
        public void TestUnitUseCase()
        {
            Log("유닛 유즈케이스 테스트!!");
            var positionGetter = new TestPositionGetter(Vector3.one * 10);
            var attacker = new UnitAttackUseCase(new Unit(new UnitFlags(0, 0), positionGetter), 5);
            var target = new Monster(1000, new TestPositionGetter(Vector3.zero));

            attacker.TryAttack(target);
            Assert(target.CurrentHp == 1000);
            positionGetter.SetPos(Vector3.zero);
            attacker.TryAttack(target);
            Assert(target.CurrentHp == 900);
        }
    }

    class TestPositionGetter : IPositionGetter
    {
        Vector3 _pos;
        public TestPositionGetter(Vector3 pos) => _pos = pos;

        public Vector3 Position => _pos;
        public void SetPos(Vector3 newPos) => _pos = newPos;
    }
}
