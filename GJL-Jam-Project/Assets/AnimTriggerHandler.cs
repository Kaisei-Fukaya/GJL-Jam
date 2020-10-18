using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] _footstepSounds;
    [SerializeField] GameObject _footstepObject;
    AudioManager _audioManager;


    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    public void TriggerFootstep()
    {
        _audioManager.SetUpAudioSource(_footstepObject, _footstepSounds[Random.Range(0, _footstepSounds.Length)], limitToOneSource: false, volume: 0.5f);
    }
}
