using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalculateUseCase
{
    public class ChasePositionCalculator
    {
        public static Vector3 GetChasePosition(Vector3 chaserPos, Vector3 targetPos, float gap)
        {
            Vector3 dir = (targetPos - chaserPos).normalized;
            return targetPos - (dir * gap);
        }
    }

    public class ShotDirectCalculator
    {
        public static Vector3 GetShotDirection(Vector3 shoterPos, Vector3 targetPos, float targetSpeed, Vector3 targetMoveDirection)
            => ((targetPos + targetMoveDirection * targetSpeed) - shoterPos).normalized;
    }
}
