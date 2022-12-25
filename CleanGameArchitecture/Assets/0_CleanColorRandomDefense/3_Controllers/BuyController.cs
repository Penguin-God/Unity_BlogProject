using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyController
{
    UnitColor _maxUnitColor;
    UnitClass _maxUnitClass;
    public BuyController(UnitColor maxColor, UnitClass maxClass)
    {
        _maxUnitColor = maxColor;
        _maxUnitClass = maxClass;
    }

    public UnitFlags DrawUnitFlag() => new UnitFlags(Random.Range(0, (int)_maxUnitColor + 1), Random.Range(0, (int)_maxUnitClass + 1));
}
