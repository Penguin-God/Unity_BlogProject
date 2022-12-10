using CreatureUseCase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : UnitController
{
    TrailRenderer swordTrail;
    Animator animator;

    protected override void Init()
    {
        animator = GetComponentInChildren<Animator>();
        swordTrail = GetComponentInChildren<TrailRenderer>();
    }

    protected override void Attack() => StartCoroutine(Co_SwordAttack());
    

    IEnumerator Co_SwordAttack()
    {
        animator.SetTrigger("isSword");
        yield return new WaitForSeconds(0.8f);
        swordTrail.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _DoAttack();
        swordTrail.gameObject.SetActive(false);
        swordTrail.Clear();
    }
}
