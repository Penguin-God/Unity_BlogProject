using CreatureEntities;
using System.Collections;
using System.Collections.Generic;
using UnitUseCases;
using UnityEngine;

public class Archer : UnitController
{
    [SerializeField] int _useSkillPercent;
    RandomAttackSystem _randomSkillUseCase;

    protected override IEnumerator Co_Attack() => _randomSkillUseCase.Co_DoAttack();

    protected override void Init()
    {
        _useSkillPercent = 30;
        var normal = gameObject.AddComponent<ArcherAttack>();
        var skill = gameObject.AddComponent<ArcherSkill>();
        _randomSkillUseCase = new RandomAttackSystem(_useSkillPercent, normal, skill);
    }

    class ArcherAttack : ShoterUnit, ICo_Attack
    {
        protected Archer _archer;
        TrailRenderer _trail;

        protected override string ProjectileName => "Arrow";

        protected override void Init()
        {
            _trail = GetComponentInChildren<TrailRenderer>(true);
            _archer = (Archer)_uc;
        }

        public IEnumerator Co_DoAttack()
        {
            _archer._nav.isStopped = true;
            _trail.gameObject.SetActive(false);
            Attack();
            yield return new WaitForSeconds(1f);
            _trail.gameObject.SetActive(true);
            _trail.Clear();
            _archer._nav.isStopped = false;
        }

        protected virtual void Attack() => DoShot(_archer._target, (mo) => Debug.Log("맞았어!!"));
    }

    class ArcherSkill : ArcherAttack
    {
        readonly int SKILL_ATTOW_COUNT = 3;
        protected override void Attack()
        {
            MonsterController[] targets = ManagerFacade.Controller.FindProximateMonsters(transform.position, SKILL_ATTOW_COUNT);
            if (targets == null || targets.Length == 0) return;

            for (int i = 0; i < SKILL_ATTOW_COUNT; i++)
            {
                int targetIndex = i % targets.Length;
                DoShot(targets[targetIndex], (mo) => Debug.Log("맞았어!!"));
            }
        }
    }
}
