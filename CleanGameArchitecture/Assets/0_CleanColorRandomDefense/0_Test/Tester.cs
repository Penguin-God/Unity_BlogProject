﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using EntityTests;
using UseCaseTests;
using GatewayTests;
using CalculatorsTester;
using TestManagerFacade;
using ControllerTester;

public class Tester : MonoBehaviour
{
    void TestAll()
    {
        print("====================");
        print("Test Start");

        foreach (var method in typeof(Tester).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (method.Name.StartsWith("Test") == false || method.Name == "TestAll") continue;
            method.Invoke(this, new object[] { });
        }

        print("Test End");
        print("====================");
    }

    void TestRule()
    {
        var tester = new TestGameRules();
        tester.TestCountRule();
        tester.TestStageReul();
    }

    void TestCreature()
    {
        var tester = new TestCreatures();
        tester.TestUnitAttackToMonster();
        tester.TestMonsterOnDamaged();
    }

    void TestBattleMoney()
    {
        var tester = new BattleMoneyTester();
        tester.TestCreateMoney();
        tester.TestGetMoney();
        tester.TestUseMoney();
    }

    void TestCreatureUseCase()
    {
        var tester = new CreatureUseCaseTester();
        tester.TestUnitUseCase();
        tester.TestRandomSkill();
    }

    void TestCreatureSpawner()
    {
        var tester = new CreatureManagerSpawner();
        tester.TestUnitSpawn();
        tester.TestMonsterSpawn();
    }

    void TestCreatureManager()
    {
        var tester = new CreatureManagerTester();
        tester.TestUnitManagement();
        tester.TestUnitFind();
        tester.TestCombineUnit();
        tester.TestMonsterManagement();
    }

    void TestBattleMoneyManagement()
    {
        var tester = new BattleGoodBuyTester();
        tester.TestCreateMoneyManager();
        tester.TestGetMoney();
        tester.TestUseMoney();
    }

    void TestGateWays()
    {
        var tester = new SpawnPathBuilderTester();
        tester.TestBuildUnitPath();
        tester.TestBuildMonsterPath();
    }

    void TestLoad()
    {
        var tester = new ResourcesPathBuilderTester();
        tester.TestLoadUnitMaterial();
    }

    void TestCalculators()
    {
        var tester = new VectorCalculateTester();
        tester.TestCalculateChasePos();
        tester.TestCalculateShotDirection();
    }

    void TestManagerFacade()
    {
        var tester = new ManagerFacadeTester();
        tester.TestDataManager();
        tester.TestFindMonsterController();
    }

    void TestControllers()
    {
        var tester = new UnitBuyTester();
        tester.TestDrawUnitFlag();
    }
}
