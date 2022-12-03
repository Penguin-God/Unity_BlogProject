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
    SceneManagerEx _scene = new SceneManagerEx();

    public static SceneManagerEx Scene => instance._scene;
    public static GameManager Game => instance._game;

    void Init()
    {

    }
}
