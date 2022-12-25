using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GateWays;

public class CreatureSpawnInspector : MonoBehaviour
{
    public void SpanwUnit(UnitFlags flag) => ManagerFacade.Spawn.TrySpawnUnit(flag, out var uc);
}
