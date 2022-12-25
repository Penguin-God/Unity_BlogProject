using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace ControllerTester
{
    public class UnitBuyTester
    {
        public void TestDrawUnitFlag()
        {
            Log("유닛 뽑기 테스트!!");
            var bc = new BuyController(UnitColor.Yellow, UnitClass.Swordman);
            for (int i = 0; i < 1000; i++)
            {
                var flag = bc.DrawUnitFlag();
                Assert(flag.UnitColor <= UnitColor.Yellow);
                Assert(flag.UnitClass == UnitClass.Swordman);
            }
        }
    }
}
