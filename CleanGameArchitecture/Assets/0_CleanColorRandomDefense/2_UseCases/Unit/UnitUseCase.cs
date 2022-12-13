using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;


namespace UnitUseCases
{
    public interface IMonsterFinder
    {
        Monster FindProximateMonster(IPositionGetter positionGetter);
    }

    public interface IAttack
    {
        void DoAttack();
    }

    [System.Serializable]
    public class UnitUseCase
    {
        [SerializeField] Monster _target;
        public IPositionGetter TargetPosition => _target.PositionGetter;
        IMonsterFinder _monsterFinder;
        IPositionGetter _positionGetter;
        float _attackRange;
        int _damage;

        public UnitUseCase(IMonsterFinder monsterFinder, IPositionGetter positionGetter, float attackRange, int damage)
        {
            _positionGetter = positionGetter;
            _monsterFinder = monsterFinder;
            _attackRange = attackRange;
            _damage = damage;
        }

        public void FindTarget() => _target = _monsterFinder.FindProximateMonster(_positionGetter);
        public bool IsTargetValid => _target != null && _target.IsDead == false;
        public void AttackToTarget()
        {
            if (IsTargetValid && ToTargetDistace < _attackRange * 2)
                _target?.OnDamage(_damage);
        }
        public bool IsAttackable()
        {
            if (IsTargetValid == false) return false;
            return _attackRange > ToTargetDistace;
        }

        float ToTargetDistace => Vector3.Distance(_positionGetter.Position, _target.PositionGetter.Position);
    }

    public class UnitAttackUseCase
    {
        public Unit Unit { get; private set; }
        float _attackRange;

        public UnitAttackUseCase(Unit unit, float attackRange) => (Unit, _attackRange) = (unit, attackRange);

        public bool TryAttack(Monster target)
        {
            if (Vector3.Distance(Unit.PositionGetter.Position, target.PositionGetter.Position) < _attackRange)
            {
                Unit.Attack(target);
                return true;
            }
            else return false;
        }

        public bool IsAttackable(IPositionGetter targetPosGetter)
            => _attackRange > Vector3.Distance(Unit.PositionGetter.Position, targetPosGetter.Position);
    }
}
