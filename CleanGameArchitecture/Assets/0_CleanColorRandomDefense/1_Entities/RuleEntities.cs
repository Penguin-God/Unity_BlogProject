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

    public class BattleRuleEntity
    {
        public BattleRuleEntity(int maxEnemyCount) => _maxEnemyCount = maxEnemyCount;

        public int _currentEnemyCount;
        public int _maxEnemyCount;
        public bool IsLoss => _currentEnemyCount >= _maxEnemyCount;
    }
}

