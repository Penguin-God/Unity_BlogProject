using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CalculateUseCase;
using static UnityEngine.Debug;

namespace CalculatorsTester
{
    public class VectorCalculateTester
    {
        public void TestCalculateChasePos()
        {
            var chaser = new TestPositionGetter(Vector3.zero);
            var target = new TestPositionGetter(Vector3.forward * 10);
            Assert(ChasePositionCalculator.GetChasePosition(chaser, target, 1) == Vector3.forward * 9);
        }
    }
}
