using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ResultController : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;

    [SerializeField]
    public RaceData[] raceDatas;

    [SerializeField]
    Shader shader;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Vector2 waypointCount;
    [SerializeField]
    GameObject waypointPrefab;
    [SerializeField]
    private UIResultPanel resultPanel;
    private float timer;
    private float needTime = 0.5f;

    private void Start()
	{
        var unlitShader = Shader.Find("Unlit/Texture");
        cam.SetReplacementShader(unlitShader, "");
    }

	private void FixedUpdate()
    {
        if (timer < needTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            CheckResult();
            resultPanel.UpdateRaceInfo(raceDatas);
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SpawnWayPoints();
        }
	}

    public int GetPlaceByColor(Color needColor)
    {
        int place = 0;

        raceDatas = raceDatas.OrderByDescending(x => x.percent).ToArray();

        for (int i = 0; i < raceDatas.Length; i++)
		{
            if (needColor == raceDatas[i].color)
            {
                place = i + 1;
            }
		}
        Debug.LogError("Place = " + place);
        return place;
    }

    public void SpawnWayPoints()
    {
        Debug.LogError("Test  " + cam.pixelWidth+"x"+cam.pixelHeight);

        Vector2 stepSize = new Vector2(cam.pixelWidth / waypointCount.x, cam.pixelHeight / waypointCount.y);

		for (int x = 0; x < waypointCount.x; x++)
		{
			for (int y = 0; y < waypointCount.y; y++)
			{
                Vector3 scrPoint = new Vector3((stepSize.x/2 + stepSize.x* x), (stepSize.y / 2 + stepSize.y * y), 0);
                Ray ray = cam.ScreenPointToRay(scrPoint);
                RaycastHit hit;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000))
                {
                    Instantiate(waypointPrefab, hit.point, Quaternion.identity);
                }
            }
		}        
    }

	public void CheckResult()
    {

        Texture2D texture = GetArenaTexture();

        Color[] pixels = texture.GetPixels();

        int[] counters = new int[raceDatas.Length];

		foreach (var pixel in pixels)
		{
			for (int i = 0; i < raceDatas.Length; i++)
			{
                if (ColorEquals(pixel, raceDatas[i].color, 0.001f))
                {
                    counters[i]++;
                }
			}
		}

        int counter = 0;
        for (int i = 0; i < counters.Length; i++)
        {
            counter += counters[i];
        }

        for (int i = 0; i < counters.Length; i++)
        {
            raceDatas[i].percent = ((float)counters[i] / (float)counter);
        }
    }

    private Texture2D GetArenaTexture()
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        Rect rectReadPicture = new Rect(0, 0, renderTexture.width, renderTexture.height);

        RenderTexture.active = renderTexture;

        texture.ReadPixels(rectReadPicture, 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        return texture;
    }

    private bool ColorEquals(Color color1, Color color2, float threshhold = 0.01f)
    {
        bool redFlag = false;
        bool greenFlag = false;
        bool blueFlag = false;

		if (color1.r >= color2.r-threshhold && color1.r <= color2.r + threshhold)
		{
            redFlag = true;

        }

        if (color1.g >= color2.g - threshhold && color1.g <= color2.g + threshhold)
        {
            greenFlag = true;

        }

        if (color1.b >= color2.b - threshhold && color1.b <= color2.b + threshhold)
        {
            blueFlag = true;

        }

        return redFlag && greenFlag && blueFlag;
    }
}
