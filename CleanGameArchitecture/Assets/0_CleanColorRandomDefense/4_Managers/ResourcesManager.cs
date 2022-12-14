using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    public static T Load<T>(string path) where T : Object
    {
        Debug.Assert(Resources.Load<T>(path) != null, $"찾을 수 없는 리소스 경로 : {path}");
        return Resources.Load<T>(path);
    }

    public static GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>(GetPrefabPath(path));
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public static void Destroy(GameObject go) => Object.Destroy(go);
    static string GetPrefabPath(string path) => path.Contains("Prefabs/") ? path : $"Prefabs/{path}";
}
