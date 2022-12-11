using System.Collections;
using System.Collections.Generic;
using System;

namespace RuleEntities
{
    public static class RuleManager
    {
        public static UnitCountRule UnitCount { get; private set; } = new UnitCountRule(0);
        public static void InitUnitCountRule(int maxUnitCount) => UnitCount = new UnitCountRule(maxUnitCount);

        public static BattleRule Battle { get; private set; } = new BattleRule(50);
        public static void InitBattleRule(int maxMonsterCount) => UnitCount = new UnitCountRule(maxMonsterCount);

        public static StageRule Stage { get; private set; } = new StageRule();
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

