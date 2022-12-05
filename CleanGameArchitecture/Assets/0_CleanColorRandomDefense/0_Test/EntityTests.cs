using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;
using CreatureEntities;
using static UnityEngine.Debug;

namespace EntityTests
{
    public class TestGameRules
    {
        public void TestBattleRule()
        {
            Log("승패 규칙 테스트!!");
            var rule = new BattleRule(50);
            Assert(rule.MaxMonsterCount == 50);
            Assert(rule.CheckLoss(33) == false);
            Assert(rule.CheckLoss(50));
        }

        public void TestUnitCountRule()
        {
            Log("유닛 최대 갯수 규칙 테스트!!");
            var rule = new UnitCountRule(15);
            Assert(rule.MaxUnitCount == 15);
            Assert(rule.CheckFullUnit(10) == false);
            Assert(rule.CheckFullUnit(15));
        }

        public void TestStageReul()
        {
            Log("스테이지 규칙 테스트!!");
            int testInt = 0;
            var rule = new StageRule();
            rule.OnChangedStage += (stage) => testInt += 1;
            for (int i = 0; i < 5; i++)
                rule.StageUp();
            Assert(rule.Stage == 6);
            Assert(testInt == 5);
        }
    }

    public class TestCreatures
    {
        public void TestUnitAttackToMonster()
        {
            Log("유닛 공격 테스트!!");
            var unit = new Unit(new UnitFlags(0, 0));
            var monster = new Monster(1000);
            int getChangedHp = 0;
            bool getDeadState = false;
            monster.OnChanagedHp += (hp) => getChangedHp = hp;
            monster.OnDead += (deadMonster) => getDeadState = true;

            unit.Attack(monster);
            CheckMonsterCondition(900, 900, false);

            monster.OnDamage(-333);
            CheckMonsterCondition(900, 900, false);

            int eventCount = 0;
            monster.OnChanagedHp += (hp) => eventCount++;
            for (int i = 0; i < 15; i++)
                unit.Attack(monster);

            CheckMonsterCondition(0, 0, true);
            Assert(eventCount == 9);


            void CheckMonsterCondition(int monsterHp, int changedHp, bool deadState)
            {
                Assert(monster.CurrentHp == monsterHp);
                Assert(getChangedHp == changedHp);
                Assert(getDeadState == deadState);
            }
        }
    }
}