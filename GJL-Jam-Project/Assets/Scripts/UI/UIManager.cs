using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] TMP_Text _speedCounter;
    [SerializeField] GameObject _pausePanel;

    private bool _isPaused;
    public bool IsPaused
    {
        get { return _isPaused; }
        private set
        {
            _isPaused = value;
            if (_isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

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

    private void Start()
    {
        if (_pausePanel.activeInHierarchy)
        {
            _pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _speedCounter.text = "Speed: " + Mathf.Round(PlayerMovement.Instance.GetPlayerVelocityMagnitude());

        if (Input.GetButtonDown("Cancel"))
        {
            IsPaused = MenuManager.Instance.TogglePanel(_pausePanel);
        }
    }

    public void SetPauseState(bool state)
    {
        IsPaused = state;
        if (!GameManager.Instance.GameEnded)
        {
            MenuManager.Instance.SetPanelActiveState(_pausePanel, state);
        }
    }
}
