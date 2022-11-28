using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntityTests;
using UseCaseTests;

public class Tester : MonoBehaviour
{
    void Awake()
    {
        print("====================");
        print("Test Start");
        TestRule();
        TestCreature();
        TestCreatureManager();
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

    [ContextMenu("TestCreatureManager")]
    void TestCreatureManager()
    {
        var tester = new CreatureManagerTester();
        tester.TestUnitSpawnAndDead();
    }
}
