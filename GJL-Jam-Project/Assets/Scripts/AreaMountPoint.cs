using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaMountPoint : MonoBehaviour
{
    public Vector3 cubeGizmoOffset;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawSphere(Vector3.zero, 10f);
        Gizmos.DrawCube(Vector3.zero + cubeGizmoOffset, new Vector3(5f, 5f, 20f));     //Draw cuboid in direction object is facing
    }


}
