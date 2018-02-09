using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OK_PlayerTurret : MonoBehaviour {

    #region --- Variables ---
    //set variables
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visable_Targets = new List<Transform>();

    #endregion

    // Use this for initialization
    void Start () {
        //Start FindTargetsWithDelay every 0.2 seconds
        StartCoroutine("FindTargetsWithDelay", 0.2f );
	}

    IEnumerable FindTargetsWithDelay(float delay)
    {
        //When true wait for 0.2 seconds then call FindVisiableTargets
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        // Clear List
        visable_Targets.Clear();

        // Create new list of colliders that overlap with the viewRadius sphere
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position,viewRadius,targetMask);

        // For each target, get a transform and vector3 (Normalizing the vector3)
        for (int numOfTargets = 0; numOfTargets <targetsInViewRadius.Length; numOfTargets++)
        {
            Transform target = targetsInViewRadius [numOfTargets].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // If target is within the viewAngle get a distance between the player and the target
            if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle/ 2)
            {
                float distanceTarget = Vector3.Distance (transform.position, target.position);
                
                // Raycast to check if the view from the target to player runs into an obsticle, if not adding the target to the list of targets
                if (!Physics.Raycast(transform.position,dirToTarget,distanceTarget, obstacleMask))
                {
                    visable_Targets.Add(target);
                }
            }
        }


    }

    // Setting the DirFromAngle to forward of the gameobject using trigonometery
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


    
}
