using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int needMinimalPlace = 2;

    public Color playerColor;
    public ResultController resultController;

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
        if (canControll)
        {
            canControll = false;

            VehicleBehaviour.WheelVehicle[] vehicles = FindObjectsOfType<VehicleBehaviour.WheelVehicle>();

            foreach (var vehicle in vehicles)
            {
                if (vehicle.IsPlayer)
                {
                    playerColor = vehicle.GetBrushColor();
                }
            }

            if (resultController.GetPlaceByColor(playerColor) <= needMinimalPlace)
            {
                AudioController.Instance.PlaySFX("Win");
                uiController.Win();
                saveController.SaveTheAchievedLevel();
            }
            else
            {
                AudioController.Instance.PlaySFX("Lose");
                uiController.Lose();
            }
        }
    }
}
