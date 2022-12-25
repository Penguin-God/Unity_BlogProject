using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFacade : MonoBehaviour
{
    private static ManagerFacade instance;
    private static ManagerFacade Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ManagerFacade>();
                if (instance == null)
                    instance = new GameObject("Managers").AddComponent<ManagerFacade>();

                DontDestroyOnLoad(instance.gameObject);
                instance.Init();
            }

            return instance;
        }
    }

    Spawner _spawn = new Spawner();
    [SerializeField] ControllerManager _controller = new ControllerManager();
    DataManager _data = new DataManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static Spawner Spawn => Instance._spawn;
    public static ControllerManager Controller => Instance._controller;
    public static DataManager Data => Instance._data;
    public static SceneManagerEx Scene => Instance._scene;

    void Init()
    {
        _data.Init();
    }
}
