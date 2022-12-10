using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;


namespace UnitUseCases
{
    public interface IMonsterFinder
    {
        Monster FindMonster();
    }

    public class UnitUseCase
    {
        Unit Unit;
        Monster _target;
        IMonsterFinder _monsterFinder;
        float _attackRange;
        int _damage;

        public UnitUseCase(IMonsterFinder monsterFinder)
        {
            _monsterFinder = monsterFinder;
        }

        public bool CheckTargetValid() => _target != null && _target.IsDead == false;

        public void DamageToTarget()
        {
            _target.OnDamage(_damage);
        }

        public bool IsAttackable()
            => _attackRange > Vector3.Distance(Unit.PositionGetter.Position, _target.PositionGetter.Position);
    }

    public interface IAttack
    {
        void DoAttack();
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
