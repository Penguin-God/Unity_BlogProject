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

        foreach (var passive in _type.Passives)
            new MonsterPassiveFactory().GetMonsterPassive(passive.PassiveType, passive.Datas)?.Invoke(this);

        //damageCalculater = new DamageCalculaterFatory().GetCalculater(type.Region);
    }

    IDamageCalculater damageCalculater;

    public void OnDamaged(int damage, Player player)
    {
        if(damageCalculater != null)
            damage = damageCalculater.CalculateDamage(damage, player, this);
        _currentHp -= damage;
    }

   
    public event Action OnDead;
    [ContextMenu("Dead")]
    void Dead()
    {
        OnDead?.Invoke();
    }
}