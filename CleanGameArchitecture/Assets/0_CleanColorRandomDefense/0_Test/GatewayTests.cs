using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GateWays;
using static UnityEngine.Debug;
using System;

namespace GatewayTests
{
    public class SpawnPathBuilderTester
    {
        public void TestBuildUnitPath()
        {
            Log("유닛 패스 생성 테스트!!");
            Assert(SpawnPathBuilder.BuildUnitPath(UnitClass.Swordman) == "Prefabs/Unit/Swordman");
            Assert(SpawnPathBuilder.BuildUnitPath(UnitClass.Archer) == "Prefabs/Unit/Archer");
            Assert(SpawnPathBuilder.BuildUnitPath(UnitClass.Spearman) == "Prefabs/Unit/Spearman");
            Assert(SpawnPathBuilder.BuildUnitPath(UnitClass.Mage) == "Prefabs/Unit/Mage");
        }

        public void TestBuildMonsterPath()
        {
            Log("몬스터 패스 생성 테스트!!");
            Assert(SpawnPathBuilder.BuildMonsterPath(0) == "Prefabs/Monster/Swordman");
            Assert(SpawnPathBuilder.BuildMonsterPath(1) == "Prefabs/Monster/Archer");
            Assert(SpawnPathBuilder.BuildMonsterPath(2) == "Prefabs/Monster/Spearman");
            Assert(SpawnPathBuilder.BuildMonsterPath(3) == "Prefabs/Monster/Mage");
        }
    }

    public class ResourcesPathBuilderTester
    {
        public void TestLoadUnitMaterial()
        {
            Log("유닛 메테리얼 로드 테스트!!");
            foreach (UnitColor color in Enum.GetValues(typeof(UnitColor)))
                Assert(Resources.Load<Material>(ResourcesPathBuilder.BuildUnitMaterialPath(color)).name == $"Soldiers_{Enum.GetName(typeof(UnitColor), color)}");
        }
    }
}