using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool GameGoing { get; private set; }
    public bool GameEnded { get; private set; } = false;
    public float yBound;

    [SerializeField] CanvasGroup _blackScreen;
    [SerializeField] GameObject _gameOverPanel;
    Coroutine lerpCoroutine;

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

        _gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameGoing)
        {
            if (!UIManager.Instance.IsPaused)
            {

            }
            else
            {

            }
        }
        else
        {

        }

        if (PlayerInput.Instance.transform.position.y < yBound && !GameEnded)
        {
            GameOver();
        }
    }

    //Starts the timer and score tracking
    public void StartGameLoop()
    {
        print("Started Game");
        GameGoing = true;
    }

    //Eng game bring up menu
    public void GameOver()
    {
        GameGoing = false;
        GameEnded = true;
        lerpCoroutine = StartCoroutine(LerpValueOverTime(0, 1, _blackScreen, 1f));
        PlayerInput.Instance.enabled = false;
        UIManager.Instance.SetPauseState(true);
        MenuManager.Instance.SetPanelActiveState(_gameOverPanel, true);
    }


    IEnumerator LerpValueOverTime(float valA, float valB, CanvasGroup cg, float timeScale)
    {
        cg.alpha = valA;
        while (true)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, valB, timeScale * Time.deltaTime);
            if(cg.alpha == valB)
            {
                break;
            }
            yield return null;
        }
    }


}
