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
        _nav = gameObject.AddComponent<NavMeshAgent>();
    }

    public void SetInfo(UnitAttackUseCase unitAttackUseCase)
    {
        _unitAttackUseCase = unitAttackUseCase;
        _unitAttackUseCase.Unit.SetPositionGetter(this);
    }

    void ChangeTarget()
    {
        _target = Managers.Game.Monster.FindProximateMonster(Position);
        _nav.SetDestination(ChasePos);
    }

    void Update()
    {
        if(_target == null)
        {
            ChangeTarget();
            return;
        }
        _unitAttackUseCase.Attack(_target);
    }
}
