using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterEntities
{
    public class OnDamageEntity
    {
        public OnDamageEntity(int hp, EnemyType enemyType)
        {
            _hp = hp;
            _enemyType = enemyType;
        }

        int _maxHp;
        int _hp;
        EnemyType _enemyType;

        public void Dead()
        {

        }

        public void OnDamaged()
        {

        }
    }
}
