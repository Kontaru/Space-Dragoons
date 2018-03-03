using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Spawner : MonoBehaviour {

    //Spawner Type
    public enum Type
    {
        Basic,
        Specials
    }
    public Type spawnerType;
    public static List<Spawner> spawners = new List<Spawner>();

    //Things we have spawned
    public List<GameObject> spawnList;
    public List<SpawnObject> spawns;

    //The current thing we're about to spawn
    protected SpawnObject current;
    protected SpawnObject newspawn;

    //Some variables for spawn behaviour
    protected int enemiesPerSpawn = 1;
    [Header("Spawn Behaviour")]
    public int spawnLim = 20;
    protected int enemycount = 0;

    public bool BL_CanSpawn = false;
    public float FL_spawnDelay = 0;
    protected float cooldown = 0;

    //Check if we reached the spawn cap
    protected bool CheckLimits()
    {
        foreach (SpawnObject spawner in spawns)
        {
            if (spawner.BL_hasSpawned == false)
                return false;
        }

        return true;
    }

    //Choose a position amongst spawners on the map
    protected void ChoosePosition()
    {
        newspawn = spawns[Random.Range(0, spawns.Count)];
        current = newspawn;
    }

    //Update our spawn list to reflect any enemy deaths that have occured thus far
    protected void UpdateList()
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            if (spawnList[i] == null)
            {
                spawnList.RemoveAt(i);
            }
        }

        enemycount = spawnList.Count;
    }

    //Destroys all enemies and clears lists
    public void ClearLists()
    {
        foreach (GameObject enemy in spawnList)
        {
            Destroy(enemy);
        }

        UpdateList();
    }

    //abstract methods are not permitted to contain anything
    //but are required to be inhereted by the child class, or else VS throws an error
    abstract protected void CheckEnd();
    abstract protected void SpawnEntity();
}
