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

    GameManager _game = new GameManager();
    ResourcesManager _resources = new ResourcesManager();
    SceneManagerEx _scene = new SceneManagerEx();
    
    public static GameManager Game => Instance._game;
    public static ResourcesManager Resounrces => Instance._resources;
    public static SceneManagerEx Scene => Instance._scene;

    void Init()
    {

    }
}
