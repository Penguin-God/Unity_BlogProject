using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [SerializeField] MonsterType _type;
    public MonsterType Type => _type;
    [SerializeField] int _currentHp;
    public void SetInfo(MonsterType type)
    {
        _type = type;
        gameObject.name = _type.Name;
        _currentHp = _type.Hp;
        GetComponent<SpriteRenderer>().color = type.Color;

        if(_type.Passives != null)
        {
            foreach (var passive in _type.Passives)
                new MonsterPassiveFactory().GetMonsterPassive(passive.PassiveType, passive.Datas)?.Invoke(this);
        }

        //damageCalculater = new DamageCalculaterFatory().GetCalculater(type.Region);
    }

    //IDamageCalculater damageCalculater;
    public event Func<int, AttackType, int> DamageCalculte = null;

    int DamageCalculteAll(Attack attack)
    {
        if (DamageCalculte == null) return attack.Damage;

        int result = attack.Damage;
        foreach (Func<int, AttackType, int> func in DamageCalculte.GetInvocationList())
            result = func.Invoke(result, attack.AttackType);
        return result;
    }

    public void OnDamaged(Attack attack)
    {
        _currentHp -= DamageCalculteAll(attack);
        if (_currentHp <= 0)
            Dead();
    }

    public event Action OnDead;
    [ContextMenu("Dead")]
    void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}