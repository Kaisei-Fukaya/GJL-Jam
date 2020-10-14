using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoadManager : MonoBehaviour
{
    [SerializeField] GameObject[] _areaPrefabs;

    public static AreaLoadManager Instance { get; private set; }

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

    public void LoadArea(GameObject newArea, AreaMountPoint mountPoint)
    {

    }
}
