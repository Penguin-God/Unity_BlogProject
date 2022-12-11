using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalculateUseCase
{
    public class ChasePositionCalculator
    {
        public static Vector3 GetChasePosition(IPositionGetter chaser, IPositionGetter target, float gap)
        {
            Vector3 dir = (target.Position - chaser.Position).normalized;
            return target.Position - (dir * gap);
        }
    }
}
