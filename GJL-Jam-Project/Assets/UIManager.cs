using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] TMP_Text _speedCounter;

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

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _speedCounter.text = "Speed: " + Mathf.Round(PlayerMovement.Instance.GetPlayerVelocityMagnitude());
    }
}
