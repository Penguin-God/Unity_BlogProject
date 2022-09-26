using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject _monsterPrefab;
    [SerializeField] TextAsset monsterTypes;
    Dictionary<string, MonsterType> _nameByMonsterType = new Dictionary<string, MonsterType>();

    void Awake()
    {
        _nameByMonsterType = CsvUtility.CsvToArray<MonsterType>(monsterTypes.text).ToDictionary(x => x.Name, x => x);
        _nameByMonsterType
            .Values
            .Where(x => string.IsNullOrEmpty(x.Parent) == false)
            .ToList()
            .ForEach(x => x.OverrideParnet(_nameByMonsterType));

        foreach (var monsterType in _nameByMonsterType.Values)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-9, 9), Random.Range(-4, 4));
            Instantiate(_monsterPrefab, spawnPos, Quaternion.identity).GetComponent<Monster>().SetInfo(monsterType);
        }
    }
}
