﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextAreasTrigger : MonoBehaviour
{
    AreaLoadManager _areaLoadManager;

    public AreaMountPoint[] mountPoints;

    public GameObject invisWall;

    bool _hasTriggered = false;

    [SerializeField] string[] _messages;

    [SerializeField] GameObject _nextArea;

    private void Start()
    {
        invisWall.SetActive(false);
        _areaLoadManager = AreaLoadManager.Instance;
        //print(_areaLoadManager + " initialised");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TriggerLoad();
        }
    }

    void IncrementNoise()
    {
        var pp = PostProcessingController.Instance;
        pp.SetNoiseIntensity(pp.GetNoiseIntensity() + 0.02f);
    }

    private void TriggerLoad()
    {
        if (!_hasTriggered)
        {
            //print("triggerLoad " + enabled);
            foreach (var m in mountPoints)
            {
                _areaLoadManager.LoadNextArea(m, _nextArea);
            }

            //Enable invis wall to stop backtracking
            invisWall.SetActive(true);

            IncrementNoise();

            if (_messages.Length != 0)
            {
                MessageToPlayer.Instance.DisplayMessageForSetTime(_messages, 2f, 2f, 10f);
            }

            _hasTriggered = true;                        //Disable self
        }
    }
}
