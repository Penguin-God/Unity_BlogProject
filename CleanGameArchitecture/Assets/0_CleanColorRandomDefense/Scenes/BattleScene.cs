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
        var stageRule = new StageRule();
        stageRule.OnChangedStage += SpawnStageMonster;
        StartCoroutine(Co_StageStart(stageRule));
    }

    IEnumerator Co_StageStart(StageRule stage)
    {
        while (true)
        {
            stage.StageUp();
            yield return new WaitForSeconds(40);
        }
    }

    void SpawnStageMonster(int stage) => StartCoroutine(Co_SpawnStageMonster(stage));
    IEnumerator Co_SpawnStageMonster(int stage)
    {
        for (int i = 0; i < 15; i++)
        {
            var mc = Managers.Game.SpawnMonster(0);
            mc.transform.position = new Vector3(-45, 0, 35);
            yield return new WaitForSeconds(2f);
        }
    }
}
