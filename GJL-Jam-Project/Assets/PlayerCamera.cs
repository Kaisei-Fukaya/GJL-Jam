﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerCamera : MonoBehaviour
{
    public float cameraSensitivity;

    [SerializeField] Camera _camera;
    [SerializeField] float _minFov = 75f;
    [SerializeField] float _maxFov = 110f;
    [SerializeField] float _tiltSpeed = 10f;
    PlayerInput _pInput;
    Rigidbody _rb;
    Vector3 _currentPlayerRotation, _currentCameraRotation;
    float _tiltAngleTarget = 0f;

    public static PlayerCamera Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
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
        _pInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Set Y rotation of character
        _currentPlayerRotation = _rb.rotation.eulerAngles;
        _currentPlayerRotation.y += _pInput.CameraInput.y * cameraSensitivity;
        _rb.rotation = Quaternion.Euler(_currentPlayerRotation);

        //Set X rotation of camera
        _currentCameraRotation.x += _pInput.CameraInput.x * cameraSensitivity;
        _currentCameraRotation.x = Mathf.Clamp(_currentCameraRotation.x, -85f, 85f);

        //Handle Camera Tilting
        _currentCameraRotation.z = Mathf.Lerp(_currentCameraRotation.z, _tiltAngleTarget, Time.deltaTime * _tiltSpeed);

        //Assign rotation to camera
        _camera.transform.localRotation = Quaternion.Euler(_currentCameraRotation);
    }

    public void SetFOV(float newFOV)
    {
        if(newFOV > _maxFov)
        {
            _camera.fieldOfView = _maxFov;
        }
        else if(newFOV < _minFov)
        {
            _camera.fieldOfView = _minFov;
        }
        else
        {
            _camera.fieldOfView = newFOV;
        }
    }

    internal void SetCameraTilt(object p)
    {
        throw new NotImplementedException();
    }

    public void MomentumFOVShift(float percentageValue)
    {
        float range = _maxFov - _minFov;
        SetFOV(_minFov + (range * percentageValue));
    }

    public void SetCameraTilt(float targetXRotation)
    {
        _tiltAngleTarget = targetXRotation;
    }
}
