using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ShoterUnit : MonoBehaviour
{
    protected abstract string ProjectileName { get; }
    protected UnitController _uc;
    void Awake()
    {
        _uc = GetComponent<UnitController>();
        Init();
    }

    protected virtual void Init() { }

    protected void DoShot(MonsterController target, Action<MonsterController> OnHit)
    {
        var projectile = ResourcesManager.Instantiate($"Weapon/{ProjectileName}", transform.position).GetComponent<Projectile>();
        projectile.Shot(target, OnHit);
    }
}
