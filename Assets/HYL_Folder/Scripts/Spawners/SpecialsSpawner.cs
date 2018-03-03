using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsSpawner : Spawner
{
    public static SpecialsSpawner instance;

    void Awake()
    {
        #region Typical Singleton Format

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        #endregion
        spawners.Add(GetComponent<Spawner>());
    }

    void Update()
    {
        //Update out list so we know if we've reached our spawn limit
        UpdateList();
        //Sets "CanSpawn" to false
        //if certain conditions are not satisfactory, we stop spawning
        CheckEnd();

        //After all that, if we can spawn, then do it!
        SpawnEntity();
    }

    protected override void CheckEnd()
    {
        if (enemycount == spawnLim) BL_CanSpawn = false;
        if (CheckLimits()) BL_CanSpawn = false;
    }

    //Spawn a special unit at location (using ChoosePosition();)
    protected override void SpawnEntity()
    {
        if (BL_CanSpawn)
        {
            if (Time.time > cooldown + 5.0f)
            {
                for (int burstCount = 0; burstCount < enemiesPerSpawn; burstCount++)
                {
                    ChoosePosition();

                    var enemy = (GameObject)Instantiate(current.enemyPrefab, current.pos, current.rot);
                    spawnList.Add(enemy);
                }

                cooldown = Time.time;
            }
        }
        else
        {
            cooldown = Time.time + 5.0f;
        }
    }
}
