using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct RaceData
{
    public PaintIn3D.Examples.P3dColor colorPainter;
    public Mesh mesh;
    public float percent;
}

public class UIResultPanel : MonoBehaviour
{
    public VerticalLayoutGroup verticalLayout;

    public Image[] imgBGRacers;
    public Image[] imgRacers;
    public Image[] imgFGRacers;

	private void Start()
	{
        verticalLayout.spacing = -0.1024652f * GetComponent<Image>().rectTransform.rect.height;
    }

	public void UpdateRaceInfo(RaceData[] raceDatas)
    {
        raceDatas = raceDatas.OrderByDescending(x => x.percent).ToArray();

        for (int i = 0; i < (imgRacers.Length < raceDatas.Length? imgRacers.Length:raceDatas.Length); i++)
		{
            imgRacers[i].color = raceDatas[i].colorPainter.Color;
            imgRacers[i].fillAmount = raceDatas[i].percent;
            imgFGRacers[i].fillAmount = raceDatas[i].percent + 0.02f;
            imgBGRacers[i].fillAmount = raceDatas[i].percent + 0.02f;

        }
    }
}
