using System.Collections;
using System.Collections.Generic;
using System;

namespace GateWays
{
    public class SpawnPathBuilder
    {
        public string BuildUnitPath(UnitClass unitClass) => $"Prefabs/Unit/{Enum.GetName(typeof(UnitClass), unitClass)}";

        static Dictionary<int, string> _numberByName = new Dictionary<int, string>()
        {
            {0, "Swordman" },
            {1, "Archer" },
            {2, "Spearman" },
            {3, "Mage" },
        };
        public string BuildMonsterPath(int monsterNumber) => $"Prefabs/Monster/{_numberByName[monsterNumber]}";
    }
}
