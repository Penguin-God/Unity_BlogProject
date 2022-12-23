using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ControllerManager : MonoBehaviour
{
    List<MonsterController> _monsters = new List<MonsterController>();
    public IReadOnlyList<MonsterController> Monsters => _monsters;
    public event Action<int> OnMonsterCountChanaged;

    public void AddMonsterController(MonsterController mc)
    {
        _monsters.Add(mc);
        OnMonsterCountChanaged?.Invoke(_monsters.Count);
    }

    public MonsterController FindProximateMonster(Vector3 findStartPos)
        => GetOrderMonstersByDistance(findStartPos).FirstOrDefault();

    public MonsterController[] FindProximateMonsters(Vector3 findStartPos, int count)
    {
        if (_monsters.Count == 0) return null;
        return GetOrderMonstersByDistance(findStartPos).Take(count).ToArray();
    }

    IEnumerable<MonsterController> GetOrderMonstersByDistance(Vector3 findStartPos)
        => _monsters.OrderBy(x => Vector3.Distance(x.transform.position, findStartPos));
}
