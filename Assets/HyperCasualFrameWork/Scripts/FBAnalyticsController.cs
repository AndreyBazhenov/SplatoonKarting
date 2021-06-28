using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//using Facebook.Unity;

public class FBAnalyticsController : MonoBehaviour
{
    /*
    private static int startedLevelInSessionCounter;
    // Start is called before the first frame update
    void Start()
    {
        startedLevelInSessionCounter++;
        LogLevelStartEvent(SceneManager.GetActiveScene().buildIndex+1);
    }


    void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() => {
                FB.ActivateApp();
            });
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() => {
                    FB.ActivateApp();
                });
            }
        }
    }

    public void LogLevelStartEvent(double valToSum)
    {
        FB.LogAppEvent(
            "LevelStart",
            (float)valToSum
        );
    }

    public void LogLevelFinishEvent(string result, int time, int tapCount, int vibroCount, int vibroTime, double valToSum)
    {
        var parameters = new Dictionary<string, object>();
        parameters["Result"] = result;
        parameters["Time"] = time;
        parameters["TapCount"] = tapCount;
        parameters["VibroCount"] = vibroCount;
        parameters["VibroTime"] = vibroTime;
        FB.LogAppEvent(
            "LevelFinish",
            (float)valToSum,
            parameters
        );
    }
    public void LogCountStartedLevelInSessionEvent(double valToSum)
    {
        FB.LogAppEvent(
            "CountStartedLevelInSession",
            (float)valToSum
        );
    }


    private void OnApplicationQuit()
	{
        LogCountStartedLevelInSessionEvent(startedLevelInSessionCounter);
    }*/
}
