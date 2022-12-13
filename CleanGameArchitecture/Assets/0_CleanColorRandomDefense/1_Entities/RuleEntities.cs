using System.Collections;
using System.Collections.Generic;
using System;

namespace RuleEntities
{
    public class MaxCountRule
    {
        public MaxCountRule(int maxCount) => MaxCount = maxCount;
        public int MaxCount { get; protected set; }
        public bool IsMaxCount(int count) => count >= MaxCount;
    }

    public class UnitSpawnRule : MaxCountRule
    {
        public UnitSpawnRule(int maxUnitCount) : base(maxUnitCount) { }
        public Action<int> OnChangedMaxUnitCount;
        public void IncreasedMaxUnit()
        {
            MaxCount++;
            OnChangedMaxUnitCount?.Invoke(MaxCount);
        }
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
