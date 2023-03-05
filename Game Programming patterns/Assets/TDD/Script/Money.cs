using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentType
{
    USD,
    KRW,
    EUR,
    JPY,
}

public struct Money
{
    int _amount;
    public int Amount => _amount;

    public void Add(int amount) => _amount += amount;
}
