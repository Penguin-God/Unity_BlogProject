using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;


public class BattleScene : BaseScene
{
    protected override void Init()
    {
        var unitCountRule = new UnitCountRule(15);
        Managers.Game.Init(unitCountRule);
    }
}
