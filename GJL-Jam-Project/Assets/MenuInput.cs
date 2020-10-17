using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _mainPanel;

    const string SpeedCountKey = "speedCount";

    private void Start()
    {
        _settingsPanel.SetActive(false);
    }

    public void SettingsPanelToggle(bool val)
    {
        _settingsPanel.SetActive(val);
        _mainPanel.SetActive(!val);
    }

    public void ShowSpeedCount(bool toggle)
    {
        Settings.Instance.SaveStringValue(SpeedCountKey, toggle.ToString());
    }
}
