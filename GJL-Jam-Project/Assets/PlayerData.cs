using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public float Battery { get; private set; }
    public float Distance { get; private set; }

    [SerializeField] float _batteryMax = 100f;
    [SerializeField] float _batteryMin = 0f;
    Vector3 directionFromStart;

    [SerializeField] RectTransform _batteryGaugeFill;
    [SerializeField] GameObject _directionalMarker;
    [SerializeField] GameObject _startPoint;
    [SerializeField] TMP_Text _distanceText;


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

        Battery = _batteryMax;
    }

    private void Update()
    {
        UpdateDistance();
    }

    void UpdateDistance()
    {
        Distance = (_startPoint.transform.position - transform.position).magnitude;
        directionFromStart = (_startPoint.transform.position - transform.position).normalized;

        _directionalMarker.transform.rotation = Quaternion.LookRotation(directionFromStart);
        _distanceText.text = "Distance: " + Mathf.Round(Distance);
    }

    public void ReduceBattery(float valueReduction)
    {
        Battery -= valueReduction;

        //When Gauge is empty
        if (Battery <= _batteryMin)
        {
            //End game
            Battery = _batteryMin;
            GameManager.Instance.GameOver();
        }

        SetBatteryGaugeFill();
    }

    public void IncreaseBattery(float valueIncrease)
    {
        Battery += valueIncrease;

        if(Battery > _batteryMax)
        {
            Battery = _batteryMax;
        }

        SetBatteryGaugeFill();
    }

    void SetBatteryGaugeFill()
    {
        _batteryGaugeFill.localScale = new Vector3(1f, Mathf.InverseLerp(_batteryMin, _batteryMax, Battery), 1f);
        SetPostProcessing();
        //print(_batteryGaugeFill.localScale);
    }

    void SetPostProcessing()
    {
        float percentage = 1 - GetPercentageBattery();                                  //Invert value
        PostProcessingController.Instance.SetChromaticAberrationIntensity(percentage);
    }

    float GetPercentageBattery()
    {
        return Mathf.InverseLerp(_batteryMin, _batteryMax, Battery);
    }
}
