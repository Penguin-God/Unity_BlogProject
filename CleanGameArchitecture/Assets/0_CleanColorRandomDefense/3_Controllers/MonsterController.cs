using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;

public class MonsterController : MonoBehaviour, IPositionGetter
{
    [SerializeField] Monster _monster;
    [SerializeField] int _hp;
    public Monster Monster => _monster;
    public Vector3 Position => transform.position;

    public void SetInfo(Monster monster)
    {
        _monster = monster;
        _monster.SetPositionGetter(this);
        _monster.OnDead += Dead;
        _monster.OnChanagedHp += (hp) => _hp = hp;
    }

    void Dead(Monster monster)
    {
        Managers.Resounrces.Destroy(gameObject);
    }

    [SerializeField] float _speed = 5;
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
