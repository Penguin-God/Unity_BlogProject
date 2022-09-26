using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDamage : MonoBehaviour
{
    // TODO : Ray로 바꾸기
    public void SetRadius(float radius) => GetComponent<CircleCollider2D>().radius = radius;
    void OnTriggerStay2D(Collider2D collision) => collision.GetComponent<Monster>()?.OnDamaged(2);

}
