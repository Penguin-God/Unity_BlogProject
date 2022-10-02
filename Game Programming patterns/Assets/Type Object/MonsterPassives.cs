using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MonsterPassiveType
{
    None,
    AreaEffectSlow,
    Breed,
    DarkAntagonism,
}

public class MonsterPassiveFactory
{
    public Action<Monster> GetMonsterPassive(MonsterPassiveType type, IReadOnlyList<string> datas)
    {
        switch (type)
        {
            case MonsterPassiveType.AreaEffectSlow: return (monster) => new MonsterPassives().AreaEffectSlow(monster, datas[0], datas[1]);
            case MonsterPassiveType.Breed: return (monster) => new MonsterPassives().BreedWhenDead(monster, datas[0]);
        }
        return null;
    }
}

class MonsterPassives
{
    public void AreaEffectSlow(Monster monster, string radiusText, string slowRateText)
    {
        Debug.Assert(float.TryParse(radiusText, out float radius), "float 데이터 입력 잘못한 듯?");
        Debug.Assert(float.TryParse(slowRateText, out float slowRate), "float 데이터 입력 잘못한 듯?");
        monster.gameObject.AddComponent<RangeSlower>().SetCollider(radius, slowRate);
    }
    public void BreedWhenDead(Monster monster, string birthName) => monster.OnDead += () => MonsterSpawner.SpawnMonster(birthName, monster.transform.position);
    public void DarkAntagonism(Monster monster, string rateText)
    {

    }
}

class RangeSlower : MonoBehaviour
{
    [SerializeField] float _slowRate;
    public void SetCollider(float radius, float slowRate)
    {
        var colldier = gameObject.AddComponent<CircleCollider2D>();
        colldier.radius = radius;
        colldier.isTrigger = true;
        _slowRate = slowRate;
    }
    void OnTriggerEnter2D(Collider2D collision) => collision.GetComponent<Player>()?.Slow(_slowRate);
    void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Player>()?.ExitSlow();
}
