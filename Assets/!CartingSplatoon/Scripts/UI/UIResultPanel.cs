using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct RaceData
{
    public Color color;
    public PaintIn3D.Examples.P3dColor colorPainter;
    public Mesh mesh;
    public float percent;
}

public class UIResultPanel : MonoBehaviour
{
    public Image[] imgBGRacers;
    public Image[] imgRacers;
    public Image[] imgFGRacers;


    public void UpdateRaceInfo(RaceData[] raceDatas)
    {
        raceDatas = raceDatas.OrderByDescending(x => x.percent).ToArray();

        for (int i = 0; i < (imgRacers.Length < raceDatas.Length? imgRacers.Length:raceDatas.Length); i++)
		{
            imgRacers[i].color = raceDatas[i].color;
            imgRacers[i].fillAmount = raceDatas[i].percent;
            imgFGRacers[i].fillAmount = raceDatas[i].percent;
            imgBGRacers[i].fillAmount = raceDatas[i].percent;

        }
    }
}
