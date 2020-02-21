using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplitMesh))]
public class SplitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Create a button to generate the map
        SplitMesh splitMap = (SplitMesh)target;

        if (splitMap.childen != null && splitMap.childen.Count != 0)
            EditorGUILayout.HelpBox("Submesh count: " + splitMap.childen.Count, MessageType.Info, true);
        else
            EditorGUILayout.HelpBox("Submesh count: none", MessageType.Info, true);

        DrawDefaultInspector();

        if (GUILayout.Button("Split"))
        {
            splitMap.Split();
        }

        if (GUILayout.Button("Clear"))
        {
            splitMap.Clear();
        }
    }
}
