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

    public bool BL_CanSpawn = false;
    float cooldown = 0;

    public int spawnLimit = 0;
    int IN_SpawnedCount;

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

    void Update()
    {
        if (BL_CanSpawn)
        {
            if (Time.time > cooldown + 5.0f)
                SpawnSpecials();

            UpdateList();
        }else
        {
            cooldown = Time.time + 5.0f;
        }
    }

    //Spawn a special unit at location (using ChoosePosition();)
    void SpawnSpecials()
    {
        ChoosePosition();

        var enemy = (GameObject)Instantiate(current.enemyPrefab, current.pos, current.rot);
        spawnList.Add(enemy);
        cooldown = Time.time;
    }

    //Choose a position amongst spawners on the map
    void ChoosePosition()
    {
        newspawn = spawns[Random.Range(0, spawns.Count)];
        current = newspawn;
    }

    //Update our spawn list to reflect any enemy deaths that have occured thus far
    void UpdateList()
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            if (spawnList[i] == null)
            {
                spawnList.RemoveAt(i);
            }
        }
    }
}
