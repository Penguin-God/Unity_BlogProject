using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnitUseCases;
using CalculateUseCase;

public interface ICo_Attack
{
    IEnumerator Co_DoAttack();
}

public struct UnitControllerData
{
    public UnitFlags Flag { get; private set; }
    public int Damage { get; private set; }
    public float AttackRange { get; private set; }
    public float Speed { get; private set; }
    public float AttackDelayTime { get; private set; }

    public UnitControllerData((UnitFlags flag, int dam, float range) useCaseData, (float speed, float delay) controllerData)
    {
        (Flag, Damage, AttackRange) = useCaseData;
        (Speed, AttackDelayTime) = controllerData;
    }
}

public abstract class UnitController : MonoBehaviour, IPositionGetter
{
    public Vector3 Position => transform.position;

    [SerializeField] protected UnitUseCase _unitUseCase;
    [SerializeField] float _speed;
    [SerializeField] float _attackDelayTime;
    [SerializeField] float _attackRange;
    [SerializeField] float _chaseGap;
    [SerializeField] float _stopDistance;
    [SerializeField] protected MonsterController _target;

    protected NavMeshAgent _nav;
    
    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = _speed;
        _nav.stoppingDistance = _stopDistance;
        Init();
    }
    public void SetInfo(UnitControllerData data)
    {
        _speed = data.Speed;
        _attackDelayTime = data.AttackDelayTime;
        _unitUseCase = new UnitUseCase(data.Damage);
        _attackRange = data.AttackRange;
    }

    protected virtual void Init() { }

    [SerializeField] bool _attackable = true;
    bool TargetInRange => _attackRange > Vector3.Distance(_target.transform.position, transform.position);
    void Update()
    {
        if(_unitUseCase.IsTargetValid == false)
        {
            FindTarget();
            return;
        }

        _nav.SetDestination(ChasePositionCalculator.GetChasePosition(this, _unitUseCase.TargetPosition, _chaseGap));
        if (_attackable && _unitUseCase.IsTargetValid && TargetInRange)
            StartCoroutine(Co_DoAttack());
    }

    void FindTarget()
    {
        var mc = ManagerFacade.Controller.FindProximateMonster(transform.position);
        if (mc == null) return;
        _target = mc;
        _unitUseCase.SetTarget(mc.Monster);
    }

    IEnumerator Co_DoAttack()
    {
        _attackable = false;
        yield return StartCoroutine(Co_Attack());
        StartCoroutine(Co_CoolDawnAttack(_attackDelayTime));
    }
    protected abstract IEnumerator Co_Attack();
    

    IEnumerator Co_CoolDawnAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _attackable = true;
    }
}
