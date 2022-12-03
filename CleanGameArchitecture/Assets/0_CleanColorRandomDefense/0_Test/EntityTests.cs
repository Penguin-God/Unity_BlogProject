using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;
using CreatureEntities;

namespace EntityTests
{
    public class TestGameRules : Testing
    {
        public void TestBattleRule()
        {
            var rule = new BattleRule(50);
            Assert(rule.MaxMonsterCount == 50);
            Assert(rule.CheckLoss(33) == false);
            Assert(rule.CheckLoss(50));
            Log("승패 규칙 통과!!");
        }

        public void TestUnitCountRule()
        {
            var rule = new UnitCountRule(15);
            Assert(rule.MaxUnitCount == 15);
            Assert(rule.CheckFullUnit(10) == false);
            Assert(rule.CheckFullUnit(15));
            Log("유닛 최대 갯수 규칙 통과!!");
        }

        public void TestStageReul()
        {
            int testInt = 0;
            var rule = new StageRule();
            rule.OnChangedStage += (stage) => testInt += 1;
            for (int i = 0; i < 5; i++)
                rule.StageUp();
            Assert(rule.Stage == 6);
            Assert(testInt == 5);
            Log("Pass Stage Rule");
        }
    }

    public class TestCreatures : Testing
    {
        public void TestUnitAttackToMonster()
        {
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

            Log("공격 통과!!");

            void CheckMonsterCondition(int monsterHp, int changedHp, bool deadState)
            {
                Assert(monster.CurrentHp == monsterHp);
                Assert(getChangedHp == changedHp);
                Assert(getDeadState == deadState);
            }
        }
    }
}