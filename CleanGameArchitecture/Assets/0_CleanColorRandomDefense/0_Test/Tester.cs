using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tests;

public class Tester : MonoBehaviour
{
    void Awake()
    {
        print("====================");
        print("Test Start");
        TestRule();
        TestCreature();
        print("Test End");
        print("====================");
    }

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
