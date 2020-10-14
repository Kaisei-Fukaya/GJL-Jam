using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawnPoint : MonoBehaviour
{
    public Vector3 cubeGizmoOffset;

    public GameObject[] objectsToSpawn;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawSphere(Vector3.zero, 1f);
        Gizmos.DrawCube(Vector3.zero + cubeGizmoOffset, new Vector3(1f, 1f, 3f));     //Draw cuboid in direction object is facing
    }

    public void SpawnObject(GameObject objectToSpawn)
    {
        var newObject = Instantiate(objectToSpawn);
        newObject.transform.position = transform.position;
        newObject.transform.rotation = transform.rotation;
    }
}
