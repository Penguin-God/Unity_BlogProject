using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitDatas;
using System.Linq;

public class DataManager : IUnitDataLoder
{
    public void Init()
    {
        _unitFlagByName = CsvUtility.CsvToArray<UnitNameData>(ResourcesManager.Load<TextAsset>("Data/UnitNameData").text).ToDictionary(x => x.KoearName, x => x.UnitFlags);
        _unitDataByFlag = CsvUtility.CsvToArray<UnitData>(ResourcesManager.Load<TextAsset>("Data/UnitData").text).ToDictionary(x => _unitFlagByName[x.Name], x => x);
    }

    Dictionary<string, UnitFlags> _unitFlagByName = new Dictionary<string, UnitFlags>();
    Dictionary<UnitFlags, UnitData> _unitDataByFlag = new Dictionary<UnitFlags, UnitData>();
    public UnitData LoadUnitData(UnitFlags flag) => _unitDataByFlag[flag];
}
