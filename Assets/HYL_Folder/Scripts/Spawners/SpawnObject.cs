﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject enemyPrefab;
    public Vector3 pos;
    public Quaternion rot;

	// Use this for initialization
	void Start () {

        if(enemyPrefab.GetComponent<Base>().enemyType == Base.Type.Basic)
            DefaultsSpawner.instance.spawns.Add(this);
        else
            SpecialsSpawner.instance.spawns.Add(this);

        pos = transform.position;
        rot = transform.rotation;
	}

}
