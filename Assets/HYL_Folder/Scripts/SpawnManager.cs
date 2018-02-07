using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject[] spawnList;
    public List<SpawnObject> spawns = new List<SpawnObject>(0);
    SpawnObject current;
    SpawnObject newspawn;

    float cooldown = 0;
    bool allLimit = true;

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

        if (Time.time > cooldown + 5.0f)
        {

            ChoosePosition();

            var enemy = (GameObject)Instantiate(current.enemyPrefab, current.pos, current.rot);

            for (int i = 0; i < spawnList.Length; i++)
            {
                if (spawnList[i] == null)
                    spawnList[i] = enemy;
            }

            cooldown = Time.time;
        }
    }

    void ChoosePosition()
    {
        newspawn = spawns[Random.Range(0, spawns.Count)];
        current = newspawn;
    }
}
