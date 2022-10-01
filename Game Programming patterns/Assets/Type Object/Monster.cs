using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [SerializeField] string _name;

    [SerializeField] int _level;
    public int Level => _level;

    [SerializeField] int _maxHp;
    public int Hp => _maxHp;
    public void UpMaxHp(int addHp)
    {
        _maxHp += addHp;
        _currentHp += addHp;
    }

    [SerializeField] int _currentHp;

    [SerializeField] int _damage;
    public int Damage => _damage;
    public void UpDamage(int addDamage) => _damage += addDamage;

    [SerializeField] Color32 _color;

    public int _arcanePorce;

    public void SetInfo(MonsterType type)
    {
        _name = type.Name;
        gameObject.name = _name;
        _level = type.Level;
        _maxHp = type.Hp;
        _currentHp = _maxHp;
        _damage = type.Damage;
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