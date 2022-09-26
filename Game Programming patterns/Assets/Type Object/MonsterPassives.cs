using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MonsterPassiveType
{
    None,
    UpDamage,
    UpMaxHp,
    AreaEffectPosingDamage,
}

public class MonsterPassiveFactory
{
    public Action<Monster> GetMonsterPassive(MonsterPassiveType type)
    {
        switch (type)
        {
            case MonsterPassiveType.UpDamage: return new MonsterPassives().UpDamage;
            case MonsterPassiveType.UpMaxHp: return new MonsterPassives().UpMaxHp;
            case MonsterPassiveType.AreaEffectPosingDamage: return new MonsterPassives().AreaEffectPosingDamage;
        }
        return null;
    }
}

class MonsterPassives
{
    public void UpDamage(Monster monster) => monster?.UpDamage(50);
    public void UpMaxHp(Monster monster) => monster?.UpMaxHp(20);
    public void AreaEffectPosingDamage(Monster monster) => monster.gameObject.AddComponent<RangeDamage>().SetRadius(5);
}