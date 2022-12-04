using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    private static Managers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Managers>();
                if (instance == null)
                    instance = new GameObject("Managers").AddComponent<Managers>();

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
