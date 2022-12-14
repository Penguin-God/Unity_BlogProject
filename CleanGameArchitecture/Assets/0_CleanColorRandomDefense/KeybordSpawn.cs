using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordSpawn : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ManagerFacade.Game.TryUnitSpawn(new UnitFlags(0, 1), out var uc);
            uc.transform.position = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.K))
            Time.timeScale = Time.timeScale == 1 ? 15 : 1;
    }
}
