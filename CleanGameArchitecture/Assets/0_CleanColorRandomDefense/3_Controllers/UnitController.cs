using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CreatureEntities;


public class UnitController : MonoBehaviour, IPositionGetter
{
    Monster _target;
    Vector3 ChasePos => _target.PositionGetter.Position;
    public Vector3 Position => transform.position;

    NavMeshAgent _nav;
    Unit _unit;
    void Awake()
    {
        _nav = gameObject.AddComponent<NavMeshAgent>();
    }

    public void SetInfo(Unit unit)
    {
        _unit = unit;
        _unit.SetPositionGetter(this);
    }

    void ChangeTarget()
    {
        _target = Managers.Game.Monster.FindProximateMonster(_unit.PositionGetter);
        _nav.SetDestination(ChasePos);
    }

    void Update()
    {
        if(_target == null)
        {
            ChangeTarget();
            return;
        }
        if (Vector3.Distance(Position, ChasePos) < 5f)
            _unit.Attack(_target);
    }
}
