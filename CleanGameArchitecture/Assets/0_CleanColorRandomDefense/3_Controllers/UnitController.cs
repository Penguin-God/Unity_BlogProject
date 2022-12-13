using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CreatureEntities;
using UnitUseCases;
using CalculateUseCase;

public abstract class UnitController : MonoBehaviour, IPositionGetter
{
    public Vector3 Position => transform.position;

    [SerializeField] float _speed = 15;
    [SerializeField] float _chaseGap;
    [SerializeField] float _stopDistance;
    protected NavMeshAgent _nav;
    protected UnitUseCase _unitUseCase;

    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = _speed;
        _nav.stoppingDistance = _stopDistance;
        Init();
    }
    public void SetInfo(UnitUseCase unitUseCase) => _unitUseCase = unitUseCase;

    protected virtual void Init() { }
    protected abstract void DoAttack(UnitUseCase useCase);

    [SerializeField] bool _attackable = true;
    void Update()
    {
        if(_unitUseCase.IsTargetValid == false)
        {
            _unitUseCase.FindTarget();
            return;
        }

        _nav.SetDestination(ChasePositionCalculator.GetChasePosition(this, _unitUseCase.TargetPosition, _chaseGap));
        if (_attackable && _unitUseCase.IsAttackable())
        {
            StartCoroutine(Co_CoolDawnAttack(1.5f));
            DoAttack(_unitUseCase);
        }
    }

    IEnumerator Co_CoolDawnAttack(float delayTime)
    {
        _attackable = false;
        yield return new WaitForSeconds(delayTime);
        _attackable = true;
    }
}
