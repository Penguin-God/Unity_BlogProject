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

    [SerializeField] float _speed = 15;
    NavMeshAgent _nav;
    UnitAttackUseCase _unitAttackUseCase;
    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = _speed;
        Init();
    }

    protected virtual void Init() { }

    public void SetInfo(UnitAttackUseCase unitAttackUseCase)
    {
        _unitAttackUseCase = unitAttackUseCase;
        _unitAttackUseCase.Unit.SetPositionGetter(this);
    }

    void ChangeTarget()
    {
        _target = Managers.Game.MonsterManager.FindProximateMonster(Position);
    }

    [SerializeField] bool _attackable = true;
    void Update()
    {
        if(_target == null || _target.IsDead)
        {
            ChangeTarget();
            return;
        }
        _nav.SetDestination(ChasePos);
        if (_attackable)
        {
            _attackable = false;
            StartCoroutine(Co_CoolDawnAttack(1.5f));
            Attack();
        }
    }

    protected void DoAttack()
    {
        _unitAttackUseCase.TryAttack(_target);
    }
    protected virtual void Attack() { }

    IEnumerator Co_CoolDawnAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _attackable = true;
    }
}
