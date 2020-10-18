using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public static PostProcessingController Instance { get; private set; }
    [SerializeField] Volume _volume;
    Vignette _vignette;
    FilmGrain _noise;
    ChromaticAberration _chromaticAberration;

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

    private void Start()
    {
        //Cache PP parameters
        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _chromaticAberration);
        _volume.profile.TryGet(out _noise);
    }

    public void SetVignetteIntensity(float value)
    {
        _vignette.intensity.value = value;
    }

    public void SetVignetteColour(Color colour)
    {
        _vignette.color.value = colour;
    }

    public void SetNoiseIntensity(float value)
    {
        _noise.intensity.value = value;
    }

    public void SetChromaticAberrationIntensity(float value)
    {
        _chromaticAberration.intensity.value = value;
    }

}
