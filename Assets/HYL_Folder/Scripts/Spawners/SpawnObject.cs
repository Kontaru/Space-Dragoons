using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject enemyPrefab;
    public Vector3 pos;
    public Quaternion rot;

    public Spawner.Type enemyType;

    [Header("Delayed Spawning")]
    public bool BL_hasSpawned = false;
    [HideInInspector] public bool BL_delayMySpawn = false;
    [HideInInspector] public bool BL_spawnOnce = false;
    [HideInInspector] public float FL_spawnDelay = 15.0f;

    // Use this for initialization
    void Start()
    {
        foreach (Spawner spawner in Spawner.spawners)
        {
            if (enemyType == spawner.spawnerType)
                spawner.spawns.Add(this);
        }

        pos = transform.position;
        rot = transform.rotation;
    }

    void Update()
    {
        if (BL_delayMySpawn && BL_hasSpawned && !BL_spawnOnce)
            StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(FL_spawnDelay);
        BL_hasSpawned = false;
    }
}
