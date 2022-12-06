using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureEntities;

public class MonsterController : MonoBehaviour, IPositionGetter
{
    public Monster Monster { get; private set; }
    public Vector3 Position => transform.position;

    public void SetInfo(Monster monster)
    {
        Monster = monster;
        Monster.SetPositionGetter(this);
        Monster.OnDead += Dead;
    }

    void Dead(Monster monster)
    {
        gameObject.SetActive(false);
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
