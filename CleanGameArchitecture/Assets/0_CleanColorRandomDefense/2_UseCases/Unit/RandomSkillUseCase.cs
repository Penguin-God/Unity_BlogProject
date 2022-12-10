using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitUseCases;

public class RandomSkillUseCase : IAttack
{
    int _useSkillPersent;
    IAttack _normalAttack;
    IAttack _skillAttack;
    public RandomSkillUseCase(int useSkillPersent, IAttack normalAttack, IAttack skillAttack)
    {
        _useSkillPersent = useSkillPersent;
        _normalAttack = normalAttack;
        _skillAttack = skillAttack;
    }

    public void DoAttack()
    {
        int rand = Random.Range(1, 101);
        if (rand > _useSkillPersent)
            _normalAttack?.DoAttack();
        else
            _skillAttack?.DoAttack();
    }
}
