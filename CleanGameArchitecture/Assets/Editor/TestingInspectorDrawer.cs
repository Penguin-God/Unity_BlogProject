using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(Tester))]
public class TestingInspectorDrawer : Editor
{
    Tester userSkillTest;
    private void OnEnable()
    {
        userSkillTest = (Tester)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawTestButtons();
    }

    void DrawTestButtons()
    {
        foreach (var method in typeof(Tester).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (method.Name.StartsWith("Test") == false) continue;

            if (GUILayout.Button(method.Name, GUILayout.Height(20)))
            {
                method.Invoke(userSkillTest, new object[] { });
            }
            GUILayout.Space(7);
        }
    }
}
