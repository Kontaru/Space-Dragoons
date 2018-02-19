using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OK_MenuManager : MonoBehaviour {

    private void Start()
    {
        AudioManager.instance.Play("Main Menu");
    }

    public void SceneLoad(string st_SceneName)
    {
        SceneManager.LoadScene(st_SceneName);
    }
    public void ExitAp()
    {
        Application.Quit();
    }
}
