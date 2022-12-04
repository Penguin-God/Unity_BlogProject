using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GateWays;

namespace GatewayTests
{
    public class SpawnPathBuilderTester
    {
        public void TestBuildUnitPath()
        {
            var builder = new SpawnPathBuilder();
            Debug.Assert(builder.BuildUnitPath(UnitClass.Swordman) == "Prefabs/Unit/Swordman");
            Debug.Assert(builder.BuildUnitPath(UnitClass.Archer) == "Prefabs/Unit/Archer");
            Debug.Assert(builder.BuildUnitPath(UnitClass.Spearman) == "Prefabs/Unit/Spearman");
            Debug.Assert(builder.BuildUnitPath(UnitClass.Mage) == "Prefabs/Unit/Mage");
        }

        public void TestBuildMonsterPath()
        {
            var builder = new SpawnPathBuilder();
            Debug.Assert(builder.BuildMonsterPath(0) == "Prefabs/Monster/Swordman");
            Debug.Assert(builder.BuildMonsterPath(1) == "Prefabs/Monster/Archer");
            Debug.Assert(builder.BuildMonsterPath(2) == "Prefabs/Monster/Spearman");
            Debug.Assert(builder.BuildMonsterPath(3) == "Prefabs/Monster/Mage");
        }
    }
}