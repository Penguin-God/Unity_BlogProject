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
        
    }

    void DrawTestButtons()
    {
        // if (GUILayout.Button())
    }
}
