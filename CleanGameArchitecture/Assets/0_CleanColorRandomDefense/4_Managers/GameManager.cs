using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureUseCase;
using RuleEntities;
using GateWays;

public class GameManager
{
    UnitManager _unit = new UnitManager();
    MonsterManager _monster = new MonsterManager();

    public UnitManager Unit => _unit;
    public MonsterManager Monster => _monster;

    public void Init(UnitCountRule unitCountRule)
    {
        _unit.Init(unitCountRule);
    }

    public UnitController SpawnUnit(UnitFlags flag)
    {
        var unitController = Managers.Resounrces.Instantiate(new SpawnPathBuilder().BuildUnitPath(flag.UnitClass)).GetComponent<UnitController>();
        unitController.SetInfo(new UnitAttackUseCase(_unit.Spawn(flag), 5));
        return unitController;
    }

    public MonsterController SpawnMonster(int monstNumber)
    {
        var monsterController = Managers.Resounrces.Instantiate(new SpawnPathBuilder().BuildMonsterPath(monstNumber)).GetComponent<MonsterController>();
        monsterController.SetInfo(_monster.Spawn(1000));
        return monsterController;
    }
}
