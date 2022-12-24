using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RayCastSystem
{
    public static RaycastHit[] SphereCastAll(Vector3 pos, float raidus)
        => Physics.SphereCastAll(pos, raidus, Vector3.up, 0);
}
