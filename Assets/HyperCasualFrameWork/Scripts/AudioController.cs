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
}
