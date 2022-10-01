using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] int _level;
    public int Level => _level;

    [SerializeField] int _speed;
    public int Speed => _speed;
    public void SetSpeed(int newSpeed) => _speed = newSpeed;

    int _arcanePorce;
    public int ArcanePorce => _arcanePorce;

    void Awake()
    {
        StartCoroutine(Co_AttackRangeInMonsterLoop());
    }

    IEnumerator Co_AttackRangeInMonsterLoop()
    {
        while (true)
        {
            AttackRangeInMonster();
            yield return new WaitForSeconds(2);
        }
    }

    void AttackRangeInMonster()
    {
        IEnumerable<Monster> monsters = Physics2D.CircleCastAll(transform.position, 10, Vector2.zero).Select(x => x.transform.GetComponent<Monster>());
        foreach (var monster in monsters)
        {
            monster.OnDamaged(100, this);
        }
    }
}
