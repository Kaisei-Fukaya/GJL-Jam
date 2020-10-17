using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSpinController : MonoBehaviour
{
    public Animator[] _tunnels;

    private void Start()
    {
        bool clockwise = true;
        foreach (var a in _tunnels)
        {
            a.SetBool("clockwise", clockwise);
            clockwise = !clockwise;
        }
    }
}
