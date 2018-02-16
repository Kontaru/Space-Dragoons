using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class OK_AdManager : MonoBehaviour
{


    public void ShowAd(bool bl_Play)
    {
        if (bl_Play == true)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResult });
            }
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Player Watched Ad");
                break;
            case ShowResult.Skipped:
                Debug.Log("Player Skipped ad");
                break;
            case ShowResult.Failed:
                Debug.Log("Player Failed to launch ad");
                break;
        }
    }
}
