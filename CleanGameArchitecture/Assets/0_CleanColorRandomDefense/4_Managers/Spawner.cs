using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using GateWays;
using System;
using UnitDatas;

public interface IUnitDataLoder
{
    UnitData LoadUnitData(UnitFlags flag);
}

public class Spawner // 컨트롤러 매니저랑 합칠까?
{
    UnitSpanwer _unitSpanwer;
    IUnitDataLoder _unitDataLoder;

    public void Init(UnitSpanwer unitSpanwer, IUnitDataLoder unitDataLoder)
    {
        _unitSpanwer = unitSpanwer;
        _unitDataLoder = unitDataLoder;
    }

    public event Action<UnitController> OnUnitSpawn;
    public bool TrySpawnUnit(UnitFlags flag, out UnitController uc)
    {
        if(_unitSpanwer.TrySpawn(flag, out var unit) == false)
        {
            uc = null;
            return false;
        }
        else
        {
            uc = ResourcesManager.Instantiate(SpawnPathBuilder.BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
            var dbData = _unitDataLoder.LoadUnitData(flag);
            var data = new UnitControllerData(
                (flag, dbData.Damage),
                (dbData.Speed, dbData.AttackDelayTime, dbData.AttackRange, ResourcesManager.Load<Material>(ResourcesPathBuilder.BuildUnitMaterialPath(flag.UnitColor)))
                );
            uc.SetInfo(data);
            OnUnitSpawn?.Invoke(uc);
            return true;
        }
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = ResourcesManager.Instantiate(SpawnPathBuilder.BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        var monster = MonsterSpawner.SpawnMonster(1000);
        monsterController.SetInfo(monster);
        ManagerFacade.Controller.AddMonster(monsterController);

        return monsterController;
    }
}
