using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

public class TestingInspectorDrawer : Editor
{
    protected object _target;

    void OnEnable() => _target = Target;
    protected virtual object Target => target;

    protected void DrawTestButtons(object target)
    {
        foreach (var method in target.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (method.Name.StartsWith("Test") == false) continue;

            if (GUILayout.Button(method.Name, GUILayout.Height(20)))
                method.Invoke(target, new object[] { });
            GUILayout.Space(7);
        }
    }
}


[CustomEditor(typeof(Tester))]
public class PureObjectTestDrawer : TestingInspectorDrawer
{
    protected override object Target => (Tester)base.Target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawTestButtons(_target);
    }
}

[CustomEditor(typeof(MonoBehaviourTestExecuter))]
public class MonoBehaviourTestDrawer : TestingInspectorDrawer
{
    protected override object Target => (MonoBehaviourTestExecuter)base.Target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawTestButtons(_target);
    }
}

