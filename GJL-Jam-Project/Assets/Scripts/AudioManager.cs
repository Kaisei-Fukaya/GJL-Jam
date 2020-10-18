using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

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

    const string MusicVolumeKey = "musicVolume";
    const string SFXVolumeKey = "sfxVolume";

    public AudioMixerGroup musicBus, sfxBus;
    AudioSource _defaultAudioSource;

    [SerializeField] Slider musicVolSlider;
    [SerializeField] Slider sfxVolSlider;

    float _musicVolume;
    public float MusicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            _musicVolume = value;
            Settings.Instance.SaveFloatValue(MusicVolumeKey, _musicVolume);
        }
    }
    float _sfxVolume;
    public float SFXVolume
    {
        get
        {
            return _sfxVolume;
        }
        set
        {
            _sfxVolume = value;
            Settings.Instance.SaveFloatValue(SFXVolumeKey, _sfxVolume);
        }
    }


    private void Start()
    {
        Initialize();
    }


    public void Initialize()
    {
        _defaultAudioSource = GetComponent<AudioSource>();
        //Load Audio Settings
        MusicVolume =  Settings.Instance.LoadFloatValue(MusicVolumeKey);
        SFXVolume = Settings.Instance.LoadFloatValue(SFXVolumeKey);

        //Set slider values
        musicVolSlider.value = MusicVolume;
        sfxVolSlider.value = SFXVolume;

        //Set volume
        musicBus.audioMixer.SetFloat("MusicVol", MusicVolume);
        sfxBus.audioMixer.SetFloat("SFXVol", SFXVolume);

        //Add event listeners to sliders
        musicVolSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void PlayOneShot(AudioClip clip, float volumeScale)
    {
        _defaultAudioSource.PlayOneShot(clip, volumeScale);
    }

    public AudioSource SetUpAudioSource(GameObject owner, AudioClip clip, bool isMusic = false, bool playInstantly = true, bool limitToOneSource = true, float volume = 1f, float spacialBlend = 0f)
    {
        AudioSource source;
        if (limitToOneSource)
        {
            source = owner.GetComponent<AudioSource>();
            if (source == null)
            {
                source = owner.AddComponent<AudioSource>();
            }
        }
        else
        {
            //Use an source that is not playing, if all playing then use the first one
            var sources = owner.GetComponents<AudioSource>();
            source = sources[0];
            foreach (var s in sources)
            {
                if (!s.isPlaying)
                {
                    source = s;
                    break;
                }
            }
        }
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = spacialBlend;
        if (isMusic)
        {
            source.outputAudioMixerGroup = musicBus;
        }
        else
        {
            source.outputAudioMixerGroup = sfxBus;
        }

        if (playInstantly)
        {
            source.Play();
        }

        return source;
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        musicBus.audioMixer.SetFloat("MusicVol", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        sfxBus.audioMixer.SetFloat("SFXVol", volume);
    }

    internal void SetUpAudioSource(object footstepObject, object slideSound)
    {
        throw new NotImplementedException();
    }

    internal void SetUpAudioSource(GameObject footstepObject, object slideSound)
    {
        throw new NotImplementedException();
    }
}
