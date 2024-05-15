using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapThreeD))]
public class TileMapThreeDEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileMapThreeD myScript = (TileMapThreeD)target;
        if (GUILayout.Button("Update Positions"))
        {
            myScript.UpdatePositions();
        }
        if (GUILayout.Button("Get Tiles"))
        {
            myScript.GetTiles();
        }
    }
}
