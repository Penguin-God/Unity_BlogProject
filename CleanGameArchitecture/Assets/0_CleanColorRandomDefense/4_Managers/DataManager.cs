using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitDatas;
using System.Linq;

public class DataManager
{
    public UnitData GetUnitData(UnitFlags flag)
    {
        string csv = ResourcesManager.Load<TextAsset>("Data/UnitData").text;
        var data = CsvUtility.CsvToArray<UnitData>(csv).ToDictionary(x => x.Flag, x => x);
        return data[flag];
    }
}
