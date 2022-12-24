using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AOE_System
{
    public static void AOE_Expansion(Vector3 expanstionPos, float radius, float expansionTime, Action<Collider> OnHit)
    {
        var collider = ResourcesManager.Instantiate("Weapon/AOE", expanstionPos).GetComponent<SphereCollider>();
        collider.radius = radius;
        collider.gameObject.AddComponent<AOE_Spell>().SetInfo(expansionTime, OnHit);
    }
}

public class AOE_Spell : MonoBehaviour
{
    public event Action<Collider> OnEnterArea;
    public void SetInfo(float expansionTime, Action<Collider> OnHit)
    {
        OnEnterArea = OnHit;
        StartCoroutine(Co_AfterDestory(expansionTime));
    }
    void OnTriggerEnter(Collider other) => OnEnterArea?.Invoke(other);

    IEnumerator Co_AfterDestory(float useTime)
    {
        yield return new WaitForSeconds(useTime);
        ResourcesManager.Destroy(gameObject);
    }
}
