using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;


    // Loading Bars
    public GameObject loadingScreen;
    public Slider slider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(LoadAsynchronously(nextScene));
        }
    }

    public void LoadByIndex(int index)
    {
        StartCoroutine(LoadAsynchronously(index));
    }

    public void LoadByName(string name)
    {
        StartCoroutine(LoadAsynchronouslyByName(name));
    }

    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;

            if (operation.progress == 0.9f)
            {
                loadingScreen.SetActive(false);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator LoadAsynchronouslyByName(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (operation.progress == 0.9f)
                operation.allowSceneActivation = true;

            yield return null;
        }
    }

    public void PauseGame()
    {
        //BL_Pause = !BL_Pause;

        //if (BL_Pause == true)
        //    Time.timeScale = 0;
        //else
        //    Time.timeScale = 1;
    }
}
