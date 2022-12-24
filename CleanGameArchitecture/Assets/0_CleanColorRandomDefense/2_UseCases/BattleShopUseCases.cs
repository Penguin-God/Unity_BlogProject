using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMoneyEntities;

public class BattleMoneyManager
{
    Dictionary<BattleMoneyType, Money> _typeByMoney = new Dictionary<BattleMoneyType, Money>();

    public int GoldAmount => _typeByMoney[BattleMoneyType.Gold].Amount;
    public int FoodAmount => _typeByMoney[BattleMoneyType.Food].Amount;

    public BattleMoneyManager(int startGold, int startFood)
    {
        _typeByMoney.Add(BattleMoneyType.Gold, MoneyFactory.Gold(startGold));
        _typeByMoney.Add(BattleMoneyType.Food, MoneyFactory.Food(startFood));
    }

    public int GetMoney(BattleMoneyType moneyType, int amount)
    {
        _typeByMoney[moneyType] = _typeByMoney[moneyType].Add(amount);
        return _typeByMoney[moneyType].Amount;
    }

    public bool UseMoney(BattleMoneyType moneyType, int amount, out int result)
    {
        bool useable = _typeByMoney[moneyType].Use(amount, out var money);
        result = money.Amount;
        _typeByMoney[moneyType] = money;
        return useable;
    }
}
