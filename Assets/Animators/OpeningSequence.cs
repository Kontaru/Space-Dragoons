using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public TextAnimator anim;
    public int spawnCount;
    public int curCount;
}

public class OpeningSequence : MonoBehaviour {

    public Wave[] waves;
    public int waveCount;
    OK_PlayerMovement player;
    DefaultsSpawner defaultSpawner;
    SpecialsSpawner specialSpawner;

    // Use this for initialization
    void Start () {

        GrabReferences();
        DisableScripts();
        StartCoroutine(CinematicMove());
	}
	
	// Update is called once per frame
	void Update () {

        if(player.enabled == false)
        {
            player.transform.Translate(Vector3.forward * Time.deltaTime * 8.0f);
        }

	    if(waves[waveCount].anim.Play == false)
        {
            EnableScripts();
            defaultSpawner.BL_CanSpawn = true;
            specialSpawner.BL_CanSpawn = true;
        }
	}

    //Grab References
    void GrabReferences()
    {
        player = OK_PlayerMovement.instance;
        defaultSpawner = DefaultsSpawner.instance;
        specialSpawner = SpecialsSpawner.instance;
    }
    
    //Disable Scripts
    void DisableScripts()
    {
        player.enabled = false;
        defaultSpawner.enabled = false;
        specialSpawner.enabled = false;
    }

    //Enable Scripts
    void EnableScripts()
    {
        player.enabled = true;
        defaultSpawner.enabled = true;
        specialSpawner.enabled = true;
    }

    //Cinematic Move Player
    IEnumerator CinematicMove()
    {
        yield return new WaitForSeconds(2.0f);
        player.enabled = true;
        CameraFollow.instance.tracking = true;
    }
}
