using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [SerializeField] string _name;

    [SerializeField] int _maxHp;
    public int Hp => _maxHp;
    public void UpMaxHp(int addHp)
    {
        _maxHp += addHp;
        _currentHp += addHp;
    }

    [SerializeField] int _currentHp;
    public void OnDamaged(int damage)
    {
        print("¾ÆÆÄ¿ä");
        _currentHp -= damage;
    }

    [SerializeField] int _damage;
    public int Damage => _damage;
    public void UpDamage(int addDamage) => _damage += addDamage;

    [SerializeField] Color32 _color;
    [SerializeField] string _message;

    public void SetInfo(MonsterType type)
    {
        _name = type.Name;
        gameObject.name = _name;
        _maxHp = type.Hp;
        _currentHp = _maxHp;
        _damage = type.Damage;
        GetComponent<SpriteRenderer>().color = type.Color;
        _message = type.Message;
        print(type.Passive);
        new MonsterPassiveFactory().GetMonsterPassive(type.Passive)?.Invoke(this);
    }
}
