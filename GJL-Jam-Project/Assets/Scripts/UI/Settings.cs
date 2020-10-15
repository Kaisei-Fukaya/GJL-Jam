using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    static Settings _instance;
    public static Settings Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Settings();
                return _instance;
            }
            else
            {
                return _instance;
            }
        }
    }

    public void SaveIntValue(string valueName, int value)
    {
        PlayerPrefs.SetInt(valueName, value);
    }
    public void SaveFloatValue(string valueName, float value)
    {
        PlayerPrefs.SetFloat(valueName, value);
    }
    public void SaveStringValue(string valueName, string value)
    {
        PlayerPrefs.SetString(valueName, value);
    }
    public int LoadIntValue(string valueName)
    {
        return PlayerPrefs.GetInt(valueName);
    }
    public float LoadFloatValue(string valueName)
    {
        return PlayerPrefs.GetFloat(valueName);
    }
    public string LoadStringValue(string valueName)
    {
        return PlayerPrefs.GetString(valueName);
    }

}
