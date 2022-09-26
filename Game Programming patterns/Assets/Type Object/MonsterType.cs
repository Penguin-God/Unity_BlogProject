using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterType
{
    [SerializeField] string _name;
    public string Name => _name;

    [SerializeField] string _parent;
    public string Parent => _parent;

    [SerializeField] int _hp;
    public int Hp => _hp;

    [SerializeField] int _damage;
    public int Damage => _damage;

    [SerializeField] Color32 _color;
    public Color32 Color => _color;

    [SerializeField] string _message;
    public string Message => _message;

    [SerializeField] MonsterPassiveType _passive;
    public MonsterPassiveType Passive => _passive;


    public void OverrideParnet(IReadOnlyDictionary<string, MonsterType> nameByType)
    {
        if (nameByType.TryGetValue(_parent, out MonsterType parent) == false) return;

        if (_hp == 0) _hp = parent._hp;
        if (_damage == 0) _damage = parent._damage;
        if (_color.a == 0) _color = parent._color;
        if (string.IsNullOrEmpty(_message)) _message = parent.Message;
    }
}
