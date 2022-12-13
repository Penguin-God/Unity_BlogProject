using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CalculateUseCase;
using System;

public class Projectile : MonoBehaviour, IPositionGetter
{
    public Vector3 Position => transform.position;

    void Awake()
    {
        
    }

    Action _onHit = null;
    public void Shot(MonsterController mc, Action onHit)
    {
        Quaternion lookDir = Quaternion.LookRotation(ShotDirectCalculator.GetShotDirection(this, mc, mc.Speed, mc.transform.forward));
        transform.rotation = lookDir;
        _onHit = onHit;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MonsterController>())
            _onHit?.Invoke();
    }
}
