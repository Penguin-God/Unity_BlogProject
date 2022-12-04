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
        int _damage;
        public IPositionGetter PositionGetter { get; private set; }
        public Unit(UnitFlags flag)
        {
            _damage = 100; // 임시
            Flag = flag;
        }

        public Unit(UnitFlags flag, IPositionGetter positionGetter)
        {
            Flag = flag;
            PositionGetter = positionGetter;
        }

        public void Attack(Monster monster)
        {
            monster.OnDamage(_damage);
        }

        public event Action<Unit> OnDead;
        public void Dead() => OnDead?.Invoke(this);
    }

    public class Monster
    {
        int maxHp;
        public int CurrentHp { get; private set; }
        public IPositionGetter PositionGetter { get; private set; }
        public Monster(int hp)
        {
            maxHp = hp;
            CurrentHp = maxHp;
        }

        public Monster(int hp, IPositionGetter positionGetter)
        {
            maxHp = hp;
            CurrentHp = maxHp;
            PositionGetter = positionGetter;
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

        public Action<Monster> OnDead;
        public bool IsDead { get; private set; }
        void Dead()
        {
            IsDead = true;
            OnDead?.Invoke(this);
        }
    }
}
