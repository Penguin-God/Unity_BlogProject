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
        Monster _target;
        IMonsterFinder _monsterFinder;
        public UnitUseCase(IMonsterFinder monsterFinder)
        {
            _monsterFinder = monsterFinder;
        }
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
    }
}
