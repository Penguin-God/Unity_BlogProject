using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;

public class MonsterController : MonoBehaviour
{
    [SerializeField] Monster _monster;
    [SerializeField] int _hp; // UI 넣기전까지만 테스트용으로 넣어둠
    public Monster Monster => _monster;
    
    public void SetInfo(Monster monster)
    {
        _monster = monster;
        _monster.OnDead += Dead;
        _hp = _monster.CurrentHp;
        _monster.OnChanagedHp += (hp) => _hp = hp;
    }

    public void Dead() => Monster.OnDamage(Monster.CurrentHp);

    void Dead(Monster monster)
    {
        ResourcesManager.Destroy(gameObject);
    }

    [SerializeField] float _speed = 5;
    public float Speed => _speed;
    void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    bool isFirstHitToWayPoint = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            if (isFirstHitToWayPoint) isFirstHitToWayPoint = false;
            else transform.Rotate(Vector3.up * -90);
        }
    }
}
