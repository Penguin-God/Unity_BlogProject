using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;

namespace CreatureUseCase
{
    public class UnitAttackUseCase
    {
        public Unit Unit { get; private set; }
        float _attackRange;

        public UnitAttackUseCase(Unit unit, float attackRange) => (Unit, _attackRange) = (unit, attackRange);

        public void Attack(Monster target)
        {
            if (Vector3.Distance(Unit.PositionGetter.Position, target.PositionGetter.Position) < _attackRange)
                Unit.Attack(target);
        }
    }
}
