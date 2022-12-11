﻿using System.Collections;
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
            Log("추적 좌표 계산 테스트!!");
            var chaser = new TestPositionGetter(Vector3.zero);
            var target = new TestPositionGetter(Vector3.forward * 10);
            Assert(ChasePositionCalculator.GetChasePosition(chaser, target, 1) == Vector3.forward * 9);
        }

        public void TestCalculateShotDirection()
        {
            Log("투사체 발사 방향 계산 테스트");
            var shoter = new TestPositionGetter(Vector3.zero);
            var target = new TestPositionGetter(new Vector3(10, 10, 8));
            Assert(Vector3.Distance(ShotDirectCalculator.GetShotDirection(shoter, target, 2, Vector3.forward), Vector3.one * 0.6f) < 1);
        }
    }
}
