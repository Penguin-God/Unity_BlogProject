using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitDatas
{
    public struct UnitData
    {
        [SerializeField] public string _name;
        public string Name => _name;

        [SerializeField] public int _damage;
        public int Damage => _damage;

        [SerializeField] public float _attackRange;
        public float AttackRange => _attackRange;

        [SerializeField] public float _attackDelayTime;
        public float AttackDelayTime => _attackDelayTime;

        [SerializeField] public float _speed;
        public float Speed => _speed;
    }
}
