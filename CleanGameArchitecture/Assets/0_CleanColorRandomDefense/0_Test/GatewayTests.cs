using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GateWays;
using static UnityEngine.Debug;

namespace GatewayTests
{
    public class SpawnPathBuilderTester
    {
        public void TestBuildUnitPath()
        {
            Log("유닛 패스 생성 테스트!!");
            var builder = new SpawnPathBuilder();
            Assert(builder.BuildUnitPath(UnitClass.Swordman) == "Prefabs/Unit/Swordman");
            Assert(builder.BuildUnitPath(UnitClass.Archer) == "Prefabs/Unit/Archer");
            Assert(builder.BuildUnitPath(UnitClass.Spearman) == "Prefabs/Unit/Spearman");
            Assert(builder.BuildUnitPath(UnitClass.Mage) == "Prefabs/Unit/Mage");
        }

        public void TestBuildMonsterPath()
        {
            Log("몬스터 패스 생성 테스트!!");
            var builder = new SpawnPathBuilder();
            Assert(builder.BuildMonsterPath(0) == "Prefabs/Monster/Swordman");
            Assert(builder.BuildMonsterPath(1) == "Prefabs/Monster/Archer");
            Assert(builder.BuildMonsterPath(2) == "Prefabs/Monster/Spearman");
            Assert(builder.BuildMonsterPath(3) == "Prefabs/Monster/Mage");
        }
    }
}