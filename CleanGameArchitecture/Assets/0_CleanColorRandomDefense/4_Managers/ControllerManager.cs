using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class ControllerManager
{
    [SerializeField] List<MonsterController> _monsters = new List<MonsterController>();
    public IReadOnlyList<MonsterController> Monsters => _monsters;
    public event Action<int> OnMonsterCountChanaged;

    public void AddMonster(MonsterController mc)
    {
        _monsters.Add(mc);
        mc.Monster.OnDead += (monster) => RemoveMonster(mc);
        OnMonsterCountChanaged?.Invoke(_monsters.Count);
    }

    void RemoveMonster(MonsterController mc)
    {
        _monsters.Remove(mc);
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
