using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [SerializeField] MonsterType _type;
    public MonsterType Type => _type;

    [SerializeField] int _currentHp;

    [SerializeField] Color32 _color;

    public void SetInfo(MonsterType type)
    {
        _type = type;
        gameObject.name = _type.Name;
        _currentHp = _type.Hp;
        GetComponent<SpriteRenderer>().color = type.Color;
        new MonsterPassiveFactory().GetMonsterPassive(type.Passive)?.Invoke(this);
        damageCalculater = new DamageCalculaterFatory().GetCalculater(type.Region);
    }

    IDamageCalculater damageCalculater;
    public void OnDamaged(int damage, Player player)
    {
        if(damageCalculater != null)
            damage = damageCalculater.CalculateDamage(damage, player, this);
        _currentHp -= damage;
        print($"¾ÆÆÄ¿ä : {damage}");
    }

    public event Action OnDead;
    void Dead()
    {
        OnDead?.Invoke();
    }
}