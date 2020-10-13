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
    }

    public void SetVignetteIntensity(float value)
    {
        _vignette.intensity.value = value;
    }


}
