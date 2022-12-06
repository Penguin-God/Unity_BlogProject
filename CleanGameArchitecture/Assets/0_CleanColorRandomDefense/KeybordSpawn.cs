using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordSpawn : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            var unit = Managers.Game.SpawnUnit(new UnitFlags(0, 2));
            unit.transform.position = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.K))
            Time.timeScale = Time.timeScale == 1 ? 15 : 1;
    }
}
