using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;

public class MonsterController : MonoBehaviour, IPositionGetter
{
    Monster _monster;
    public Monster Monster => _monster;
    public Vector3 Position => transform.position;

    public void SetInfo(Monster monster)
    {
        _monster = monster;
        _monster.SetPositionGetter(this);
        _monster.OnDead += Dead;
    }

    void Dead(Monster monster)
    {
        Destroy(gameObject);
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
