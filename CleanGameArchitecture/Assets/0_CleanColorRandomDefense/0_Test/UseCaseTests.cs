﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureEntities;
using RuleEntities;
using UnitUseCases;
using static UnityEngine.Debug;

namespace UseCaseTests
{
    public class CreatureManagerSpawner
    {
        public void TestUnitSpawn()
        {
            Log("유닛 스폰 테스트!!");
            var manager = new UnitManager();
            var spanwer = new UnitSpanwer(manager, new UnitSpawnRule(15));
            var spawnFlag = new UnitFlags(3, 2);
            Assert(spanwer.TrySpawn(spawnFlag, out Unit unit));
            Assert(spawnFlag == unit.Flag);

            for (int i = 0; i < 15; i++)
                manager.AddUnit(new Unit(spawnFlag));

            Assert(spanwer.TrySpawn(spawnFlag, out unit) == false);
        }

        public void TestMonsterSpawn()
        {
            Log("몬스터 스폰 테스트!!");
            var monster = MonsterSpawner.SpawnMonster(1000);
            Assert(monster.CurrentHp == 1000);
        }
    }

    public class CreatureManagerTester
    {
        public void TestUnitManagement()
        {
            Log("유닛 매니저 테스트!!");
            var manager = new UnitManager();
            var unitFlag = new UnitFlags(0, 0);
            var unit = new Unit(new UnitFlags(0, 0));
            
            manager.AddUnit(unit);
            Assert(manager.Units[0] == unit);
            Assert(manager.Units.Count == 1);
            Assert(manager.TryFindUnit(unitFlag, out Unit findUnit));
            Assert(findUnit == unit);

            unit.Dead();
            Assert(manager.Units.Count == 0);
            Assert(manager.TryFindUnit(unitFlag, out Unit nullUnit) == false);
            Assert(nullUnit == null);
        }

        public void TestCombineUnit()
        {
            Log("유닛 조합 테스트!!");
            var manager = new UnitManager();
            var testCondition = new Dictionary<UnitFlags, CreatureManagementUseCases.CombineCondition[]>()
            {
                { new UnitFlags(0, 1), new CreatureManagementUseCases.CombineCondition[] {new CreatureManagementUseCases.CombineCondition(new UnitFlags(0,0), 3) } },
                { new UnitFlags(0, 2), new CreatureManagementUseCases.CombineCondition[] {new CreatureManagementUseCases.CombineCondition(new UnitFlags(0,0), 2),
                new CreatureManagementUseCases.CombineCondition(new UnitFlags(0,1), 3)} }
            };

            var combiner = new UnitCombiner(testCondition, manager);
            Assert(combiner.TryCombine(new UnitFlags(0, 1), out Unit unit) == false);

            int loopCount = 0;
            foreach (var item in testCondition) // Key는 조합할 유닛 플래그, Value는 조건[]
            {
                loopCount++;
                foreach (var condition in item.Value)
                {
                    for (int i = 0; i < condition.NeedCount; i++)
                        manager.AddUnit(new Unit(condition.Flag));
                }
                Assert(combiner.TryCombine(item.Key, out unit));
                Assert(unit.Flag == item.Key);
                Assert(manager.Units.Count == loopCount);
            }
        }

        public void TestMonsterManagement()
        {
            Log("몬스터 매니저 테스트!!");
            var manager = new MonsterManager();
            var monster = new Monster(1000);
            manager.AddMonster(monster);
            Assert(manager.Monsters[0] == monster);

            monster.OnDamage(monster.CurrentHp);
            Assert(manager.Monsters.Count == 0);
        }

        public void TestBattleLoss()
        {
            Log("게임 패배 테스트!!");
            bool isLoss = false;
            var manager = new MonsterManager();
            var rule = new MaxCountRule(50);
            manager.OnMonsterCountChanged += (count) => { if (rule.IsMaxCount(count)) isLoss = true; };
            AddMonsters(manager, 30);
            Assert(isLoss == false);

            AddMonsters(manager, 20);
            Assert(isLoss);
        }

        void AddMonsters(MonsterManager manager, int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
                manager.AddMonster(new Monster(1000));
        }

        public void TestFindMonster()
        {
            Log("몬스터 찾기 테스트!!");
            var manager = new MonsterManager();
            IPositionGetter finder = new TestPositionGetter(Vector3.zero);
            
            for (int i = 0; i < 20; i++)
                manager.AddMonster(new Monster(1000, new TestPositionGetter(Vector3.one * i)));
            var findFirstMonster = manager.FindProximateMonster(finder);
            Assert(findFirstMonster.PositionGetter.Position == Vector3.zero);
            findFirstMonster.OnDamage(findFirstMonster.CurrentHp);

            var findSecondMonster = manager.FindProximateMonster(finder);
            Assert(findSecondMonster.PositionGetter.Position == Vector3.one);
        }

        public void TestFindMonsters()
        {
            Log("여러 몬스터 찾기 테스트!!");
            var manager = new MonsterManager();
            IPositionGetter finder = new TestPositionGetter(Vector3.zero);

            for (int i = 0; i < 20; i++)
                manager.AddMonster(new Monster(1000, new TestPositionGetter(Vector3.one * i)));

            var monsters = manager.FindProximateMonsters(finder, 3);
            for (int i = 0; i < monsters.Length; i++)
                Assert(monsters[i].PositionGetter.Position == Vector3.one * i);
        }
    }

    class CreatureUseCaseTester
    {
        public void TestUnitUseCase()
        {
            Log("유닛 유즈케이스 테스트!!");
            var target = MonsterSpawner.SpawnMonster(1000);
            var unitUseCase = new UnitUseCase(100);
            
            unitUseCase.AttackToTarget();
            StateAssert(false, 1000);
            
            unitUseCase.SetTarget(target);
            unitUseCase.AttackToTarget();
            StateAssert(true, 900);

            void StateAssert(bool targetValid, int hp)
            {
                Assert(unitUseCase.IsTargetValid == targetValid);
                Assert(target.CurrentHp == hp);
            }
        }

        public void TestRandomSkill()
        {
            Log("랜덤 스킬 테스트!!");
            bool isSkillAttack = false;
            TestAttackEvent normal = new TestAttackEvent(() => isSkillAttack = false);
            TestAttackEvent skill = new TestAttackEvent(() => isSkillAttack = true);
            var skillUseCase = new RandomAttackSystem(100, normal, skill);
            for (int i = 0; i < 1000; i++)
            {
                skillUseCase.Co_DoAttack();
                Assert(isSkillAttack);
            }

            skillUseCase = new RandomAttackSystem(0, normal, skill);
            for (int i = 0; i < 1000; i++)
            {
                skillUseCase.Co_DoAttack();
                Assert(isSkillAttack == false);
            }
        }
    }

    class TestAttackEvent : ICo_Attack
    {
        public event System.Action OnAttack;
        public TestAttackEvent(System.Action action) => OnAttack = action;

        public IEnumerator Co_DoAttack()
        {
            OnAttack?.Invoke();
            return null;
        }
    }
}

class TestPositionGetter : IPositionGetter
{
    Vector3 _pos;
    public TestPositionGetter(Vector3 pos) => _pos = pos;

    public Vector3 Position => _pos;
    public void SetPos(Vector3 newPos) => _pos = newPos;
}
