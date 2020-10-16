using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGameTrigger : MonoBehaviour
{
    public UnityEvent userDefinedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.StartGameLoop();
            userDefinedEvent.Invoke();
            enabled = false;
        }
    }
}
