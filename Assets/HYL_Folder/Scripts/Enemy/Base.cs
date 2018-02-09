using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base : Entity
{

    //---------------------------------------------------------------------------
    //----- Base: Base enemy. Can be used, but is moreoften used for inheritance
    //---------------------------------------------------------------------------

    #region // ------------ Variables ------------

    public GameObject bulletPrefab;
    public float shotCooldown;
    public float combatDistance;

    private static GameObject GO_Target;
    private float FL_shotCooldown;
    protected NavMeshAgent nav_Agent;

    public enum State
    {
        Hunt,
        Combat
    }

    protected State CurrentState;

    #endregion

    #region // ------------ Monobehaviour ------------

    virtual public void Start()
    {
        GO_Target = GameObject.FindGameObjectWithTag("Player");
        nav_Agent = GetComponent<NavMeshAgent>();
        CurrentState = State.Hunt;
    }

    void Update()
    {
        DecideState();
    }

    #endregion

    #region // ------------ Methods ------------

    void DecideState()
    {
        //State switching
        if (CurrentState == State.Hunt)
            Hunt();
        else if (CurrentState == State.Combat)
            Combat();
    }

    //Hunt Mode
    void Hunt()
    {
        nav_Agent.destination = GO_Target.transform.position;

        //---
        if (Vector3.Distance(transform.position, GO_Target.transform.position) < combatDistance)
        {
            nav_Agent.isStopped = true;
            CurrentState = State.Combat;
        }
    }

    virtual public void Combat()
    {
        transform.LookAt(GO_Target.transform.position);
        FireBullet();

        //---
        if (Vector3.Distance(transform.position, GO_Target.transform.position) > combatDistance + 5)
        {
            nav_Agent.isStopped = false;
            CurrentState = State.Hunt;
        }
    }

    virtual public void FireBullet()
    {
        // Create a bullet and reset the shot timer
        if (Time.time > FL_shotCooldown)
        {
            var _bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.TransformDirection(new Vector3(0, 0, 1.5F)), transform.rotation);
            FL_shotCooldown = Time.time + shotCooldown;
        }
    }

    #endregion
}
