using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntityTests;
using UseCaseTests;

public class Tester : MonoBehaviour
{
    void Awake()
    {
        TestAll();
    }

    [ContextMenu("TestAll")]
    void TestAll()
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
        tester.TestBattleRule();
        tester.TestUnitCountRule();
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
        tester.TestUnitManagement();
        tester.TestMonsterManagement();
        tester.TestFindMonster();
        tester.TestBattleLoss();
    }
}
