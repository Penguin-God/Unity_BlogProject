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