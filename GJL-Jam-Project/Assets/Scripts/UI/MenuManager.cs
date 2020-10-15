using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

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

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public bool TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
        return panel.activeInHierarchy;
    }

    public void SetPanelActiveState(GameObject panel, bool state)
    {
        panel.SetActive(state);
    }
}
