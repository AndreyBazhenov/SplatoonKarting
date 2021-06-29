using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct RaceData
{
    public Color color;
    public float percent;
}

public class UIResultPanel : MonoBehaviour
{
    public Image[] imgRacers;


    public void UpdateRaceInfo(RaceData[] raceDatas)
    {
        raceDatas = raceDatas.OrderByDescending(x => x.percent).ToArray();

        for (int i = 0; i < (imgRacers.Length < raceDatas.Length? imgRacers.Length:raceDatas.Length); i++)
		{
            imgRacers[i].color = raceDatas[i].color;
            imgRacers[i].fillAmount = raceDatas[i].percent;
        }
    }
}
