using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleEntities
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

    public class CountRuleEntity
    {
        public CountRuleEntity(int maxEnemyCount, int maxUnitCount)
        {
            MaxMonsterCount = maxEnemyCount;
            MaxUnitCount = maxUnitCount;
        }

        public int MaxMonsterCount { get; private set; }
        public bool CheckLoss(int monsterCount) => monsterCount >= MaxMonsterCount;

        public int MaxUnitCount { get; private set; }
        public bool CheckFullUnit(int unitCount) => unitCount >= MaxUnitCount;
    }
}

