using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleMoneyType
{
    Gold,
    Food,
}

namespace BattleMoneyEntities
{
    public static class MoneyFactory
    {
        public static Money Gold(int amount) => new Money(amount);
        public static Money Food(int amount) => new Money(amount);
    }

    public struct Money
    {
        public int Amount { get; private set; }
        public Money(int amount) => Amount = amount;
        public Money Add(int amount) => new Money(Amount + amount);
        public Money Sub(int amount) => Amount >= amount ? new Money(Amount - amount) : this;
        public bool Use(int amount, out Money result)
        {
            result = Sub(amount);
            return Amount >= amount;
        }
    }
}
