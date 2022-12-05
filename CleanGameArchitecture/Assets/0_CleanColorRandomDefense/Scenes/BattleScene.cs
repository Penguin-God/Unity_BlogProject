using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;
using CreatureManagementUseCases;

public class BattleScene : BaseScene
{
    protected override void Init()
    {
        var unitCountRule = new UnitCountRule(15);
        var unitManager = new UnitManager(unitCountRule);
        var monsterManager = new MonsterManager();
        Managers.Game.Init(unitManager, monsterManager);
    }
}
