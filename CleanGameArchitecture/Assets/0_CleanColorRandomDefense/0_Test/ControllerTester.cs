using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class ControllerTester
{
    public void TestShot()
    {
        var projectile = new GameObject("Shot").AddComponent<Projectile>();
        var target = new GameObject("Target").AddComponent<MonsterController>();
        target.transform.position = new Vector3(10, 10, 5);
        projectile.Shot(target, null);
        Assert(Quaternion.Angle(projectile.transform.rotation, Quaternion.Euler(new Vector3(-35.264f, 45, 0))) < 0.1f);
        Object.DestroyImmediate(projectile.gameObject);
        Object.DestroyImmediate(target.gameObject);
    }
}
