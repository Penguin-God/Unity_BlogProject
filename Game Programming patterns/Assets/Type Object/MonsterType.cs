using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterType
{
    [SerializeField] string _name;
    public string Name => _name;
    [SerializeField] int _hp;
    public int Hp => _hp;
    [SerializeField] int _damage;
    public int Damage => _damage;
    [SerializeField] Color _color;
    public Color Color => _color;
    [SerializeField] string _message;
    public string Message => _message;

    public void OverrideParnet(MonsterType parent)
    {
        if (parent == null) return;

        if (string.IsNullOrEmpty(_name)) _name = parent._name;
        if (_hp == 0) _hp = parent._hp;
        if (_damage == 0) _damage = parent._damage;
        if (_color.a == 0) _color = parent._color;
        if (string.IsNullOrEmpty(_message)) _message = parent._name;
    }
}
