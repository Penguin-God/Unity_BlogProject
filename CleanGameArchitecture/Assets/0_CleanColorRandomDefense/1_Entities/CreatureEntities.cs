using System.Collections;
using System.Collections.Generic;
using System;

namespace CreatureEntities
{
    public abstract class Unit
    {
        public Unit(int damage)
        {
            _damage = damage;
        }

        int _damage;
        public void Attack(Monster monster)
        {
            monster.OnDamage(_damage);
        }
    }

    public class Knight : Unit
    {
        public Knight(int damage) : base(damage)
        {

        }
    }

    public abstract class Monster
    {
        int maxHp;
        public int CurrentHp { get; private set; }
        public Monster(int hp)
        {
            maxHp = hp;
            CurrentHp = maxHp;
        }
        public Action<int> OnChanagedHp;
        void ChangeHp(int newHp)
        {
            if (newHp > maxHp) newHp = maxHp;
            if (0 >= newHp) newHp = 0;
            CurrentHp = newHp;
            OnChanagedHp?.Invoke(CurrentHp);
        }

        public void OnDamage(int damage)
        {
            if (IsDead) return;

            if (0 > damage) damage = 0;
            ChangeHp(CurrentHp -= damage);
            if (0 >= CurrentHp)
                Dead();
        }

        public Action OnDead;
        public bool IsDead { get; private set; }
        void Dead()
        {
            IsDead = true;
            OnDead?.Invoke();
        }
    }

    public class NormalMonster : Monster
    {
        public NormalMonster(int hp) : base(hp)
        {

        }
    }
}
