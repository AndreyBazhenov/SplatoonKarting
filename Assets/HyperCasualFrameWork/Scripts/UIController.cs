using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    //[SerializeField] private TextMeshProUGUI txtIngameLvlName;
    [SerializeField] private TextMeshProUGUI txtTimer;



    public void SetTimerData(string count)
    {
        txtTimer.text = count;
    }

    public void Play()
    {
        inGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        GameController.Instance.StartGame();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            GameController.Instance.saveController.DestroySave();
            SceneManager.LoadSceneAsync(0);
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        inGamePanel.SetActive(false);

        GameController.Instance.canControll = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        inGamePanel.SetActive(true);
        pausePanel.SetActive(false);
        SetUpState();

        GameController.Instance.canControll = true;
        Time.timeScale = 1;
    }


    public void Lose()
    {
        StartCoroutine(LoseDelay());
    }

    IEnumerator LoseDelay()
    {
        yield return new WaitForSeconds(1f);

        SetAnalytic("Lose");

        GameController.Instance.canControll = false;
        SetUpState();
        Debug.LogError("You lose");

        //Time.timeScale = 0;

        losePanel.SetActive(true);
        inGamePanel.SetActive(false);

    }

    public void Win()
    {
        StartCoroutine(WinDelay());
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(0.5f);

        SetAnalytic("Win");

        GameController.Instance.canControll = false;
        SetUpState();
        //Time.timeScale = 0;

        Debug.LogError("You win");
        winPanel.SetActive(true);
        inGamePanel.SetActive(false);

    }

    private void SetAnalytic(string result)
    {
        //GameManager.Instance().fbAnaliticsController.LogLevelFinishEvent(result, (int)GameManager.Instance().levelTimer, GameManager.Instance().playerController.tapCounter,
        //GameManager.Instance().shakeDetect.tapVibroCounter, (int)GameManager.Instance().shakeDetect.timerVibro, SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetUpState()
    {
        foreach (var button in FindObjectsOfType<ButtonHandler>())
        {
            button.SetUpState();
        }
    }

}
