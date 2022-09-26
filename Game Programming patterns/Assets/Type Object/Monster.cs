using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] int _hp;
    [SerializeField] int _damage;
    [SerializeField] Color32 _color;
    [SerializeField] string _message;

    public void SetInfo(MonsterType type)
    {
        _name = type.Name;
        _hp = type.Hp;
        _damage = type.Damage;
        _color = type.Color;
        _message = type.Message;
    }

    void Start()
    {
        print($"{_name} : {_message}");
    }
}
