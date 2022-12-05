using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureUseCase;
using GateWays;

public class GameManager
{
    public UnitManager Unit { get; private set; }
    public MonsterManager Monster { get; private set; }

    public void Init(UnitManager unitManager, MonsterManager monsterManager)
        => (Unit, Monster) = (unitManager, monsterManager);

    public UnitController SpawnUnit(UnitFlags flag)
    {
        var unitController = Managers.Resounrces.Instantiate(new SpawnPathBuilder().BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
        unitController.SetInfo(new UnitAttackUseCase(Unit.Spawn(flag), 5));
        return unitController;
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = Managers.Resounrces.Instantiate(new SpawnPathBuilder().BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        monsterController.SetInfo(Monster.Spawn(1000));
        return monsterController;
    }
}
