using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PropSpawner : MonoBehaviour
{
    [SerializeField] EnvironmentSpawnPoint[] spawnPoints;

    private void Start()
    {
        //test
        Spawn(6);
    }

    //Only to be called one in lifetime
    public void Spawn(int nOfObstacles)
    {
        List<EnvironmentSpawnPoint> spawnPointPool = spawnPoints.ToList();
        if(nOfObstacles > spawnPointPool.Count)
        {
            nOfObstacles = spawnPointPool.Count;                                                            //Cap number of obstacles to size of pool
        }

        for (int i = 0; i < nOfObstacles; i++)
        {
            int chosenPointIndex = Random.Range(0, spawnPointPool.Count);

            var point = spawnPointPool[chosenPointIndex];
            point.SpawnObject(point.objectsToSpawn[Random.Range(0, point.objectsToSpawn.Length)]);          //Spawn random object from available array

            spawnPointPool.RemoveAt(chosenPointIndex);                                                      //Remove from pool
        }
    }
}
