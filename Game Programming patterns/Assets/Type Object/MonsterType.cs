using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterRegion
{
    MapleWorld,
    ArcaneRiver,
    Cernium,
}

[System.Serializable]
public class MonsterPassive
{
    [SerializeField] MonsterPassiveType _passiveType;
    public MonsterPassiveType PassiveType => _passiveType;

    [SerializeField] string[] _datas;
    public IReadOnlyList<string> Datas => _datas;
}

[System.Serializable]
public class MonsterType
{
    [SerializeField] string _name;
    public string Name => _name;

    [SerializeField] string _parent;
    public string Parent => _parent;

    [SerializeField] int _level;
    public int Level => _level;

    [SerializeField] int _hp;
    public int Hp => _hp;

    [SerializeField] int _damage;
    public int Damage => _damage;

    [SerializeField] Color32 _color;
    public Color32 Color => _color;

    [SerializeField] MonsterPassive[] _passives;
    public IReadOnlyList<MonsterPassive> Passives => _passives;
    public void SetPassive(MonsterPassive[] newPassive) => _passives = newPassive;

    public void OverrideParnet(MonsterType parent)
    {
        Debug.Assert(_parent == parent.Name, "이상한 사람이 부모라고 함(유괴인 듯?)");

        if (_level == 0) _level = parent._level;
        if (_hp == 0) _hp = parent._hp;
        if (_damage == 0) _damage = parent._damage;
        if (_color.a == 0) _color = parent._color;
        if ((_passives == null || _passives.Length == 0) && parent._passives != null) _passives = parent._passives;
    }
}
