using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct sfxData
{
    public string name;
    public AudioClip clip;
}

public class AudioController : MonoBehaviour
{
    static AudioController instance;

    public static AudioController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<AudioController>();
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



    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] bgClips;
    [SerializeField] private sfxData[] sfxDatas;
    

    public static bool canPlayBG;

	private void Start()
	{
        if (canPlayBG)
        {
            audioSource.loop = true;
            audioSource.clip = bgClips[Random.Range(0, bgClips.Length)];
            audioSource.Play();
        }
	}
    void OnApplicationPause(bool pauseStatus)
    {
        if (canPlayBG && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = bgClips[Random.Range(0, bgClips.Length)];
            audioSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
		foreach (var soundEffect in sfxDatas)
		{
            if (soundEffect.name == name)
            {
                audioSource.PlayOneShot(soundEffect.clip);
            }
		}
	}
    public void PlaySFX(string name, Vector3 pos)
    {
        foreach (var soundEffect in sfxDatas)
        {
            if (soundEffect.name == name)
            {
                //AudioSource.PlayClipAtPoint(soundEffect.clip, pos,10f);
                audioSource.PlayOneShot(soundEffect.clip, 0.5f);
            }
        }
    }
}
