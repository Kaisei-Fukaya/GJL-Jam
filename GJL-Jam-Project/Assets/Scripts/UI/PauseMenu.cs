using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _settingsPanel, _mainPanel, _speedCount;

    const string SpeedCountKey = "speedCount";

    private void Start()
    {
        //Initialise settings
        _speedCount.SetActive(StringToBool(Settings.Instance.LoadStringValue(SpeedCountKey)));
    }

    public void ResumeGame() => UIManager.Instance.SetPauseState(false);

    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
        _mainPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }

    public void ShowSpeedCount(bool toggle)
    {
        _speedCount.SetActive(toggle);
        Settings.Instance.SaveStringValue(SpeedCountKey, toggle.ToString());
    }

    bool StringToBool(string value)
    {
        if(value == "True")
        {
            return true;
        }

        if(value == "False")
        {
            return false;
        }

        return false;
    }


}
