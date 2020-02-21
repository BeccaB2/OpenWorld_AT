using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]

public class MapGenerationEditor : Editor
{
    //public bool split = GameObject.Find("Mesh").GetComponent<SplitMesh>().meshSplit;

    public override void OnInspectorGUI()
    {
        // Create a button to generate the map
        MapGenerator mapGen = (MapGenerator)target;

        // Auto updates the map when values are changed in the editor
        if (DrawDefaultInspector ())
        {
            if(mapGen.autoUpdate)
            {
                mapGen.GenerateMap();    
            }
        }

        if(GUILayout.Button ("Generate Map"))
        {
           mapGen.GenerateMap();           
        }
    }
}
