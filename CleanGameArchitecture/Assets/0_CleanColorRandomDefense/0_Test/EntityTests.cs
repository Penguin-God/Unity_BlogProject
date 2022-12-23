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
        public void TestCountRule()
        {
            Log("카운트 규칙 테스트!!");
            var rule = new MaxCountRule(50);
            Assert(rule.MaxCount == 50);
            Assert(rule.IsMaxCount(33) == false);
            Assert(rule.IsMaxCount(50));
        }

        public void TestStageReul()
        {
            Log("스테이지 규칙 테스트!!");
            int testInt = 0;
            var rule = new StageRule();
            rule.OnChangedStage += (stage) => testInt += 1;
            for (int i = 0; i < 5; i++)
                rule.StageUp();
            Assert(rule.Stage == 5);
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
            
            unit.Attack(monster);
            Assert(monster.CurrentHp == 900 && monster.IsDead == false);

            for (int i = 0; i < 15; i++)
                unit.Attack(monster);

            Assert(monster.IsDead);
        }

        public void TestMonsterOnDamaged()
        {
            Log("몬스터 피해 받는거 테스트!!");
            var monster = new Monster(1000);
            int getChangedHp = 0;
            bool getDeadState = false;
            monster.OnChanagedHp += (hp) => getChangedHp = hp;
            monster.OnDead += (deadMonster) => getDeadState = true;

            monster.OnDamage(100);
            CheckMonsterCondition(900, 900, false);

            monster.OnDamage(-333);
            CheckMonsterCondition(900, 900, false);

            int eventCount = 0;
            monster.OnChanagedHp += (hp) => eventCount++;
            for (int i = 0; i < 15; i++)
                monster.OnDamage(100);

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

    public class BattleShopTester
    {
        public void TestCreateMoney()
        {
            Log("재화 생성 테스트!!");
            var five = MoneyFactory.Gold(5);
            Assert(five.Amount == 5);
        }

        public void TestGetMoney()
        {
            Log("재화 획득 테스트!!");
        }

        public void TestUseMoney()
        {
            Log("재화 사용 테스트!!");
        }
    }
}