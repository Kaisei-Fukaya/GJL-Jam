using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PropSpawner : MonoBehaviour
{
    public EnvironmentSpawnPoint[] spawnPoints;
    public EnvironmentSpawnPoint[] enemySpawnPoints;

    public int pickupsToSpawn = 3, enemiesToSpawn = 2;

    private void Start()
    {
        //test
        Spawn(pickupsToSpawn, spawnPoints);
        Spawn(enemiesToSpawn, enemySpawnPoints);
    }

    //Only to be called one in lifetime
    public void Spawn(int nOfObstacles, EnvironmentSpawnPoint[] sPoints)
    {
        List<EnvironmentSpawnPoint> spawnPointPool = sPoints.ToList();
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
