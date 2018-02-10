using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//Custom Editor script
[CustomEditor (typeof (OK_PlayerTurret))]
public class OK_FieldofView : Editor {

    // Displays different GUI lines corrisonding to different editor set variables
    private void OnSceneGUI()
    {
        //Referencing the OK_PlayerTurret to access variables
        OK_PlayerTurret fow = (OK_PlayerTurret)target;

        // Change color and draw an Arc 360 degrees around the player using viewRadius as the diameter
        Handles.color = Color.cyan;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);

        //Setting two angles based on the viewAngle variable, the same value is set to a negative for one of the variables
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        // Draw both lines up to the arc of the viewRadius from the gameobjects position.
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        // Foreach target in view draw a line in red from the player to the target
        Handles.color = Color.red;
        foreach (Transform visTarget in fow.visible_Targets)
        {
            Handles.DrawLine(fow.transform.position, visTarget.position);
        }
    }
}
