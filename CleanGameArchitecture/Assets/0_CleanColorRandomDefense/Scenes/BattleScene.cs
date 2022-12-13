using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleEntities;
using CreatureManagementUseCases;

public class BattleScene : BaseScene
{
    MaxCountRule _battleRule;
    protected override void Init()
    {
        var unitSpawnRule = new UnitSpawnRule(15);
        var unitManager = new UnitManager();
        var unitSpawner = new UnitSpanwer(unitManager, unitSpawnRule);
        var monsterManager = new MonsterManager();
        ManagerFacade.Game.Init(unitManager, unitSpawner, monsterManager);

        _battleRule = new MaxCountRule(50);
        monsterManager.OnMonsterCountChanged += CheckGameLoss;

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
        int spawnNumber = Random.Range(0, 4);
        for (int i = 0; i < 15; i++)
        {
            var mc = ManagerFacade.Game.SpawnMonster(spawnNumber);
            mc.transform.position = new Vector3(-45, 0, 35);
            yield return new WaitForSeconds(2f);
        }
    }

    void CheckGameLoss(int monsterCount)
    {
        if (_battleRule.IsMaxCount(monsterCount))
            Time.timeScale = 0;
    }
}
