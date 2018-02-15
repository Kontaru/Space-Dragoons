using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsSpawner : MonoBehaviour
{
    public static SpecialsSpawner instance;

    public List<GameObject> spawnList;
    public List<SpawnObject> spawns;
    SpawnObject current;
    SpawnObject newspawn;

    float cooldown = 0;
    bool allLimit = true;

    public int spawnLimit = 0;
    int IN_SpawnedCount;

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
        if (Time.time > cooldown + 5.0f) SpawnSpecials();

        foreach (GameObject enemy in spawnList)
        {
            if(enemy == null)
            {
                spawnList.Remove(enemy);
            }
        }
    }

    void SpawnSpecials()
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
