using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CreatureEntities;
using CreatureUseCase;

public class UnitController : MonoBehaviour, IPositionGetter
{
    Monster _target;
    Vector3 ChasePos => _target.PositionGetter.Position;
    public Vector3 Position => transform.position;

    NavMeshAgent _nav;
    UnitAttackUseCase _unitAttackUseCase;
    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    public void SetInfo(UnitAttackUseCase unitAttackUseCase)
    {
        _unitAttackUseCase = unitAttackUseCase;
        _unitAttackUseCase.Unit.SetPositionGetter(this);
    }

    void ChangeTarget()
    {
        _target = Managers.Game.Monster.FindProximateMonster(Position);
    }

    void Update()
    {
        if(_target == null || _target.IsDead)
        {
            ChangeTarget();
            return;
        }
        _nav.SetDestination(ChasePos);
        _unitAttackUseCase.TryAttack(_target);
    }
}
