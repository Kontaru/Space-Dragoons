using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OK_PlayerTurret : MonoBehaviour {

    #region --- Variables ---

    //Finding all targets
    [Header("Viewing Cone")]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask obstacleMask;
    public List<Transform> visible_Targets = new List<Transform>();

    //Find closest target
    [Header("Target")]
    public Transform enemy;

    [Header("Bullet Params")]
    public int shotDamage;
    public float shotCooldown;
    public float currentCooldown = 0;

    #endregion

    // Use this for initialization
    void Start () {
        InvokeRepeating("FindVisibleTargets", 0f, 0.2f);
	}

    void Update()
    {
        FindClosestTarget();

        if (enemy == null) return;

        CheckRange();
        PointGun();

        if (Time.time > currentCooldown)
        {
            FireProjectile();
            currentCooldown = Time.time + shotCooldown;
        }
    }

    //Target Hunting
    void FindVisibleTargets()
    {
        //Create a temp list (so that, in between invokes, visible_Targets won't be empty)
        List<Transform> vTargets = new List<Transform>();

        //--- Add Specials

        //Check all enemies out of all enemies spawned
        foreach (GameObject vEnemy in SpecialsSpawner.instance.spawnList)
        {
            //Don't check if the enemy is null (possible if enemy dies)
            if (vEnemy == null) break;
            //Check if enemy is within range
            else if (Vector3.Distance(transform.position, vEnemy.transform.position) < viewRadius)
            {
                // For each target, get a transform and vector3 (Normalizing the vector3)
                Transform vTarget = vEnemy.transform;

                //Checking if they're within our view angles
                Vector3 dirToTarget = (vTarget.position - transform.position).normalized;
                // If target is within the viewAngle get a distance between the player and the target
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distanceTarget = Vector3.Distance(transform.position, vTarget.position);

                    // Raycast to check if the view from the target to player runs into an obsticle, if not adding the target to the list of targets
                    if (!Physics.Raycast(transform.position, dirToTarget, distanceTarget, obstacleMask))
                    {
                        vTargets.Add(vTarget);
                    }
                }
            }
        }

        //--- Add Normies

        //Check all enemies out of all enemies spawned
        foreach (GameObject vEnemy in DefaultsSpawner.instance.spawnList)
        {
            //Don't check if the enemy is null (possible if enemy dies)
            if (vEnemy == null) break;
            //Check if enemy is within range
            else if (Vector3.Distance(transform.position, vEnemy.transform.position) < viewRadius)
            {
                // For each target, get a transform and vector3 (Normalizing the vector3)
                Transform vTarget = vEnemy.transform;

                //Checking if they're within our view angles
                Vector3 dirToTarget = (vTarget.position - transform.position).normalized;
                // If target is within the viewAngle get a distance between the player and the target
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distanceTarget = Vector3.Distance(transform.position, vTarget.position);

                    // Raycast to check if the view from the target to player runs into an obsticle, if not adding the target to the list of targets
                    if (!Physics.Raycast(transform.position, dirToTarget, distanceTarget, obstacleMask))
                    {
                        vTargets.Add(vTarget);
                    }
                }
            }
        }

        visible_Targets = new List<Transform>(vTargets);
    }

    void FindClosestTarget()
    {
        if (enemy != null) return;

        //Initialise distance variables
        float curTarget = Mathf.Infinity;
        float closestTarget = Mathf.Infinity;

        foreach (Transform vEnemy in visible_Targets)
        {
            //Calculate distance from player to target
            if (vEnemy != null)
            {
                curTarget = Vector3.Distance(transform.position, vEnemy.position);
                //If this distance is the smallest, set this as our enemy
                if (curTarget < closestTarget)
                {
                    curTarget = closestTarget;
                    enemy = vEnemy;
                }
            }
        }
    }

    void CheckRange()
    {
        //Check if enemy is within range
        if (Vector3.Distance(enemy.position, transform.position) > viewRadius + 2.0f) enemy = null;
    }

    void PointGun()
    {
        Transform Child;
        Child = transform.GetChild(0).transform;

        if (enemy != null)
        {
            Vector3 relativePos = enemy.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Child.rotation = rotation;
        }

    }

    void FireProjectile()
    {
        //Deal damage using message sender
        AudioManager.instance.PlayOverlap("Player Shoot");
        enemy.gameObject.SendMessage("TakeDamage", shotDamage, SendMessageOptions.DontRequireReceiver);
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
