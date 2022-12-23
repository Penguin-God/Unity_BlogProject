using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;


namespace UnitUseCases
{
    public interface IMonsterFinder
    {
        Monster FindProximateMonster(IPositionGetter positionGetter);
        Monster[] FindProximateMonsters(IPositionGetter positionGetter, int count);
    }

    [System.Serializable]
    public class UnitUseCase
    {
        Monster _target;
        public IPositionGetter TargetPosition => _target.PositionGetter;
        [SerializeField] int _damage;

        public UnitUseCase(int damage)
        {
            _damage = damage;
        }

        public void SetTarget(Monster target) => _target = target;
        public bool IsTargetValid => _target != null && _target.IsDead == false;
        public void AttackToTarget()
        {
            if (IsTargetValid)
                _target?.OnDamage(_damage);
        }
    }
}
