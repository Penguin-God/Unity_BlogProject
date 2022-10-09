using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject _monsterPrefab;
    static GameObject monsterPrefab;

    [SerializeField] TextAsset monsterTypes;
    [SerializeField] TextAsset passiveData;

    static Dictionary<string, MonsterType> _nameByMonsterType = new Dictionary<string, MonsterType>();
    void Awake()
    {
        
        _nameByMonsterType = CsvUtility.CsvToArray<MonsterType>(monsterTypes.text).ToDictionary(x => x.Name, x => x);
        
        monsterPrefab = _monsterPrefab;
        SetPassives();
        OverrideMonsterData();
        SpanwAllMonster();
    }

    void SpanwAllMonster()
    {
        foreach (var monsterType in _nameByMonsterType.Values)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-9, 9), Random.Range(-4, 4));
            SpawnMonster(monsterType.Name, spawnPos);
        }
    }

    void SetPassives()
    {
        var _nameByPassives = CsvUtility.CsvToArray<MonsterType>(passiveData.text).ToDictionary(x => x.Name, x => x.Passives);
        foreach (var type in _nameByMonsterType.Values)
        {
            if (_nameByPassives.TryGetValue(type.Name, out IReadOnlyList<MonsterPassive> passives))
                type.SetPassive(passives.ToArray());
        }
    }

    void OverrideMonsterData()
    {
        _nameByMonsterType
            .Values
            .Where(x => string.IsNullOrEmpty(x.Parent) == false)
            .ToList()
            .ForEach(x => x.OverrideParnet(_nameByMonsterType[x.Parent]));
    }

    public static Monster SpawnMonster(string monsterName, Vector2 pos)
    {
        var monster = Instantiate(monsterPrefab, pos, Quaternion.identity).AddComponent<Monster>();
        monster.SetInfo(_nameByMonsterType[monsterName]);
        return monster;
    }
}
