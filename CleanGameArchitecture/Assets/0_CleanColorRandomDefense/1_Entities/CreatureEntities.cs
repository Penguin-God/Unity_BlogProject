using System.Collections;
using System.Collections.Generic;
using System;

public enum UnitColor { Red, Blue, Yellow, Green, Orange, Violet, White, Black };
public enum UnitClass { Swordman, Archer, Spearman, Mage }

namespace CreatureEntities
{
    public class Unit
    {
        public UnitFlags Flag { get; private set; }
        public Unit(UnitFlags flag)
        {
            Flag = flag;
        }

        public void Attack(Monster monster)
        {
            monster.OnDamage(100);
        }

        public event Action<Unit> OnDead;
        public void Dead() => OnDead?.Invoke(this);
    }

    public class Monster
    {
        int _maxHp;
        public int CurrentHp { get; private set; }
        public Monster(int hp)
        {
            _maxHp = hp;
            CurrentHp = _maxHp;
        }

        public Action<int> OnChanagedHp;
        void ChangeHp(int newHp)
        {
            if (newHp > _maxHp) newHp = _maxHp;
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

        public Action<Monster> OnDead;
        public bool IsDead { get; private set; } = false;
        void Dead()
        {
            IsDead = true;
            OnDead?.Invoke(this);
        }
    }
}
