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
}
