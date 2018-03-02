using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OK_MenuManager : MonoBehaviour {

    public void SceneLoad(string st_SceneName)
    {
        GameManager.instance.LoadByName(st_SceneName);
    }
    public void ExitAp()
    {
        Application.Quit();
    }
}
