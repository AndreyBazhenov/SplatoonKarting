using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    public List<Color> test;

    public List<float> percents = new List<float>();

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckResult(test);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Test();
        }
	}

    public void Test()
    {
        Debug.LogError("PlayerColor  " + test[0]);

        Texture2D texture = GetArenaTexture();

        Color[] pixels = texture.GetPixels();

        foreach (var pixel in pixels)
        {
            Debug.LogError("Other = " + pixel);
        }
    }

	public List<float> CheckResult(List<Color> colors)
    {
        percents.Clear();

        Texture2D texture = GetArenaTexture();

        Color[] pixels = texture.GetPixels();

        int[] counters = new int[colors.Count];

		foreach (var pixel in pixels)
		{
			for (int i = 0; i < colors.Count; i++)
			{
                if (ColorEquals(pixel, colors[i], 0.001f))
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
            percents.Add((float)counters[i] / (float)counter);
        }

        return percents;
    }

    private Texture2D GetArenaTexture()
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        Rect rectReadPicture = new Rect(0, 0, renderTexture.width, renderTexture.height);

        RenderTexture.active = renderTexture;

        // Read pixels
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
