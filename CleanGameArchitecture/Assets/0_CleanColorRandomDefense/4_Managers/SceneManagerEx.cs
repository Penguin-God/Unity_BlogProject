using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagerEx : MonoBehaviour
{
    public enum SceneType
    {
        Battle,
    }

    public class Scene_Manager
    {
        public BaseScene CurrentScene => GameObject.FindObjectOfType<BaseScene>();
        public SceneType CurrentSceneType;
       
        public void LoadScene(SceneType type)
        {
            Multi_Managers.Clear();
            SceneManager.LoadScene(Enum.GetName(typeof(SceneType), type));
            CurrentSceneType = type;
        }

        public void Clear() => CurrentScene?.Clear();
    }

}
