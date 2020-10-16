using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoadManager : MonoBehaviour
{
    [SerializeField] GameObject[] _areaPrefabs;
    List<GameObject> _currentAreas;

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

        _currentAreas = new List<GameObject>();
        //TEST
        if (GameObject.Find("Area_Street_Section1"))
        {
            _currentAreas.Add(GameObject.Find("Area_Street_Section1"));
        }
    }

    public void LoadNextArea(AreaMountPoint mountPoint, GameObject areaToLoad)
    {
        print("loadNextArea");
        ////Randomisation
        //GameObject areaToLoad = _areaPrefabs[Random.Range(0, _areaPrefabs.Length)];

        LoadArea(areaToLoad, mountPoint);

        //Remove the first area in the list (oldest)
        if(_currentAreas.Count > 7)
        {
            RemoveArea(0);
        }
    }

    internal void LoadNextArea(AreaMountPoint m, object nextArea)
    {
        throw new NotImplementedException();
    }

    void LoadArea(GameObject areaToLoad, AreaMountPoint mountPoint)
    {
        var newArea = Instantiate(areaToLoad);
        newArea.transform.position = mountPoint.transform.position;
        newArea.transform.rotation = mountPoint.transform.rotation;
        _currentAreas.Add(newArea);
    }

    void RemoveArea(int areaIndex)
    {
        Destroy(_currentAreas[areaIndex]);
        _currentAreas.RemoveAt(areaIndex);
    }
}
