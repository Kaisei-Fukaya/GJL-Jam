using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashingLight : MonoBehaviour
{
    Light _light;

    [SerializeField] float _minIntensity;
    [SerializeField] float _maxIntensity;
    [SerializeField] float _timing;

    bool _direction;

    private void Start()
    {
        _light = GetComponent<Light>();
    }

    //Ping pong the intensity over time
    private void Update()
    {
        if (_direction)
        {
            if (_light.intensity < _maxIntensity - 0.01f)
            {
                _light.intensity = Mathf.Lerp(_light.intensity, _maxIntensity, _timing * Time.deltaTime);
            }
            else
            {
                _direction = false;
            }
        }
        else
        {
            if (_light.intensity > _minIntensity + 0.01f)
            {
                _light.intensity = Mathf.Lerp(_light.intensity, _minIntensity, _timing * Time.deltaTime);
            }
            else
            {
                _direction = true;
            }
        }
    }
}
