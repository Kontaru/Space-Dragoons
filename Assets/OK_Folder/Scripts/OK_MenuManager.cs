using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OK_MenuManager : MonoBehaviour {

    public void SceneLoad(string st_SceneName)
    {
        SceneManager.LoadScene(st_SceneName);
    }
    public void ExitAp()
    {
        Application.Quit();
    }
}
