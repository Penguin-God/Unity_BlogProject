using System.Collections;
using System.Collections.Generic;
using UnitUseCases;
using UnityEngine;

public class Archer : UnitController
{
    [SerializeField] int skillArrowCount = 3;
    
    [SerializeField] int _useSkillPercent;
    [SerializeField] float _skillReboundTime;
    RandomSkillUseCase _randomSkillUseCase;

    protected override void Init()
    {
        _useSkillPercent = 30;
        var attack = gameObject.AddComponent<ArcherAttack>();
        attack.SetInfo(this);
        _randomSkillUseCase = new RandomSkillUseCase(0, attack, new ArcherSkillAttack());
    }

    protected override void DoAttack(UnitUseCase useCase) => _randomSkillUseCase.DoAttack();

    //void ShotSkill()
    //{
    //    Transform[] targetArray = GetTargets();
    //    if (targetArray == null || targetArray.Length == 0) return;

    //    for (int i = 0; i < skillArrowCount; i++)
    //    {
    //        int targetIndex = i % targetArray.Length;
    //        ProjectileShotDelegate.ShotProjectile(arrawData, targetArray[targetIndex], OnSkillHit);
    //    }
    //}
    // 노말 어택용 공격, 스킬용 공격 클래스 만들기

    class ArcherAttack : MonoBehaviour, IAttack
    {
        Archer _archer;
        const string _weaponName = "Arrow";
        TrailRenderer _trail;

        void Awake()
        {
            _trail = GetComponentInChildren<TrailRenderer>(true);
        }

        public void SetInfo(Archer archer)
        {
            _archer = archer;
        }

        public void DoAttack() => StartCoroutine(Co_ArrowAttack());

        IEnumerator Co_ArrowAttack()
        {
            _archer._nav.isStopped = true;
            _trail.gameObject.SetActive(false);
            Attack();
            yield return new WaitForSeconds(1f);
            _trail.gameObject.SetActive(true);
            _trail.Clear();
            _archer._nav.isStopped = false;
        }

        protected virtual void Attack()
        {
            var arrow = ResourcesManager.Instantiate($"Weapon/{_weaponName}", transform.position).GetComponent<Projectile>();
            arrow.Shot(ManagerFacade.Game.GetMonseterController(_archer._unitUseCase.Target), _archer._unitUseCase.AttackToTarget);
        }

        void Shot()
        {

        }
    }

    class ArcherSkillAttack : ArcherAttack
    {
        protected override void Attack()
        {
            base.Attack();
        }
    }
}
