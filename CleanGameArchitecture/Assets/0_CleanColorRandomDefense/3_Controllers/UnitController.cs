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

public struct UnitControllerData // 위에 3개는 유즈케이스 데이터, 아래 3개는 컨트롤러 데이터로 분리하기
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

    [SerializeField] float _speed;
    [SerializeField] float _attackDelayTime;
    [SerializeField] float _chaseGap;
    [SerializeField] float _stopDistance;
    protected NavMeshAgent _nav;
    [SerializeField] protected UnitUseCase _unitUseCase;

    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = _speed;
        _nav.stoppingDistance = _stopDistance;
        Init();
    }
    public void SetInfo(IMonsterFinder monsterFinder, UnitControllerData data)
    {
        _speed = data.Speed;
        _attackDelayTime = data.AttackDelayTime;
        _unitUseCase = new UnitUseCase(monsterFinder, this, data.AttackRange, data.Damage);
    }

    protected virtual void Init() { }

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
            StartCoroutine(Co_DoAttack());
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
