using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CreatureSpawnInspector))]
public class CreatureSpawnInspectorDrawer : Editor
{
    CreatureSpawnInspector _target;

    void OnEnable()
    {
        _target = (CreatureSpawnInspector)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSpawnButtons();
    }

    void DrawSpawnButtons()
    {
        foreach (UnitColor unitColor in Enum.GetValues(typeof(UnitColor)))
        {
            foreach (UnitClass unitClass in Enum.GetValues(typeof(UnitClass)))
            {
                if (GUILayout.Button($"{Enum.GetName(typeof(UnitColor), unitColor) } {Enum.GetName(typeof(UnitClass), unitClass)} Spawn!!"))
                    _target.SpanwUnit(new UnitFlags(unitColor, unitClass));
                GUILayout.Space(10);
            }
        }
    }
}
