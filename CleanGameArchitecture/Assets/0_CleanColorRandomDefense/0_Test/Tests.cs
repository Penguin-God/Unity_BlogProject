using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;
using CreatureEntities;

namespace Tests
{
    public class TestGameRules
    {
        public void TestCountRule()
        {
            var rule = new CountRule(50, 15);
            Debug.Assert(rule.CheckLoss(50));
            Debug.Assert(rule.CheckFullUnit(15));
            Debug.Log("PassCountRule!!");
        }

        public void TestStageReul()
        {
            int testInt = 0;
            var rule = new StageRule();
            rule.OnChangedStage += (stage) => testInt += 1;
            for (int i = 0; i < 5; i++)
                rule.StageUp();
            Debug.Assert(rule.Stage == 6);
            Debug.Assert(testInt == 5);
            Debug.Log("PassStageRule");
        }
    }

    public class TestCreatures
    {
        public void TestUnitAttackToMonster()
        {
            var unit = new Knight(100);
            var monster = new NormalMonster(1000);
            int getChangedHp = 0;
            bool getDeadState = false;
            monster.OnChanagedHp += (hp) => getChangedHp = hp;
            monster.OnDead += () => getDeadState = true;

            unit.Attack(monster);
            Debug.Assert(monster.CurrentHp == 900);
            Debug.Assert(getChangedHp == 900);
            Debug.Assert(getDeadState == false);

            monster.OnDamage(-333);
            Debug.Assert(monster.CurrentHp == 900);
            Debug.Assert(getChangedHp == 900);
            Debug.Assert(getDeadState == false);

            int eventCount = 0;
            monster.OnChanagedHp += (hp) => eventCount++;
            for (int i = 0; i < 15; i++)
                unit.Attack(monster);

            Debug.Assert(monster.CurrentHp == 0);
            Debug.Assert(eventCount == 9);
            Debug.Assert(getChangedHp == 0);
            Debug.Assert(getDeadState);

            Debug.Log("PassAttack!!");
        }
    }
}