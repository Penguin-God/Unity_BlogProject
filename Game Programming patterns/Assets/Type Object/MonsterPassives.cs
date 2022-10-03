using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MonsterPassiveType
{
    None,
    범위슬로우,
    새끼낳기,
    암흑반감,
}

public class MonsterPassiveFactory
{
    MonsterPassives passives = new MonsterPassives();
    public Action<Monster> GetMonsterPassive(MonsterPassiveType type, IReadOnlyList<string> datas)
    {
        switch (type)
        {
            case MonsterPassiveType.범위슬로우: return (monster) => passives.AreaEffectSlow(monster, datas[0], datas[1]);
            case MonsterPassiveType.새끼낳기: return (monster) => passives.BreedWhenDead(monster, datas[0]);
            case MonsterPassiveType.암흑반감: return (monster) => passives.DarkAntagonism(monster, datas[0]);
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
        Debug.Assert(float.TryParse(rateText, out float rate), "float 데이터 입력 잘못한 듯?");
        monster.DamageCalculte += (damage, attackType) =>
        {
            if(attackType == AttackType.Dark) 
                damage -= Mathf.FloorToInt(damage * (0.01f * rate));
            Debug.Log($"암흑 반감으로 대미지 {damage}로 줄어듬");
            return damage;
        };
    }
}

class RangeSlower : MonoBehaviour
{
    [SerializeField] float _slowRate;
    public void SetCollider(float radius, float slowRate)
    {
        var colldier = GetComponent<CircleCollider2D>();
        colldier.radius = radius;
        colldier.isTrigger = true;
        _slowRate = slowRate;
    }
    void OnTriggerEnter2D(Collider2D collision) => collision.GetComponent<Player>()?.Slow(_slowRate);
    void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Player>()?.ExitSlow();
}
