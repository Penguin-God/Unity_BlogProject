using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using UnitUseCases;
using GateWays;
using CreatureEntities;

public class GameManager // 이 녀석의 정체성에 심각한 수준의 의문 발생 : 얜 맨 바깥쪽 녀석이다. UseCase접근하면 안 된다.
{
    UnitManager _unitManager = new UnitManager();
    UnitSpanwer _unitSpanwer;
    MonsterManager _monsterManager;
    MonsterSpawner _monsterSpawner = new MonsterSpawner();

    public void Init(IMaxCountRule unitCountRule, MonsterManager monsterManager)
    {
        _unitSpanwer = new UnitSpanwer(_unitManager, unitCountRule);
        _monsterManager = monsterManager;
    }

    Dictionary<Monster, MonsterController> _monsetrByMc = new Dictionary<Monster, MonsterController>();
    public MonsterController GetMonseterController(Monster monster) => _monsetrByMc[monster];

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

            var dbData = ManagerFacade.Data.GetUnitData(flag);
            var data = new UnitControllerData(
                (flag, dbData.Damage, dbData.AttackRange),
                (dbData.Speed, dbData.AttackDelayTime, ResourcesManager.Load<Material>(ResourcesPathBuilder.BuildUnitMaterialPath(flag.UnitColor)))
                );
            uc.SetInfo(_monsterManager, data);
            return true;
        }
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = ResourcesManager.Instantiate(SpawnPathBuilder.BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        var monster = _monsterSpawner.SpawnMonster(1000);
        _monsterManager.AddMonster(monster);
        monsterController.SetInfo(monster);
        _monsetrByMc.Add(monster, monsterController);
        monster.OnDead += (m) => _monsetrByMc.Remove(m);
        return monsterController;
    }
}
