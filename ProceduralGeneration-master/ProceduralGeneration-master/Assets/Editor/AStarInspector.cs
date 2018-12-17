using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Grid))]
public class AStarInspector : Editor
{

    public override void OnInspectorGUI()
    {

        Grid grid = (Grid)target;

        if (DrawDefaultInspector())
        {
            if (grid.autoUpdate)
            {
                grid.GridCreater();
            }
        }

        if (GUILayout.Button("Create Grid"))
        {
            grid.GridCreater();
        }

    }
}
