using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] AudioSource[] musicSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayMusic(int track)
    {
        for (int i = 0; i < musicSources.Length; i++)
        {
            if(i == track)
            {
                musicSources[i].Play();
            }
            else
            {
                musicSources[i].Stop();
            }
        }
    }
}
