using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageToPlayer : MonoBehaviour
{
    public int poolMax = 5;
    public float spreadRadius = 10f;

    [SerializeField] GameObject textOriginalPrefab;
    List<TMP_Text> _textElementPool;

    Coroutine displayingMessagesCoroutine, stopTimerCoroutine;
    [SerializeField] GameObject _uiPanel;

    public static MessageToPlayer Instance { get; private set; }

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

        _textElementPool = new List<TMP_Text>();
    }

    private void Start()
    {
        //TEST
        string[] introText = { "Run", "Escape", "Go" };
        DisplayMessageForSetTime(introText, 1f, 0.5f, 5f);
    }

    public void DisplayMessage(string[] text, float messageLife, float timeBetweenMessages)
    {
        StopRunningCoroutines();
        displayingMessagesCoroutine = StartCoroutine(DisplayingMessages(text, messageLife, timeBetweenMessages));
    }

    public void DisplayMessageForSetTime(string[] text, float messageLife, float timeBetweenMessages, float timeToDisplay)
    {
        StopRunningCoroutines();
        displayingMessagesCoroutine = StartCoroutine(DisplayingMessages(text, messageLife, timeBetweenMessages));
        stopTimerCoroutine = StartCoroutine(StopTimer(timeToDisplay));
    }

    void StopRunningCoroutines()
    {
        if(stopTimerCoroutine != null)
        {
            StopCoroutine(stopTimerCoroutine);
        }
        if(displayingMessagesCoroutine != null)
        {
            StopCoroutine(displayingMessagesCoroutine);
        }
    }

    public void StopDisplayingMessage()
    {
        foreach (var t in _textElementPool)
        {
            t.gameObject.SetActive(false);
        }
        StopCoroutine(displayingMessagesCoroutine);
    }

    public TMP_Text SpawnMessage(string text, Vector2 rectPosition, RectTransform rectTransform, TMP_Text messageText, float messageLife)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = text;
        rectTransform.anchoredPosition = rectPosition;
        StartCoroutine(MessageLife(messageText.gameObject, messageLife));
        return messageText;
    }

    IEnumerator MessageLife(GameObject message, float life)
    {
        float timer = 0f;
        while (true)
        {
            if(timer < life)
            {
                //print(timer);
                timer += Time.deltaTime;
            }
            else
            {
                message.SetActive(false);
                //print("closed message");
                break;
            }
            yield return null;
        }
    }

    IEnumerator StopTimer(float timeToDisplay)
    {
        float timer = 0f;
        while(timer < timeToDisplay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        StopDisplayingMessage();

    }

    IEnumerator DisplayingMessages(string[] text, float messageLife, float timeBetweenMessages)
    {
        float timer = timeBetweenMessages;
        while (true)
        {
            if(timer < timeBetweenMessages)
            {
                timer += Time.deltaTime;
            }
            else
            {
                //Spawn new
                if(_textElementPool.Count < poolMax)
                {
                    //Create new
                    GameObject newTextGameObject = Instantiate(textOriginalPrefab, _uiPanel.transform);
                    _textElementPool.Add(SpawnMessage(text[UnityEngine.Random.Range(0, text.Length)], UnityEngine.Random.insideUnitCircle * spreadRadius, newTextGameObject.GetComponent<RectTransform>(), newTextGameObject.GetComponent<TMP_Text>(), messageLife));
                }
                else
                {
                    //Re-use
                    for (int i = 0; i < _textElementPool.Count; i++)
                    {
                        //Useable if inactive
                        if (!_textElementPool[i].gameObject.activeInHierarchy)
                        {
                            SpawnMessage(text[UnityEngine.Random.Range(0, text.Length)], UnityEngine.Random.insideUnitCircle * spreadRadius, _textElementPool[i].GetComponent<RectTransform>(), _textElementPool[i], messageLife);
                            break;
                        }
                    }
                }

                timer = 0f;
            }
            yield return null;
        }
    }

}
