using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool GameGoing { get; private set; }

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
        if (GameGoing)
        {
            if (!UIManager.Instance.IsPaused)
            {

            }
            else
            {

            }
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
    }


}
