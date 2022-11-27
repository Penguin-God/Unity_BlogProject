using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tests;

public class Tester : MonoBehaviour
{
    [ContextMenu("TestRule")]
    void TestRule()
    {
        var tester = new TestGameRules();
        tester.TestCountRule();
        tester.TestStageReul();
    }

    [ContextMenu("TestCreature")]
    void TestCreature()
    {
        var tester = new TestCreatures();
        tester.TestUnitAttackToMonster();
    }
}
