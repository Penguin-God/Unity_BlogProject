using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MonsterPassiveType
{
    None,
    AreaEffectSlow,
    Breed,
}

public class MonsterPassiveFactory
{
    public Action<Monster> GetMonsterPassive(MonsterPassiveType type)
    {
        switch (type)
        {
            case MonsterPassiveType.AreaEffectSlow: return new MonsterPassives().AreaEffectSlow;
            case MonsterPassiveType.Breed: return new MonsterPassives().BreedWhenDead;
        }
        return null;
    }
}

class MonsterPassives
{
    public void AreaEffectSlow(Monster monster) => monster.gameObject.AddComponent<RangeSlower>().SetRadius(5);

    class RangeSlower : MonoBehaviour
    {
        public void SetRadius(float radius) => GetComponent<CircleCollider2D>().radius = radius;
        void OnTriggerStay2D(Collider2D collision) => collision.GetComponent<Player>()?.SetSpeed(2);
        void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Player>()?.SetSpeed(5);
    }

    public void BreedWhenDead(Monster monster) => monster.OnDead += () => MonsterSpawner.SpawnMonster("ÁÖÈ² ¹ö¼¸");
}
