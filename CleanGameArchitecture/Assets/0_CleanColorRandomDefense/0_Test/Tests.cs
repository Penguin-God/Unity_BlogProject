using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;

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

}