using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public WaveSequence.EnumWaves myWave;
    public TextAnimator anim;
    public int killsRequired;
    public int killCount;
}

public class WaveSequence : MonoBehaviour {

    public static WaveSequence instance;
    public Wave[] Waves;
    Wave CurrentWave;
    [SerializeField] int IN_waveCount = 0;

    public enum EnumWaves
    {
        WaveOne,
        WaveTwo,
        WaveThree
    }

    public EnumWaves currentWave;

    OK_PlayerMovement player;
    DefaultsSpawner defaultSpawner;
    SpecialsSpawner specialSpawner;

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
    }

    // Use this for initialization
    void Start () {

        //Setup
        GrabReferences();
        player.enabled = false;
        DisableSpawners();
        CurrentWave = Waves[IN_waveCount];
        StartCoroutine(EnableWave(CurrentWave));

        //Animations
        PreWaveAnimation(Waves[0]);
        StartCoroutine(FlyIntoScene());
	}

    // Update is called once per frame
    void Update () {

        WaveCondition();
    }

    public void RegisterKill(int amount)
    {
        CurrentWave.killCount += amount;
    }

    void WaveCondition()
    {
        if (currentWave == EnumWaves.WaveOne)
        {
            if(Waves[0].killCount >= Waves[0].killsRequired)
            {
                CurrentWave = Waves[1];
                currentWave = EnumWaves.WaveTwo;
                EndWave();
            }
        }
        else if (currentWave == EnumWaves.WaveTwo)
        {
            if (Waves[1].killCount >= Waves[1].killsRequired)
            {
                CurrentWave = Waves[2];
                currentWave = EnumWaves.WaveThree;
                EndWave();
            }
        }
    }

    void EndWave()
    {
        PreWaveAnimation(CurrentWave);
        StartCoroutine(EnableWave(CurrentWave));
    }

    #region "EndWave" functions

    //When a new wave begins, do an animation before we begin the wave
    void PreWaveAnimation(Wave vWave)
    {
        DisableSpawners();
        vWave.anim.Play = true;
    }

    //Enabled the wave
    IEnumerator EnableWave(Wave vWave)
    {
        while(vWave.anim.Play == false)
        {
            yield return null;
        }
        EnabledSpawners();
    }

    #endregion

    //Disable Scripts
    void DisableSpawners()
    {
        defaultSpawner.BL_CanSpawn = true;
        specialSpawner.BL_CanSpawn = true;

        defaultSpawner.ClearLists();
        specialSpawner.ClearLists();

        defaultSpawner.enabled = false;
        specialSpawner.enabled = false;
    }

    //Enable Scripts
    void EnabledSpawners()
    {
        defaultSpawner.enabled = true;
        specialSpawner.enabled = true;

        defaultSpawner.BL_CanSpawn = true;
        specialSpawner.BL_CanSpawn = true;
    }

    #region On Scene Initialise

    //Grab References
    void GrabReferences()
    {
        player = OK_PlayerMovement.instance;
        defaultSpawner = DefaultsSpawner.instance;
        specialSpawner = SpecialsSpawner.instance;
    }

    //Cinematic Move Player
    IEnumerator FlyIntoScene()
    {
        float start = Time.timeSinceLevelLoad;

        while (Time.timeSinceLevelLoad < start + 2.0f)
        {
            if (player.enabled == false)
            {
                player.transform.Translate(Vector3.forward * Time.deltaTime * 8.0f);
            }
            yield return null;
        }

        player.enabled = true;
        CameraFollow.instance.tracking = true;
    }

    #endregion
}
