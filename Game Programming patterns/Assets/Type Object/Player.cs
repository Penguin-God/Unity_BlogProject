using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AttackType
{
    Default,
    Dark,
    Holy,
}

public class Attack
{
    public Attack(AttackType type, int damage)
    {
        _attackType = type;
        _damage = damage;
    }

    AttackType _attackType;
    public AttackType AttackType => _attackType;

    int _damage;
    public int Damage => _damage;
}

public class Player : MonoBehaviour
{
    [SerializeField] int _level;
    public int Level => _level;

    [SerializeField] float _originSpeed;
    [SerializeField] float _speed;
    public void Slow(float newSpeed) => _speed -= _speed * (0.01f * newSpeed);
    public void ExitSlow() => _speed = _originSpeed;

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
        IEnumerable<Monster> monsters = 
            Physics2D.CircleCastAll(transform.position, 10, Vector2.zero)
            .Where(x => x.transform.GetComponent<Monster>() != null)
            .Select(x => x.transform.GetComponent<Monster>());
        foreach (var monster in monsters)
        {
            monster.OnDamaged(new Attack(AttackType.Dark, 300));
        }
    }
}
