﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OK_PlayerMovement : Entity {

    //Instance our player
    public static OK_PlayerMovement instance;

    // Variables
    private NavMeshAgent nma_Agent;

    #region Typical Singleton Format

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    #endregion

    // Use this for initialization
    void Start () {

        // Set variable gameobjects and set a static speed variable
        nma_Agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        MoveOnPoint();
	}

    void MoveOnPoint()
    {
        // The ship is constantly moving, transform.Translate works better than using a rigidbody due to jitters
        transform.Translate(Vector3.forward * Time.deltaTime * nma_Agent.speed);

        // Player Input click raycast formed
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                // Navmesh agent moves to player clicked position
                nma_Agent.destination = hit.point;

            }
        }

        if(transform.hasChanged)
        {
            AudioManager.instance.Play("Engine");
        }else
        {
            AudioManager.instance.Stop("Engine");
        }
    }
}
