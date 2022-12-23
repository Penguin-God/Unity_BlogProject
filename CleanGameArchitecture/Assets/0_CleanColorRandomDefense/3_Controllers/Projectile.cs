using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CalculateUseCase;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed;
    Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = 50;
    }

    Action<MonsterController> _onHit = null;
    public void Shot(MonsterController mc, Action<MonsterController> onHit)
    {
        Quaternion lookDir = Quaternion.LookRotation(ShotDirectCalculator.GetShotDirection(transform.position, mc.transform.position, mc.Speed, mc.transform.forward));
        transform.rotation = lookDir;
        _rigidbody.velocity = transform.forward * _speed;
        _onHit = onHit;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MonsterController>() != null)
            _onHit?.Invoke(other.GetComponent<MonsterController>());
    }
}
