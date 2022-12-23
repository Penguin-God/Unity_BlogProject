using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;


namespace UnitUseCases
{
    [System.Serializable]
    public class UnitUseCase
    {
        Monster _target;
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
