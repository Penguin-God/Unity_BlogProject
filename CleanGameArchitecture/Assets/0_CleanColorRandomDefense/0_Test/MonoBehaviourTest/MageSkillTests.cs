using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace MageSkillTests
{
    public class MageSkillTester : MonoBehaviourTester
    {
        public void TestAOE()
        {
            Log("AOE 테스트!!");
            var mc = ManagerFacade.Spawn.SpawnMonster(0);
            AOE_System.AOE_Expansion(Vector3.zero, 15, 1.5f,
                (collider) => collider.GetComponent<MonsterController>().Dead()
                );
            AfterAssert(0.1f, () => mc == null);
        }
    }
}
