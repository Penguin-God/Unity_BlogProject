using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace MageSkillTests
{
    public class MageSkillTester
    {
        void TestAOE()
        {
            var mc = ManagerFacade.Game.SpawnMonster(0);
            AOE_System.AOE_Expansion(Vector3.zero, 15, 1.5f,
                (collider) => collider.GetComponent<MonsterController>().Dead()
                );
            Assert(mc == null);
        }
    }
}
