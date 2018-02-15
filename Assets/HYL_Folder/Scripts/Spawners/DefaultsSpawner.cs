using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultsSpawner : MonoBehaviour {

    public static DefaultsSpawner instance;

    public List<GameObject> spawnList;
    public List<SpawnObject> spawns;
    SpawnObject current;
    SpawnObject newspawn;

    float cooldown = 0;

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

    void Update()
    {
        if (Time.time > cooldown + 3.0f) SpawnDefaults();

        foreach (GameObject enemy in spawnList)
        {
            if (enemy == null)
            {
                spawnList.Remove(enemy);
            }
        }
    }

    void SpawnDefaults()
    {
        ChoosePosition();

        var enemy = (GameObject)Instantiate(current.enemyPrefab, current.pos, current.rot);
        spawnList.Add(enemy);
        cooldown = Time.time;
    }

    void ChoosePosition()
    {
        newspawn = spawns[Random.Range(0, spawns.Count)];
        current = newspawn;
    }
}
