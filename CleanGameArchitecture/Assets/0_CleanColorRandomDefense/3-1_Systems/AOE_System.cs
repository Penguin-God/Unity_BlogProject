using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AOE_System
{
    public static void AOE_Expansion(Vector3 expanstionPos, float radius, float expansionTime, Action<Collider> OnHit)
    {
        ResourcesManager.Instantiate("");
    }
}

public class AOE_Spell : MonoBehaviour
{
    public event Action<Collider> OnEnterArea;
    public void SetInfo(float useTime)
    {

    }
    void OnTriggerEnter(Collider other) => OnEnterArea?.Invoke(other);

    IEnumerator Co_AfterDestory(float useTime)
    {
        yield return new WaitForSeconds(useTime);
        ResourcesManager.Destroy(gameObject);
    }
}
