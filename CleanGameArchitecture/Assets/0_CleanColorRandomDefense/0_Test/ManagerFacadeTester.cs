using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureManagementUseCases;
using static UnityEngine.Debug;

namespace TestManagerFacade
{
    public class ManagerFacadeTester
    {
        public void TestDataManager() // 엑셀 데이터가 바뀌면 테스트 통과 못함. 데이터를 직접 넣기
        {
            Log("데이터 매니저 테스트!!");
            var data = new DataManager();
            data.Init();
            var unitData = data.LoadUnitData(new UnitFlags(0, 0));
            Assert(unitData.Damage == 25);
            Assert(unitData.Name == "빨간 기사");

            unitData = data.LoadUnitData(new UnitFlags(2, 3));
            Assert(unitData.Damage == 25000);
            Assert(unitData.Name == "노란 마법사");
        }

        public void TestFindMonsterController()
        {
            Log("컨트롤러 매니저 몬스터 찾기 테스트!!");
            var finder = new ControllerManager();
            for (int i = 0; i < 20; i++)
            {
                var mc = SpawnComponent<MonsterController>();
                mc.SetInfo(MonsterSpawner.SpawnMonster(1000));
                mc.transform.position = Vector3.one * i;
                finder.AddMonster(mc);
            }
            Assert(finder.Monsters.Count == 20);

            var findFirstMc = finder.FindProximateMonster(Vector3.zero);
            Assert(findFirstMc.transform.position == Vector3.zero);

            var findSecondMc = finder.FindProximateMonster(Vector3.one * 3);
            Assert(findSecondMc.transform.position == Vector3.one * 3);

            foreach (var monster in finder.Monsters)
                Object.DestroyImmediate(monster.gameObject);
        }

        GameObject SpawnObject(string name = "") => new GameObject(name);
        T SpawnComponent<T>(string name = "") where T : Component
        {
            var go = new GameObject(name);
            T result = go.AddComponent<T>();
            return result;
        }
    }
}
