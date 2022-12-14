using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using UnitUseCases;
using GateWays;
using CreatureEntities;

public class GameManager
{
    public UnitManager UnitManager { get; private set; }
    UnitSpanwer _unitSpanwer;
    public MonsterManager MonsterManager { get; private set; }
    MonsterSpawner _monsterSpawner = new MonsterSpawner();

    public void Init(UnitManager unitManager, UnitSpanwer unitSpanwer, MonsterManager monsterManager) 
        => (UnitManager, _unitSpanwer, MonsterManager) = (unitManager, unitSpanwer, monsterManager);

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
            UnitManager.AddUnit(unit);
            uc = ResourcesManager.Instantiate(new SpawnPathBuilder().BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
            uc.SetInfo(new UnitUseCase(MonsterManager, uc, 5, 100));
            return true;
        }
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = ResourcesManager.Instantiate(new SpawnPathBuilder().BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        var monster = _monsterSpawner.SpawnMonster(1000);
        MonsterManager.AddMonster(monster);
        monsterController.SetInfo(monster);
        _monsetrByMc.Add(monster, monsterController);
        return monsterController;
    }
}
