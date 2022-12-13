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
        Assert(Vector3.Angle(projectile.transform.eulerAngles, new Vector3(35.2f, 45, 0)) < 0.1f);
        Log(Vector3.Angle(projectile.transform.eulerAngles, new Vector3(35.2f, 45, 0)));
    }
}
