using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public UnityEvent userDefinedEvent;

    public string tagToCheck = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagToCheck)
        {
            userDefinedEvent.Invoke();
        }
    }
}
