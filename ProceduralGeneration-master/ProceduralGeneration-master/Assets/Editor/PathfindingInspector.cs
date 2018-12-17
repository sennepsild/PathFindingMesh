using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Pathfinding))]
public class PathfindingInspector : Editor
{

    public override void OnInspectorGUI()
    {

        Pathfinding pathfinder = (Pathfinding)target;

        if (DrawDefaultInspector())
        {
            if (pathfinder.autoUpdate)
            {
                pathfinder.Pathfind();
            }
        }

        if (GUILayout.Button("Update"))
        {
            pathfinder.Pathfind();
        }

        

    }
}
