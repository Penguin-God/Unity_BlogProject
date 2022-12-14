using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using UnitUseCases;
using GateWays;
using CreatureEntities;

public class GameManager
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

    public bool TryUnitSpawn(UnitFlags flag, out UnitController uc)
    {
        if(_unitSpanwer.TrySpawn(flag, out var unit) == false)
        {
            uc = null;
            return false;
        }
        else
        {
            _unitManager.AddUnit(unit);
            uc = ResourcesManager.Instantiate(new SpawnPathBuilder().BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
            uc.SetInfo(new UnitUseCase(_monsterManager, uc, 50, 100));
            return true;
        }
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = ResourcesManager.Instantiate(new SpawnPathBuilder().BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        var monster = _monsterSpawner.SpawnMonster(1000);
        _monsterManager.AddMonster(monster);
        monsterController.SetInfo(monster);
        _monsetrByMc.Add(monster, monsterController);
        monster.OnDead += (m) => _monsetrByMc.Remove(m);
        return monsterController;
    }
}
