using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
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

        public void TestDataManager()
        {
            Log("데이터 매니저 테스트!!");
            var data = new DataManager();
            data.Init();
            var unitData = data.GetUnitData(new UnitFlags(0, 0));
            Assert(unitData.Damage == 25);
            Assert(unitData.Name == "빨간 기사");

            unitData = data.GetUnitData(new UnitFlags(2, 3));
            Assert(unitData.Damage == 25000);
            Assert(unitData.Name == "노란 마법사");
        }

        public void TestControllerManager()
        {
            Log("컨트롤러 매니저 테스트!!");
            var controller = new ControllerManager();
            Vector3 findStartPos = Vector3.zero;
            for (int i = 0; i < 20; i++)
            {
                var mc = SpawnComponent<MonsterController>();
                mc.transform.position = Vector3.one * i;
                controller.AddMonsterController(mc);
            }

            var findFirstMc = controller.FindProximateMonster(findStartPos);
            Assert(findFirstMc.PositionGetter.Position == Vector3.zero);
            findFirstMc.OnDamage(findFirstMc.CurrentHp);

            var findSecondMc = controller.FindProximateMonster(findStartPos);
            Assert(findSecondMc.PositionGetter.Position == Vector3.one);
        }

        GameObject SpawnObject(string name = "") => new GameObject(name);
        T SpawnComponent<T>(string name = "") where T : Component
        {
            var go = new GameObject(name);
            go.AddComponent<T>();
            return go;
        }
    }
}
