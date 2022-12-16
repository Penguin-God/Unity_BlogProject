using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackSystem : ICo_Attack
{
    int _useSkillPersent;
    ICo_Attack _normalAttack;
    ICo_Attack _skillAttack;
    
    public RandomAttackSystem(int useSkillPersent, ICo_Attack normalAttack, ICo_Attack skillAttack)
    {
        _useSkillPersent = useSkillPersent;
        _normalAttack = normalAttack;
        _skillAttack = skillAttack;
    }

    public IEnumerator Co_DoAttack()
    {
        int rand = Random.Range(1, 101);
        return rand > _useSkillPersent ? _normalAttack?.Co_DoAttack() : _skillAttack?.Co_DoAttack();
    }
}
