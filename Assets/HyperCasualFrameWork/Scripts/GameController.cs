using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Reciepe
{
    public string name;
    public int count;
}

public class GameController : MonoBehaviour
{

    static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<GameController>();
            }
            return instance;
        }
    }

	private void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }
    }

    public UIController uiController;
    public bool canControll;
    public SaveController saveController;
    public float timer = 30f;
    public GameObject camera1;
    public GameObject camera2;
    public int needReceptCount = 2;

    public Color playerColor;

    public void StartGame()
    {
        canControll = true;
    }

	private void Update()
	{
        if (canControll)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                canControll = false;
                Invoke("Result", 0.5f);
            }
            uiController.SetTimerData(((int)timer).ToString());
        }
	}

    public void Result()
    {
        Debug.LogError("Result");
        Invoke("CheckResult", 1f);
    }

    public void CheckResult()
    {
        if (true)
        {
            uiController.Win();
        }
        else
        {
            uiController.Lose();
        }
    }
}
