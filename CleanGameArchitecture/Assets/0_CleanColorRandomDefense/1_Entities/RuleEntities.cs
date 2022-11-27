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

    public class CountRule
    {
        public CountRule(int maxEnemyCount, int maxUnitCount)
        {
            MaxMonsterCount = maxEnemyCount;
            MaxUnitCount = maxUnitCount;
        }

        public int MaxMonsterCount { get; private set; }
        public bool CheckLoss(int monsterCount) => monsterCount >= MaxMonsterCount;

        public int MaxUnitCount { get; private set; }
        public bool CheckFullUnit(int unitCount) => unitCount >= MaxUnitCount;
    }

    public class StageRule
    {
        public int Stage { get; private set; } = 1;
        public event Action<int> OnChangedStage;
        public void StageUp()
        {
            Stage++;
            OnChangedStage?.Invoke(Stage);
        }
    }
}

