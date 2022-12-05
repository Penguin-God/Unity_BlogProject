using System.Collections;
using System.Collections.Generic;
using System;

namespace RuleEntities
{
    enum PlayerLookWorld
    {
        Field,
        EnemyTower,
    }

    public class PlayerStatus
    {
        int _enemyCount;
        int _unitCount;

        CurrencyManager _currencyManager;
        PlayerLookWorld _playerLookWorld;
    }

    public class UnitCountRule
    {
        public int MaxUnitCount { get; private set; }
        public UnitCountRule(int maxUnitCount) => MaxUnitCount = maxUnitCount;
        public Action<int> OnChangedMaxUnitCount;
        public void IncreasedMaxUnit()
        {
            MaxUnitCount++;
            OnChangedMaxUnitCount?.Invoke(MaxUnitCount);
        }
        public bool CheckFullUnit(int unitCount) => unitCount >= MaxUnitCount;
    }

    public class BattleRule
    {
        public int MaxMonsterCount { get; private set; }
        public BattleRule(int maxMonsterCount) => MaxMonsterCount = maxMonsterCount;

        public bool CheckLoss(int monsterCount) => monsterCount >= MaxMonsterCount;
    }

    public class StageRule
    {
        public int Stage { get; private set; }
        public event Action<int> OnChangedStage;
        public void StageUp()
        {
            Stage++;
            OnChangedStage?.Invoke(Stage);
        }
    }
}

