using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tests
{
    public class TestGameRules
    {
        public void TestCountRule()
        {
            var rule = new BattleEntities.CountRuleEntity(50, 15);
            Debug.Assert(rule.CheckLoss(50));
            Debug.Assert(rule.CheckFullUnit(15));
            Debug.Log("GOOD!!");
        }
    }

}