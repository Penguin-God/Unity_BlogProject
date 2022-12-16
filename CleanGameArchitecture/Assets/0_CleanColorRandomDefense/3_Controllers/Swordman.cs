using System.Collections;
using System.Collections.Generic;
using UnitUseCases;
using UnityEngine;

public class Swordman : UnitController
{
    TrailRenderer swordTrail;
    Animator animator;

    protected override void Init()
    {
        animator = GetComponentInChildren<Animator>();
        swordTrail = GetComponentInChildren<TrailRenderer>(true);
    }

    protected override IEnumerator Co_Attack()
    {
        animator.SetTrigger("isSword");
        yield return new WaitForSeconds(0.8f);
        swordTrail.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _unitUseCase.AttackToTarget();
        swordTrail.gameObject.SetActive(false);
        swordTrail.Clear();
    }
}
