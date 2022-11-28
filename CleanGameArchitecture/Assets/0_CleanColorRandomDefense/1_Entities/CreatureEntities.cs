using System.Collections;
using System.Collections.Generic;
using System;

public enum UnitColor { red, blue, yellow, green, orange, violet, white, black };
public enum UnitClass { sowrdman, archer, spearman, mage }

namespace CreatureEntities
{
    public abstract class Unit
    {
        public UnitFlags Flag { get; private set; }
        public Unit(UnitFlags flag)
        {
            _damage = 100; // 임시
            Flag = flag;
        }

        int _damage;
        public void Attack(Monster monster)
        {
            monster.OnDamage(_damage);
        }

        public event Action<Unit> OnDead;
        public void Dead() => OnDead?.Invoke(this);
    }

    public class Knight : Unit
    {
        public Knight(UnitFlags flag) : base(flag)
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

        public Action<Monster> OnDead;
        public bool IsDead { get; private set; }
        void Dead()
        {
            IsDead = true;
            OnDead?.Invoke(this);
        }
    }

    public class NormalMonster : Monster
    {
        public NormalMonster(int hp) : base(hp)
        {

        }
    }
}
