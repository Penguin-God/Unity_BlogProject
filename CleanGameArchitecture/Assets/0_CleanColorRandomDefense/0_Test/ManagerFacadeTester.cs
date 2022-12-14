using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using CreatureEntities;
using static UnityEngine.Debug;

namespace TestManagerFacade
{
    public class ManagerFacadeTester
    {
        public void TestGameManager()
        {
            Log("게임 매니저 테스트!!");
            var game = new GameManager();
            game.Init(null, new MonsterManager());
            var mc = game.SpawnMonster(1);
            var monster = game.GetMonseterController(mc.Monster);
            Assert(monster == mc);
            Object.DestroyImmediate(mc.gameObject);
        }
    }
}
