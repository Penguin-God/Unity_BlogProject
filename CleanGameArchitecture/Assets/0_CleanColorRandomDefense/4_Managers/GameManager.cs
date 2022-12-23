using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using GateWays;
using System;

public class GameManager // 이 녀석의 정체성에 심각한 수준의 의문 발생 : 얜 맨 바깥쪽 녀석이다. UseCase접근하면 안 된다.
{
    UnitManager _unitManager = new UnitManager();
    UnitSpanwer _unitSpanwer;

    public void Init(IMaxCountRule unitCountRule)
    {
        _unitSpanwer = new UnitSpanwer(_unitManager, unitCountRule);
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
            _unitManager.AddUnit(unit);
            uc = ResourcesManager.Instantiate(SpawnPathBuilder.BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
            foreach (var mesh in uc.GetComponentsInChildren<MeshRenderer>())
                mesh.material = ResourcesManager.Load<Material>(ResourcesPathBuilder.BuildUnitMaterialPath(flag.UnitColor));
            var dbData = ManagerFacade.Data.GetUnitData(flag);
            var data = new UnitControllerData(
                (flag, dbData.Damage),
                (dbData.Speed, dbData.AttackDelayTime, dbData.AttackRange)
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
