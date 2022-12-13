using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using UnitUseCases;
using GateWays;

public class GameManager
{
    public UnitManager UnitManager { get; private set; }
    UnitSpanwer _unitSpanwer;
    public MonsterManager MonsterManager { get; private set; }
    MonsterSpawner _monsterSpawner = new MonsterSpawner();

    public void Init(UnitManager unitManager, UnitSpanwer unitSpanwer, MonsterManager monsterManager) 
        => (UnitManager, _unitSpanwer, MonsterManager) = (unitManager, unitSpanwer, monsterManager);

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
            uc = ManagerFacade.Resounrces.Instantiate(new SpawnPathBuilder().BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
            uc.SetInfo(new UnitUseCase(MonsterManager, uc, 5, 100));
            return true;
        }
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = ManagerFacade.Resounrces.Instantiate(new SpawnPathBuilder().BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        var monster = _monsterSpawner.SpawnMonster(1000);
        MonsterManager.AddMonster(monster);
        monsterController.SetInfo(monster);
        return monsterController;
    }
}
