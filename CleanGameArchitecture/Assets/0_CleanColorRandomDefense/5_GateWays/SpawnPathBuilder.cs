using System.Collections;
using System.Collections.Generic;
using System;

namespace GateWays
{
    public static class SpawnPathBuilder
    {
        public static string BuildUnitPath(UnitClass unitClass) => $"Prefabs/Unit/{Enum.GetName(typeof(UnitClass), unitClass)}";

        static Dictionary<int, string> _numberByName = new Dictionary<int, string>()
        {
            {0, "Swordman" },
            {1, "Archer" },
            {2, "Spearman" },
            {3, "Mage" },
        };
        public static string BuildMonsterPath(int monsterNumber) => $"Prefabs/Monster/{_numberByName[monsterNumber]}";
    }

    public static class ResourcesPathBuilder
    {
        public static string BuildUnitMaterialPath(UnitColor color) => $"Materials/Unit/Soldiers_{Enum.GetName(typeof(UnitColor), color)}";
    }
}
