using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private static bool isLoaded;
    // Start is called before the first frame update
    void Start()
    {
        if (isLoaded)
        {

        }
        else
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("CurrentLevel"));
            isLoaded = true;
            AudioController.canPlayBG = true;
        }
    }

    public void SaveTheAchievedLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex+1);
    }

    public void DestroySave()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
    }
}
