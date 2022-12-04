using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordSpawn : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            var mc = Managers.Game.SpawnMonster(0);
            mc.Monster.OnChanagedHp += (hp) => print($"아파요!!! {hp}");
            var uc = Managers.Game.SpawnUnit(new UnitFlags(0, 1));
            uc.transform.position = Vector3.one * 20;
        }
    }
}
